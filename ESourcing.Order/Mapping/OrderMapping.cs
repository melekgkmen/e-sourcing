using AutoMapper;
using EventBusRabbitMQ.Events.Interfaces;
using OrderingApplication.Commands.OrderCreate;

namespace ESourcing.Order.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderCreateEvent, OrderCreateCommand>().ReverseMap();
        }
    }
}
