using CourseLibray.Api.Validation_Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibray.Api.Models
{
    [PropertiesShouldBeDifferent(ErrorMessage ="two fields cannot have same values")]
    public class CreateCourseDTO  // : IValidatableObject
    {
        public string Title { get; set; }

        public string Description { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Title == Description)
        //    {
        //        yield return new ValidationResult("title and description can not be same", new []{ "CreateCourseDTO"});
        //    }
        //}
    }
}
