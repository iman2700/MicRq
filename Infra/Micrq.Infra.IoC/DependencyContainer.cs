using Microsoft.Extensions.DependencyInjection;
using MicRq.Infra.Bus;

namespace Micrq.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services) 
        {
            services.AddTransient<IEventBus, RabbitMqBus>();
        }
    }
}