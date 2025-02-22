using Book_API_TASK.Model.Entity;
using Book_API_TASK.Repository;

namespace Book_API_TASK.Service;

public class BookService
{
    private readonly BookRepository bookRepository;

    public BookService(BookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
    }


    public EBook? GetBookByName(string name)
    {
        return bookRepository.Ebooks.Find(name);
    }

    public bool ExistByName(string name)
    {
        return bookRepository.Ebooks
            .Any(e => e.Title.Equals(name));
    }

    public List<EBook> GetAllBooks()
    {
        return bookRepository.Ebooks
            .OrderBy(b => b.Title)
            .ToList();
    }

    public List<string> GetAllTitlesByPopularity()
    {
        return bookRepository.Ebooks
            .AsEnumerable()
            .OrderByDescending(b => CalculatePopularityScore(b))
            .Select(b => b.Title)
            .ToList();
    }

    private double CalculatePopularityScore(EBook ebook)
    {
        int differenceInYears = DateTime.Now.Year - ebook.PublicationYear;
        double result =  ebook.ViewsCount * 0.5 + differenceInYears * 2;
        Console.WriteLine(ebook.Title + " " + result);
        return result;
    }

    public dynamic AddBook(EBook eBook)
    {
        try
        {
            var entity = bookRepository.Ebooks.Add(eBook);
            bookRepository.SaveChanges();
            return entity;
        }
        catch (Exception ex)
        {
            return eBook.Title + " Already exists";
        }
    }

    public List<EBook> AddBooks(List<EBook> eBook)
    {
        List<EBook> newBooks = eBook.Where(b => !ExistByName(b.Title)).ToList();
        bookRepository.Ebooks.AddRange(newBooks);
        bookRepository.SaveChanges();
        return newBooks;
    }

    public dynamic Update(EBook eBook)
    {
        try
        {
            bookRepository.Ebooks.Update(eBook);
            bookRepository.SaveChanges();
            return null;
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return "Internal Server Error";
        }
    }
}