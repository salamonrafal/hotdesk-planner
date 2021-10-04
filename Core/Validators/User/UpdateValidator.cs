using Core.Enums;
using FluentValidation;

namespace Core.Validators.User
{
    public class UpdateValidator: AbstractValidator<Models.User>
    {
        public UpdateValidator()
        {
            RuleSet (nameof(ValidationModelType.Update), () =>
            {
                RuleFor (model => model.Id)
                    .NotNull ()
                    .NotEmpty ();

                RuleFor (model => model.Name)
                    .NotEmpty ()
                    .When (model => model.Name != null);
                
                RuleFor (model => model.Surname)
                    .NotEmpty ()
                    .When (model => model.Surname != null);
                
                RuleFor (model => model.Email)
                    .NotEmpty ()
                    .When (model => model.Email != null);
                
                RuleFor (model => model.UrlAvatar)
                    .NotEmpty ()
                    .When (model => model.UrlAvatar != null);
                
                RuleFor (model => model.Role)
                    .NotEmpty ()
                    .When (model => model.Role != null);
            });
        }
    }
}