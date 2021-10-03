using Core.Validators.Desk;
using FluentValidation;
using DeskModel = Core.Models.Desk;

namespace Core.Validators
{
    public class DeskValidator: AbstractValidator<DeskModel>
    {
        public DeskValidator()
        {
            Include (new InsertValidator ());
            Include (new DeleteValidator ());
            Include (new UpdateValidator ());
            Include (new GetOneValidator ());
        }
    }
}