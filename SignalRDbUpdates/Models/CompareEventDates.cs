using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SignalRDbUpdates.Models
{
    public class CompareEventDates : ValidationAttribute
    {
        private const string DefualtErrorMessage = "";
        private readonly string _dependentPropertyName;

        public CompareEventDates(string dependentPropertyName)
        {
            _dependentPropertyName = dependentPropertyName; 
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Get details about the dependent property
            var dependentProperty =
                Utility.GetPropertyInfo(validationContext, _dependentPropertyName);
            var dependentPropertyValue =
                Utility.GetPropertyValueAsString(validationContext, _dependentPropertyName);

            if (bool.TryParse(Convert.ToString(dependentPropertyValue), out var hasValue))
            {
                //Validation should fail if the checkbox is selected but the main field is empty
                if (hasValue  && string.IsNullOrEmpty(Convert.ToString(value)))
                {
                    return new ValidationResult(Utility.GetValidationErrorMessage(this,DefualtErrorMessage,
                        Utility.GetDisplayName(validationContext),
                        Utility.GetDisplayName(dependentProperty)));
                }
            }
            return ValidationResult.Success;
        }
    }
}