using Core.Validators.Reservation;
using FluentValidation;
using ReservationModel = Core.Models.Reservation;

namespace Core.Validators
{
    public class ReservationValidator: AbstractValidator<ReservationModel>
    {
        public ReservationValidator()
        {
            Include (new InsertValidator ());
            Include (new DeleteValidator ());
            Include (new UpdateValidator ());
            Include (new GetOneValidator ());
        }
    }
}