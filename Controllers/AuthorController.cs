using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using CourseLibray.Api.Helpers;
using CourseLibray.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibray.Api.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorController : ControllerBase
    {
        private ICourseLibraryRepository _courseLibrary;
        private IMapper _mapper;

        public AuthorController(ICourseLibraryRepository courseLibrary, IMapper mapper)
        {
            _courseLibrary = courseLibrary ?? throw new ArgumentNullException(nameof(_courseLibrary));
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<AuthorDTO>> GetAuthors()
        {
            var authors = _courseLibrary.GetAuthors();
            return Ok(_mapper.Map<IEnumerable<AuthorDTO>>(authors));
        }



        [HttpGet("{authorId}", Name ="GetAuthor")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var author = _courseLibrary.GetAuthor(authorId);
            

            if (author is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AuthorDTO>(author));
        }


        [HttpPost]
        public IActionResult CreateAuthor(CreateAuthorDTO authorRequest)
        {
            var author = _mapper.Map<Author>(authorRequest);
            _courseLibrary.AddAuthor(author);
            _courseLibrary.Save();

            var authorToReturn = _mapper.Map<AuthorDTO>(author);
            return CreatedAtRoute("GetAuthor",new { authorId = author.Id},authorToReturn);
        }







    }
}
