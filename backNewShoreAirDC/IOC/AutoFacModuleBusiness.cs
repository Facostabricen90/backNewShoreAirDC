using Autofac;
using Autofac.Core;
using AutoMapper;
using BusinessLayer.BusinessDisponibility;
using BusinessLayer.Mapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using DomainLayer.Models.Third;
using ServicesLayer.WebServices;

namespace backNewShoreAirDC.IOC
{
    public class AutoFacModuleBusiness : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WebService>().As<WebService>().SingleInstance();
            builder.RegisterType<DisponibilityBusiness>().As<DisponibilityBusiness>().SingleInstance();
            builder.RegisterType<FlightResponse_Journey<List<Flight>, List<FlightResponse>>>().As<IMap<List<Flight>, List<FlightResponse>>>().SingleInstance();
            builder.RegisterType<Flight_Transport<Transport, FlightResponse>>().As<IMap<Transport, FlightResponse>>().SingleInstance();

            builder.Register(context => new MapperConfiguration(creationFlight =>
            {
                creationFlight.CreateMap<List<FlightResponse>, List<Flight>>();
            })).AsSelf().SingleInstance();

            builder.Register(create =>
            {
                var context = create.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            }).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}
