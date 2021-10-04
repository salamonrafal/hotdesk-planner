using FluentValidation;

namespace Core.Validators.Desk
{
    public class LocalizationUpdateValidator: AbstractValidator<Models.Localization>
    {
        public LocalizationUpdateValidator()
        {
            RuleFor (model => model.Floor)
                .NotEmpty ()
                .When (model => model.Floor != null);
            
            RuleFor (model => model.Outbuilding)
                .NotEmpty ()
                .When (model => model.Outbuilding != null);
            
            RuleFor (model => model.Coordination)
                .Cascade (CascadeMode.Stop)
                .SetValidator (new CoordinationUpdateValidator ())
                .When (model => model.Coordination != null);
        }
    }
}