using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Conracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public BooksController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _serviceManager.BookService.GetAllBooks(false);
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            var book = _serviceManager.BookService.GetOneBookById(id, false);

            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] BookDtoForInsertion bookDto)
        {
            if (bookDto is null)
                return BadRequest();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var book = _serviceManager.BookService.CreateOneBook(bookDto);

            return StatusCode(201, book);  //CreatedAtRoute()
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDtoForUpdate)
        {
            if (bookDtoForUpdate is null)
                return BadRequest();

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _serviceManager.BookService.UpdateOneBook(id, bookDtoForUpdate, false);

            return NoContent(); //204
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            _serviceManager.BookService.DeleteOneBook(id, false);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
        {

            if (bookPatch is null)
                return BadRequest();

            var result = _serviceManager.BookService.GetOneBookForPatch(id, false);

            bookPatch.ApplyTo(result.bookDtoForUpdate, ModelState);

            TryValidateModel(result.bookDtoForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _serviceManager.BookService.SaveChangesForPatch(result.bookDtoForUpdate, result.book);

            return NoContent(); // 204
        }
    }
}
