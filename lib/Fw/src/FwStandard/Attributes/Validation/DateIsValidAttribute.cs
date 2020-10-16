using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FwStandard.Attributes.Validation
{
    public class DateIsValidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"{validationContext.DisplayName}: cannot be null");
            }
            DateTime parsedDate = DateTime.MinValue;
            if (DateTime.TryParse(value.ToString(), out parsedDate))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"{validationContext.DisplayName}: Invalid date \"{value}\"");
            }
        }
    }
}
