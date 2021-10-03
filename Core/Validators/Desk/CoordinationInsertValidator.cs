using FluentValidation;

namespace Core.Validators.Desk
{
    public class CoordinationInsertValidator: AbstractValidator<Models.Coordination>
    {
        public CoordinationInsertValidator()
        {
            RuleFor (model => model.X)
                .NotNull ()
                .NotEmpty ();
            
            RuleFor (model => model.Y)
                .NotNull ()
                .NotEmpty ();
        }
    }
}