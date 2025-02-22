using Book_API_TASK.Model.Dto;
using Book_API_TASK.Model.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Book_API_TASK.Model.Mapper;

public class EBookMapper
{
    public static EBook MapToEBook(EBookDto bookDto)
    {
        return new EBook(bookDto.Title,bookDto.AuthorNames, bookDto.PublicationYear, 0);
    }
    
    public static List<EBook> MapToEBooks(List<EBookDto> bookDto)
    {
        List<EBook> eBooks = new List<EBook>();
        foreach (EBookDto dto in bookDto)
        {
            eBooks.Add(MapToEBook(dto));
        }

        return eBooks;
    }

    public static EBook MapToEBook(EntityEntry<EBook> entityEntry)
    {
        return new EBook(entityEntry.Entity.Title,
            entityEntry.Entity.Author,
            entityEntry.Entity.PublicationYear,
            entityEntry.Entity.ViewsCount);
    }
    
}