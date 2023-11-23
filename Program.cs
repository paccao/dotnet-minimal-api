var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var books = new List<Book> {
    new Book { Id = 1, Title = "The hitchhiker's Guide to the Galaxy", Author = "Douglas Adams"},
    new Book { Id = 2, Title = "1984", Author = "George Orwell"},
    new Book { Id = 3, Title = "Ready Player One", Author ="Ernest Cline"},
    new Book { Id = 4, Title = "The Martian", Author = "Andy Weir"},
};

app.MapGet("/book", () => {
    return books.OrderBy(b => b.Id);
});

app.MapGet("/book/{id}", (int id) => {
    var book = books.Find(b => b.Id == id);
    if(book is null) return Results.NotFound("This book does not exist.");

    return Results.Ok(book);
});

app.MapPost("/book", (Book book) => {
    if (book is null) return Results.BadRequest("Missing details of book to add.");
    var bookExists = books.Find((b) => b.Id == book.Id);
    if (bookExists != null) return Results.BadRequest("The Id you specified already exists.");
    
    books.Add(book);
    
    return Results.Ok("Book was added successfully.");
});

app.MapPut("/book/{id}", (Book updatedBook, int id) => {
    var book = books.Find(b => b.Id == id);
    if(book is null) return Results.NotFound("This book does not exist.");

    book.Title = updatedBook.Title;
    book.Author = updatedBook.Author;
    return Results.Ok(book);
});

app.MapDelete("/book/{id}", (int id) => {
    var book = books.Find(b => b.Id == id);
    if(book is null) return Results.NotFound("This book does not exist.");

    books.Remove(book);
    return Results.Ok(book);
});

app.Run();

class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
}