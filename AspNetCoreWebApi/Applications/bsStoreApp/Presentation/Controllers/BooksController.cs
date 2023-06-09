﻿using Entities.DataTransferObjects;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Conracts;

namespace Presentation.Controllers;

[ServiceFilter(typeof(LogFilterAttribute))]
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
    public async Task<IActionResult> GetAllBooksAsync()
    {
        var books = await _serviceManager.BookService.GetAllBooksAsync(false);
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
    {
        var book = await _serviceManager.BookService.GetOneBookByIdAsync(id, false);

        return Ok(book);
    }

    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost]
    public async Task<IActionResult> CreateOneBookAsync([FromBody] BookDtoForInsertion bookDto)
    {

        var book = await _serviceManager.BookService.CreateOneBookAsync(bookDto);

        return StatusCode(201, book);  //CreatedAtRoute()
    }

    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDtoForUpdate)
    {

        await _serviceManager.BookService.UpdateOneBookAsync(id, bookDtoForUpdate, false);

        return NoContent(); //204
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
    {
        await _serviceManager.BookService.DeleteOneBookAsync(id, false);

        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> PartiallyUpdateOneBookAsync([FromRoute(Name = "id")] int id,
        [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
    {

        if (bookPatch is null)
            return BadRequest();

        var result = await _serviceManager.BookService.GetOneBookForPatchAsync(id, false);

        bookPatch.ApplyTo(result.bookDtoForUpdate, ModelState);

        TryValidateModel(result.bookDtoForUpdate);

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _serviceManager.BookService.SaveChangesForPatchAsync(result.bookDtoForUpdate, result.book);

        return NoContent(); // 204
    }
}

