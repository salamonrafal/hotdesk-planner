using System;
using Core.Enums;
using FluentValidation;

namespace Core.Validators.Reservation
{
    public class UpdateValidator: AbstractValidator<Models.Reservation>
    {
        public UpdateValidator()
        {
            RuleSet (nameof(ValidationModelType.Update), () =>
            {
                RuleFor (model => model.Id).NotNull ().NotEmpty ().NotEqual (Guid.Empty);
                RuleFor (model => model.AssignedTo).Equal (Guid.Empty);
                RuleFor (model => model.DeskId).Equal (Guid.Empty);
                
                RuleFor (model => model.StartDate)
                    .NotEmpty ()
                    .NotEqual (model => model.EndDate)
                    .When (model => model.StartDate != null);
                
                RuleFor (model => model.EndDate)
                    .NotEmpty ()
                    .NotEqual (model => model.StartDate)
                    .When (model => model.EndDate != null);
                
                RuleFor (model => model.IsPeriodical)
                    .NotEmpty ()
                    .When (model => model.IsPeriodical != null);
            });
        }
    }
}