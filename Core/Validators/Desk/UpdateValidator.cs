using Core.Enums;
using FluentValidation;

namespace Core.Validators.Desk
{
    public class UpdateValidator: AbstractValidator<Models.Desk>
    {
        public UpdateValidator()
        {
            RuleSet (nameof(ValidationModelType.Update), () =>
            {
                RuleFor (model => model.Id).NotNull ().NotEmpty ();
                
                RuleFor (model => model.Description)
                    .NotEmpty ()
                    .When (model => model.Description != null);
                
                RuleFor (model => model.Localization)
                    .SetValidator (new LocalizationUpdateValidator ())
                    .When (model => model.Description != null);
            });
        }
    }
}