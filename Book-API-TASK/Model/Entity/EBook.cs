using System.ComponentModel.DataAnnotations;

namespace Book_API_TASK.Model.Entity;

public class EBook
{
    [Key]
    private string title;
    
    private string author;
    
    private int publicationYear;
    
    private int viewsCount;

    public EBook(string title, string author, int publicationYear, int viewsCount)
    {
        Title = title;
        Author = author;
        PublicationYear = publicationYear;
        ViewsCount = viewsCount;
    }

    public string Title
    {
        get => title;
        set => title = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Author
    {
        get => author;
        set => author = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int PublicationYear
    {
        get => publicationYear;
        set => publicationYear = value;
    }

    public int ViewsCount
    {
        get => viewsCount;
        set => viewsCount = value;
    }
}