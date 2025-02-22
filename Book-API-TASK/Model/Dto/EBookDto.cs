namespace Book_API_TASK.Model.Dto;

public class EBookDto
{
    
    private string title;
    
    private string author;
    
    private int publicationYear;
    
    public string Title
    {
        get => title;
        set => title = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public string AuthorNames
    {
        get => author;
        set => author = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public int PublicationYear
    {
        get => publicationYear;
        set => publicationYear = value;
    }

    public override string ToString()
    {
        return title + " - " + author + " - " + publicationYear;
    }
}