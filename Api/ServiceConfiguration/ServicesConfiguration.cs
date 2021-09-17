using Core.Infrastructure;
using Core.Models;
using Core.Services;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Api.ServiceConfiguration
{
    public static class ServicesConfiguration
    {
        public static void ServicesConfigure(this IServiceCollection services)
        {
            services.AddSingleton<IDeskService, DeskService>();
            services.AddSingleton<IReservationService, ReservationService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton(typeof(IRepository<Desk>), typeof(DeskRepository<Desk>));
            services.AddSingleton(typeof(IRepository<Reservation>), typeof(ReservationRepository<Reservation>));
            services.AddSingleton(typeof(IRepository<User>), typeof(UserRepository<User>));
        }
    }
}
