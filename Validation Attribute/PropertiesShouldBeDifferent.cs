using CourseLibray.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibray.Api.Validation_Attribute
{
    public class PropertiesShouldBeDifferent : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var course = (CreateCourseDTO)validationContext.ObjectInstance;
            if (course.Title == course.Description)
            {
                return new ValidationResult(ErrorMessage, new[] { "CreateCourseDTO" });
            }

            return ValidationResult.Success;
        }
    }
}
