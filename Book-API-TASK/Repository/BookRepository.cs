using Book_API_TASK.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace Book_API_TASK.Repository;

public class BookRepository : DbContext
{

    /// <summary>
    ///  Switch with your server, usually localhost
    /// </summary>
    public static readonly string SERVER = "localhost\\SQLEXPRESS01";
    
    /// <summary>
    /// switch with your database name
    /// </summary>
    public static readonly string DATABASE_NAME = "BookDB";
    
    public DbSet<EBook> Ebooks { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer($"Server={SERVER};Database={DATABASE_NAME};Trusted_Connection=True;TrustServerCertificate=True;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EBook>()
            .HasKey(e => e.Title); 
    }
}