using Core.Enums;
using FluentValidation;

namespace Core.Validators.User
{
    public class DeleteValidator: AbstractValidator<Models.User>
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