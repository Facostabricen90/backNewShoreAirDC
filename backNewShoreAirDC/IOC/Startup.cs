using Autofac;
using ServicesLayer.WebServices;
using ServicesLayer.IWebServices;
using DomainLayer.Contracts;
using BusinessLayer.BusinessDisponibility;

namespace backNewShoreAirDC.IOC
{
    public class Startup
    {
        // Other code is removed for clarity

        /// <summary>
        /// Configure Container using Autofac: Register DI.
        /// This is called AFTER ConfigureServices.
        /// So things you register here OVERRIDE things registered in ConfigureServices.
        /// You must have the call to `UseServiceProviderFactory(new AutofacServiceProviderFactory())` in Program.cs
        /// When building the host or this won't be called.
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            #region WAY-2 (Direct Registration)

            // Add services registrations.

            builder.RegisterType<WebService>().As<IWebService>();
            builder.RegisterType<DisponibilityBusiness>().As<IDisponibilityBusiness>();

            #endregion
        }
    }
}
