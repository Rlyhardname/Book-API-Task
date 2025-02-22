using Book_API_TASK.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace Book_API_TASK.Repository;

public class BookRepository : DbContext
{
    public DbSet<EBook> Ebooks { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=BookDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EBook>()
            .HasKey(e => e.Title); 
    }
}