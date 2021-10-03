using Core.Enums;
using FluentValidation;

namespace Core.Validators.Desk
{
    public class GetOneValidator: AbstractValidator<Models.Desk>
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