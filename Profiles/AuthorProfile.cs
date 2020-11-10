using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibray.Api.Helpers;
using CourseLibray.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibray.Api.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDTO>()
                .ForMember(
                    dest =>dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
                ).ForMember(
                        dest => dest.Age,
                        opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge())
                );

            CreateMap<CreateAuthorDTO, Author>();
        }
    }
}
