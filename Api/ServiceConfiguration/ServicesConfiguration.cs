using Core.Infrastructure;
using Core.Models;
using Core.Services;
using Core.Validators;
using FluentValidation;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Api.ServiceConfiguration
{
    public static class ServicesConfiguration
    {
        public static void ServicesConfigure(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IDeskService, DeskService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IUserService, UserService>();
            
            // Repositories
            services.AddScoped(typeof(IRepository<Desk>), typeof(DeskRepository<Desk>));
            services.AddScoped(typeof(IRepository<Reservation>), typeof(ReservationRepository<Reservation>));
            services.AddScoped(typeof(IRepository<User>), typeof(UserRepository<User>));

            // Validators
            services.AddScoped<IValidator<Desk>, DeskValidator> ();
            services.AddScoped<IValidator<Reservation>, ReservationValidator> ();
            services.AddScoped<IValidator<User>, UserValidator> ();
        }
    }
}
