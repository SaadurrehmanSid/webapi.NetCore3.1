using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibray.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibray.Api.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDTO>();
            CreateMap<CreateCourseDTO, Course>();
        }
    }
}
