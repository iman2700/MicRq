using Microsoft.Extensions.DependencyInjection;
using MicRq.Banking.Application.Interface;
using MicRq.Banking.Application.Service;
using MicRq.Banking.Data.Context;
using MicRq.Banking.Data.Repository;
using MicRq.Banking.Domain.Interfaces;
using MicRq.Domain.Bus;
using MicRq.Infra.Bus;

namespace Micrq.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services) 
        {
            services.AddTransient<IEventBus, RabbitMqBus>();
            services.AddTransient<IAccountServices, AccountService>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            //services.AddTransient<BankingDbContext1>();
        }
    }
}