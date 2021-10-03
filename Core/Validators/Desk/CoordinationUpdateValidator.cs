using FluentValidation;

namespace Core.Validators.Desk
{
    public class CoordinationUpdateValidator: AbstractValidator<Models.Coordination>
    {
        public CoordinationUpdateValidator()
        {
            RuleFor (model => model.X)
                .NotEmpty ()
                .When (model => model.X != null);
            
            RuleFor (model => model.Y)
                .NotEmpty ()
                .When (model => model.Y != null);
        }
    }
}