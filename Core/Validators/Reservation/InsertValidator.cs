using Core.Enums;
using Core.Validators.Desk;
using FluentValidation;

namespace Core.Validators.Reservation
{
    public class InsertValidator: AbstractValidator<Models.Reservation>
    {
        public InsertValidator()
        {
            RuleSet (nameof(ValidationModelType.Insert), () =>
            {
                RuleFor (model => model.Id)
                    .Empty ();
                
                RuleFor (model => model.AssignedTo)
                    .NotNull ()
                    .NotEmpty ();

                RuleFor (model => model.DeskId)
                    .Cascade (CascadeMode.Stop)
                    .NotNull ();
                
                RuleFor (model => model.StartDate)
                    .NotNull ()
                    .NotEqual (model => model.EndDate);
                
                RuleFor (model => model.EndDate)
                    .NotNull ();
            });
        }
    }
}