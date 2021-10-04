using System;
using System.Threading.Tasks;
using Core.Enums;
using FluentValidation;

namespace Core.Helpers
{
    public static class ServiceExtensions
    {
        public static async Task ValidateModel<T>(
            IValidator<T> validator, 
            ValidationModelType validationType, 
            T model
        )
        {
            await validator.ValidateAsync
            (
                instance: model,
                options: o =>
                {
                    o.IncludeRuleSets (GetTypeName(validationType));
                    o.ThrowOnFailures ();
                }
            );
        }
        
        private static string GetTypeName (ValidationModelType validationType)
        {
            return Enum.GetName (typeof(ValidationModelType), validationType);
        }
    }
}