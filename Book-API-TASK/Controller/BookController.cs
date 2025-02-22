using Book_API_TASK.Model.Dto;
using Book_API_TASK.Model.Entity;
using Book_API_TASK.Model.Mapper;
using Book_API_TASK.Model.Validation;
using Book_API_TASK.Service;
using Microsoft.AspNetCore.Mvc;

namespace Book_API_TASK.controller;

[ApiController]
[Route("api/")]
public class BookController : ControllerBase
{
    private readonly BookService service;
    private readonly EBookValidator eBookValidator;

    public BookController(BookService service, EBookValidator eBookValidator)
    {
        this.service = service;
        this.eBookValidator = eBookValidator;
    }

    [HttpGet]
    [Route("book/{title}")]
    public IActionResult GetBookByName(string title)
    {
        try
        {
            eBookValidator.ValidateTitle(title);
            EBook? eBook = service.GetBookByName(title);
            if (eBook == null)
            {
                return NotFound();
            }

            return Ok(eBook);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("book/")]
    public List<EBook> GetBooks()
    {
        return service.GetAllBooks();
    }

    [HttpGet]
    [Route("book/popularity")]
    public List<string> GetBookTitlesByPopularity()
    {
        return service.GetAllTitlesByPopularity();
    }

    [HttpPost]
    [Route("book/")]
    public IActionResult AddBook(EBookDto eBookDto)
    {
        try
        {
            eBookValidator.ValidateTitle(eBookDto.Title);
            eBookValidator.ValidateAuthor(eBookDto.AuthorNames);
            eBookValidator.ValidatePublicationYear(eBookDto.PublicationYear);

            var result = service.AddBook(EBookMapper.MapToEBook(eBookDto));
            return result is string
                ? Conflict(result)
                : Created("Book added successfully", EBookMapper.MapToEBook(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("book/list")]
    public IActionResult AddBooks([FromBody] List<EBookDto> eBookDto)
    {
        List<EBookDto> validateEbooks = eBookValidator.ValidateEbooks(eBookDto);
        List<EBook> addedBooks = service.AddBooks(EBookMapper.MapToEBooks(validateEbooks));
        return Created("Books added successfully", addedBooks);
    }

    [HttpPut]
    [Route("book/")]
    public IActionResult UpdateBook(EBookDto eBookDto)
    {
        try
        {
            var exists = service.ExistByName(eBookDto.Title);
            if (!exists)
            {
                return NotFound($"Book with title {eBookDto.Title} not found");
            }

            string result = service.Update(EBookMapper.MapToEBook(eBookDto));
            return result is string
                ? Conflict(result)
                : Accepted("Book updated successfully", EBookMapper.MapToEBook(eBookDto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}