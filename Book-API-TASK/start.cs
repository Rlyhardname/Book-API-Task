using Book_API_TASK.Model.Entity;
using Book_API_TASK.Model.Validation;
using Book_API_TASK.Repository;
using Book_API_TASK.Service;

var builder = WebApplication.CreateBuilder();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<EBookValidator>();
builder.Services.AddDbContext<BookRepository>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BookRepository>();

    // Drop database on start (if exists)
    dbContext.Database.EnsureDeleted();

    // Create fresh database with tables
    dbContext.Database.EnsureCreated();

    // Insert test data
    dbContext.Ebooks.Add(new EBook ( "Little Red Riding Hood", "Unknown",  502, 1052 ));
    dbContext.Ebooks.Add(new EBook ( "The Merry Adventures of Robin Hood", "Howard Pyle",  1883,5025 ));
    dbContext.Ebooks.Add(new EBook ( "Elon Musk: Tesla, SpaceX, and the quest for a fantastic future", "Ashlee Vance",  2015, 142342 ));
    dbContext.Ebooks.Add(new EBook ( "Clean Code", "Robert C. Martin",  2008,1337 ));
    dbContext.Ebooks.Add(new EBook ( "White Bim Black Ear", "Gavriil Troyepolsky",  1977, 55 ));
    dbContext.SaveChanges();
}

app.Lifetime.ApplicationStopping.Register(() =>
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<BookRepository>();
    dbContext.Database.EnsureDeleted(); // 🔴 Drop database on stop
});

app.UseAuthorization();
app.MapControllers();
app.Run();


