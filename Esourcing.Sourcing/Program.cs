using Esourcing.Sourcing.Data.Interface;
using Esourcing.Sourcing.Data;
using Esourcing.Sourcing.Settings;
using Microsoft.Extensions.Options;
using Esourcing.Sourcing.Repositories.Interfaces;
using Esourcing.Sourcing.Entities;
using Esourcing.Sourcing.Repositories;
using Microsoft.OpenApi.Models;
using EventBusRabbitMQ;
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using EventBusRabbitMQ.Producer;
using AutoMapper;
using Esourcing.Sourcing.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.Configure<SourcingDatabaseSettings>(builder.Configuration.GetSection(nameof(SourcingDatabaseSettings)));
builder.Services.AddSingleton<ISourcingDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<SourcingDatabaseSettings>>().Value);

#region Project Dependencis
builder.Services.AddSingleton<ISourcingContext, SourcingContext>();
builder.Services.AddTransient<ISourcingContext, SourcingContext>();
builder.Services.AddTransient<IAuctionRepository, AuctionRepository>();
builder.Services.AddTransient<IBidRepository, BidRepository>();

builder.Services.AddAutoMapper(typeof(Program));
#endregion

#region Swagger Dependencis
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "ESourcing.Sourcing", Version = "v1" });
});
# endregion

#region EventBus

builder.Services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

    var factory = new ConnectionFactory() { 
        HostName = builder.Configuration["EventBus:HostName"]
    };

    if(!string.IsNullOrWhiteSpace(builder.Configuration["EventBus:HostName"]))
    {
        factory.UserName = builder.Configuration["EventBus:HostName"];
    }

    if (!string.IsNullOrWhiteSpace(builder.Configuration["EventBus:Password"]))
    {
        factory.UserName = builder.Configuration["EventBus:Password"];
    }

    var retryCount = 5;
    if(!string.IsNullOrWhiteSpace(builder.Configuration["EventBus:RetryCount"]))
    {
        retryCount = int.Parse(builder.Configuration["EventBus:RetryCount"]);
    }

    return new DefaultRabbitMQPersistentConnection(factory, retryCount, logger);
});
#endregion

builder.Services.AddSingleton<EventBusRabbitMQProducer>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sourcing API V1");
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<AuctionHub>("/auctionhub");
    endpoints.MapControllers();
});

builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins("https://localhost:44398");
}));

app.UseCors("CorsPolicy");

builder.Services.AddSignalR();