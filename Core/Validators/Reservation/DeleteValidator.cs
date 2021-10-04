using Core.Enums;
using FluentValidation;

namespace Core.Validators.Reservation
{
    public class DeleteValidator: AbstractValidator<Models.Reservation>
    {
        public DeleteValidator()
        {
            RuleSet (nameof(ValidationModelType.Delete), () =>
            {
                RuleFor (model => model.Id).NotEmpty ();
            });
        }
    }
}