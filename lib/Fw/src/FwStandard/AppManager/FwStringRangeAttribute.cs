using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FwStandard.AppManager
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FwStringRangeAttribute : ValidationAttribute
    {
        public readonly string[] AllowableValues;
        public readonly bool IsRequired;

        public FwStringRangeAttribute(bool IsRequired, string[] AllowableValues)
        {
            this.IsRequired = IsRequired;
            this.AllowableValues = AllowableValues;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((IsRequired == false) && (value == null))
            {
                return ValidationResult.Success;
            }
            else if (AllowableValues?.Contains(value?.ToString()) == true)
            {
                return ValidationResult.Success;
            }
            else
            {
                var msg = $"Please enter one of the allowable values: {string.Join(", ", (AllowableValues ?? new string[] { "No allowable values found" }))}.";
                return new ValidationResult(msg);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
