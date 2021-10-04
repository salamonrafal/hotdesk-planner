using Core.Enums;
using FluentValidation;

namespace Core.Validators.Reservation
{
    public class GetOneValidator: AbstractValidator<Models.Reservation>
    {
        public GetOneValidator()
        {
            RuleSet (nameof(ValidationModelType.GetOne), () =>
            {
                RuleFor (model => model.Id)
                    .NotEmpty ();
            });
        }
    }
}