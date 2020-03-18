using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace BLL.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CompareEventDates : ValidationAttribute
    {
        public CompareEventDates(string otherPropertyName, bool allowEquality = true)
        {
            AllowEquality = allowEquality;
            OtherPropertyName = otherPropertyName;
        }
        #region Properties

        /// <summary>
        /// Gets the name of the  property to compare to
        /// </summary>
        private string OtherPropertyName { get; set; }

        /// <summary>
        /// Gets a value indicating whether dates could be the same
        /// </summary>
        private bool AllowEquality { get; set; }

        #endregion
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = ValidationResult.Success;
            var otherValue = validationContext.ObjectType.GetProperty(OtherPropertyName) ?.GetValue(validationContext.ObjectInstance, null);
            if (value == null) return result;
            if (!(value is DateTime)) return result;
            if (otherValue == null) return result;
            if (!(otherValue is DateTime)) return result;
            if (!OtherPropertyName.Contains("EventDateTime"))
            {
                if ((DateTime)value > (DateTime)otherValue)
                {
                    result = new ValidationResult(ErrorMessage);
                }
            }
            else
            {
                if ((DateTime)value < (DateTime)otherValue)
                {
                    result = new ValidationResult(ErrorMessage);
                }
            }
            if ((DateTime)value == (DateTime)otherValue && !AllowEquality)
            {
                result = new ValidationResult(ErrorMessage);
            }
            return result;
        }
    }
}