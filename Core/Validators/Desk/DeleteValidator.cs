using Core.Enums;
using FluentValidation;

namespace Core.Validators.Desk
{
    public class DeleteValidator : AbstractValidator<Models.Desk>
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