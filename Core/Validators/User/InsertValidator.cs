using Core.Enums;
using FluentValidation;

namespace Core.Validators.User
{
    public class InsertValidator: AbstractValidator<Models.User>
    {
        public InsertValidator()
        {
            RuleSet (nameof(ValidationModelType.Insert), () =>
            {
                RuleFor (model => model.Id)
                    .NotNull ()
                    .Empty ();
                
                RuleFor (model => model.Email)
                    .NotNull ()
                    .NotEmpty ();

                RuleFor (model => model.Name)
                    .NotNull ()
                    .NotEmpty ();
                
                RuleFor (model => model.Surname)
                    .NotNull ()
                    .NotEmpty ();
                
                RuleFor (model => model.Password)
                    .NotNull ()
                    .NotEmpty ();
                
                RuleFor (model => model.Role)
                    .NotNull ()
                    .NotEmpty ();
            });
        }
    }
}