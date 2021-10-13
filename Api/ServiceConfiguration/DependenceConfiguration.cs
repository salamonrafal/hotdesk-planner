using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Api.ServiceConfiguration
{
    public static class DependenceConfiguration
    {
        public static void DependenceConfigure(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddMvc().AddJsonOptions( op => {
                op.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        }
    }
}
