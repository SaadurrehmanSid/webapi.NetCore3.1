using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibray.Api.Helpers
{
    public static class DateTimeOffsetExtensions
    {
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset) 
        {
            
            var age = DateTime.UtcNow.Year - dateTimeOffset.Year;
            return age;

        }
    }
}
