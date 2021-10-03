using FluentValidation;

namespace Core.Validators.Desk
{
    public class LocalizationInsertValidator: AbstractValidator<Models.Localization>
    {
        public LocalizationInsertValidator()
        {
            RuleFor (model => model.Floor)
                .NotNull ()
                .NotEmpty ();
            
            RuleFor (model => model.Outbuilding)
                .NotNull ()
                .NotEmpty ();
            
            RuleFor (model => model.Coordination)
                .Cascade (CascadeMode.Stop)
                .NotNull ()
                .SetValidator (new CoordinationInsertValidator ());
        }
    }
}