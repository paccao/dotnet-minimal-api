namespace MinimalApi;
public class BookApi {
    public BookApi() {

    }

    public void Register(WebApplication app)
    {
        app.MapGet("/book", Get);
        app.MapGet("/book/{id}", GetById);
        app.MapPost("/book", Post);
        app.MapPut("/book/{id}", Put);
        app.MapDelete("/book/{id}", Delete);

    }

    void Get<IResult>(SortingOption sortingOption = SortingOption.Default)
    {
        switch (sortingOption)
        {
            case SortingOption.Ascending:
                return Results.Ok(books.OrderBy(b => b.Id));
            case SortingOption.Descending:
                return Results.Ok(books.OrderByDescending(b => b.Id));
            default:
                return Results.Ok(books);
        }
    }

    void GetById<IResult>(int id)
    {
        var book = books.Find(b => b.Id == id);
        if(book is null) return Results.NotFound("This book does not exist.");

        return Results.Ok(book);
    }

    void Post<IResult>(Book book)
    {
        if (book is null) return Results.BadRequest("Missing details of book to add.");
        var bookExists = books.Find((b) => b.Id == book.Id);
        if (bookExists != null) return Results.BadRequest("The Id you specified already exists.");

        books.Add(book);

        return Results.Ok("Book was added successfully.");
    }

    void Put<IResult>(Book updatedBook, int id)
    {
        var book = books.Find(b => b.Id == id);
        if(book is null) return Results.NotFound("This book does not exist.");

        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;
        return Results.Ok(book);
    }

    void Delete<IResult>(int id)
    {
        var book = books.Find(b => b.Id == id);
        if(book is null) return Results.NotFound("This book does not exist.");
        books.Remove(book);
        return Results.Ok(book);
    }
}
