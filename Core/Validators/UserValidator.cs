using Core.Validators.User;
using FluentValidation;
using UserModel = Core.Models.User;

namespace Core.Validators
{
    public class UserValidator: AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            Include (new InsertValidator ());
            Include (new DeleteValidator ());
            Include (new UpdateValidator ());
            Include (new GetOneValidator ());
        }
    }
}