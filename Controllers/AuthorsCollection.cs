using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using CourseLibray.Api.Helpers;
using CourseLibray.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibray.Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class AuthorsCollection : ControllerBase
    {
        private ICourseLibraryRepository _courseLibrary;
        private IMapper _mapper;

        public AuthorsCollection(ICourseLibraryRepository courseLibrary, IMapper mapper)
        {
            _courseLibrary = courseLibrary ?? throw new ArgumentNullException(nameof(_courseLibrary));
            _mapper = mapper;
        }




        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetAllCourses()
        {
            return _courseLibrary.GetAllCourses().ToList();
        }



        [HttpPut]
        [Route("{id}")]
        public ActionResult EditCourse(Guid id, CreateCourseDTO course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (id == null)
            {
                return NotFound();
            }

            var UpdatedCourse = _mapper.Map<Course>(course);
            _courseLibrary.UpdateCourse(id,UpdatedCourse);

            return RedirectToAction("GetAllCourses");
       
        }
































            /// <summary>
            /// //authors collection 
            /// </summary>
            /// <param name="ids"></param>
            /// <returns></returns>


            [HttpGet("({ids})", Name = "GetAuthorCollection")]
        public ActionResult<IEnumerable<AuthorDTO>> GetAuthorCollection([FromRoute]
                            [ ModelBinder(BinderType = typeof(ArrayModelBuilder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var authorEntities = _courseLibrary.GetAuthors(ids);

            if (ids.Count() != authorEntities.Count())
            {
                return NotFound();
            }

            var authorsToReturn = _mapper.Map<IEnumerable<AuthorDTO>>(authorEntities);

            return Ok(authorsToReturn);


        }





        [HttpPost]
        public ActionResult<IEnumerable<AuthorDTO>> CreateAuthorCollection(IEnumerable<CreateAuthorDTO> collection)
        {
            var entities = _mapper.Map<IEnumerable<Author>>(collection);
            foreach (var author in entities)
            {
                _courseLibrary.AddAuthor(author);
            }

            _courseLibrary.Save();

            var authorCollectionToReturn = _mapper.Map<IEnumerable<AuthorDTO>>(entities);
            var idsAsString = string.Join(",", authorCollectionToReturn.Select(a => a.Id));
            return CreatedAtRoute("GetAuthorCollection",
             new { ids = idsAsString },
             authorCollectionToReturn);
        }


        [HttpOptions]
        public IActionResult GetAuthorOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }


        [HttpDelete("{authorId}")]
        public ActionResult DeleteAuthor(Guid authorId)
        {
            if (authorId == null)
            {
                return NotFound();
            }

            var author = _courseLibrary.GetAuthor(authorId);

            if (author == null)
                return NotFound();

            _courseLibrary.DeleteAuthor(author);
            _courseLibrary.Save();

            return NoContent();
        }













    }
}
