using Core.Enums;
using FluentValidation;

namespace Core.Validators.Desk
{
    public class InsertValidator: AbstractValidator<Models.Desk>
    {
        public InsertValidator()
        {
            RuleSet (nameof(ValidationModelType.Insert), () =>
            {
                RuleFor (model => model.Id)
                    .Empty ();
                
                RuleFor (model => model.Description)
                    .NotNull ()
                    .NotEmpty ();
                
                RuleFor (model => model.Localization)
                    .Cascade (CascadeMode.Stop)
                    .NotNull ()
                    .SetValidator (new LocalizationInsertValidator ());
                
                RuleFor (model => model.IsBlocked)
                    .NotNull ();
            });
        }
    }
}