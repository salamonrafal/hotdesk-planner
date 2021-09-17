using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Api.ServiceConfiguration
{
    public static class DependenceConfiguration
    {
        public static void DependenceConfigure(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
