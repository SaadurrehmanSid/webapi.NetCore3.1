using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using CourseLibray.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibray.Api.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CourseController : ControllerBase
    {

        private ICourseLibraryRepository _courseLibrary;
        private IMapper _mapper;

        public CourseController(ICourseLibraryRepository courseLibrary, IMapper mapper)
        {
            _courseLibrary = courseLibrary ?? throw new ArgumentNullException(nameof(_courseLibrary));
            _mapper = mapper;
        }



       
       
      
        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<CourseDTO>> GetCourses(Guid authorId)
        {
            if (!_courseLibrary.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courses = _courseLibrary.GetCourses(authorId);

            if (courses is null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<CourseDTO>>(courses));
        }


        [HttpGet("{courseId}",Name ="GetCourseForAuthor")]
        public ActionResult GetCourse(Guid authorId, Guid courseId)
        {
            if (!_courseLibrary.AuthorExists(authorId))
            {
                return BadRequest("wrong Id");
            }
            
            if (courseId == null)
            {
                return BadRequest("wrong Id");
            }
            var course = _courseLibrary.GetCourse(authorId, courseId);
            return Ok(_mapper.Map<CourseDTO>(course));
        }


        [HttpPost]
        public IActionResult CreateCourseForAuthor([FromRoute]Guid authorId, CreateCourseDTO course)
        {
            if (!_courseLibrary.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseToMap = _mapper.Map<Course>(course);
            _courseLibrary.AddCourse(authorId, courseToMap);
            _courseLibrary.Save();

            var courseToReturn = _mapper.Map<CourseDTO>(courseToMap);
            return CreatedAtRoute("GetCourseForAuthor",new { authorId = authorId, courseId = courseToReturn.Id}
                                    , courseToReturn);
        }



        [HttpDelete("{courseId}")]
        public ActionResult Delete(Guid authorId, Guid CourseId)
        {
            if (!_courseLibrary.AuthorExists(authorId))
            {
                return NotFound();
            }

            var course = _courseLibrary.GetCourse(authorId, CourseId);
            if (course == null)
            {
                return NotFound();
            }
            _courseLibrary.DeleteCourse(course);
            _courseLibrary.Save();

            return NoContent();
        }









    }
}
