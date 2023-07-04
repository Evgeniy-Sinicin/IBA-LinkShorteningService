using LinkShorteningService.DataAccess.Interfaces;
using LinkShorteningService.DataAccess.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace LinkShorteningService.BusinessLogic.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterEfAsIUnitOfWork(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IUnitOfWork>(service => new EfUnitOfWork(connectionString));
        }
    }
}
