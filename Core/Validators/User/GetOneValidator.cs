using Core.Enums;
using FluentValidation;

namespace Core.Validators.User
{
    public class GetOneValidator: AbstractValidator<Models.User>
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