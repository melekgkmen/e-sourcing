using AutoMapper;
using OrderingApplication.Commands.OrderCreate;
using OrderingApplication.Responses;
using OrderingDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingApplication.Mapper
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            CreateMap<Order, OrderCreateCommand>().ReverseMap();
            CreateMap<Order, OrderResponse>().ReverseMap();
        }
    }
}
