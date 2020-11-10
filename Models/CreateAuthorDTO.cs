using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibray.Api.Models
{
    public class CreateAuthorDTO
    {

        public string FirstName { get; set; }

       
        public string LastName { get; set; }

      
        public DateTimeOffset DateOfBirth { get; set; }

    
        public string MainCategory { get; set; }

        public List<CreateCourseDTO> CourseDTOs { get; set; }
          = new  List<CreateCourseDTO>();
    }
}
