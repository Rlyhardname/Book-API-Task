using Book_API_TASK.Model.Dto;

namespace Book_API_TASK.Model.Validation;

public class EBookValidator
{
    public void ValidateEBook(EBookDto eBookDto)
    {
        ValidateTitle(eBookDto.Title);
        ValidateAuthor(eBookDto.AuthorNames);
        ValidatePublicationYear(eBookDto.PublicationYear);
    }

    public List<EBookDto> ValidateEbooks(List<EBookDto> eBookDtos)
    {
        List<EBookDto> validEBooks = new List<EBookDto>();
        foreach (EBookDto eBookDto in eBookDtos)
        {
            try
            {
                ValidateEBook(eBookDto);
                validEBooks.Add(eBookDto);
            }
            catch (Exception ex)
            {

            }
        }
        
        return validEBooks;
    }

    public void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty");
        }

        if (title.Length < 2 || title.Length > 128)
        {
            throw new ArgumentException("Title must be between 2 and 128 characters");
        }
    }

    public void ValidateAuthor(string authorNames)
    {
        if (string.IsNullOrWhiteSpace(authorNames))
        {
            throw new ArgumentException("Author names cannot be empty");
        }

        if (authorNames.Length < 2 || authorNames.Length > 128)
        {
            throw new ArgumentException("Author names must be between 2 and 128 characters");
        }
    }

    public void ValidatePublicationYear(int year)
    {
        if (year > DateTime.Now.Year + 2)
        {
            throw new ArgumentException("Cannot add books with publication year greater than two years in the future");
        }

        if (year < 868)
        {
            throw new ArgumentException("Cannot add books older than the oldest book recorded in history");
        }
    }
}