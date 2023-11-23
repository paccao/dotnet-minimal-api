namespace MinimalApi;
public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
}

public enum SortingOption
{
    Default,
    Ascending,
    Descending
}

var books = new List<Book> {
    new Book { Id = 1, Title = "The hitchhiker's Guide to the Galaxy", Author = "Douglas Adams"},
    new Book { Id = 2, Title = "1984", Author = "George Orwell"},
    new Book { Id = 3, Title = "Ready Player One", Author ="Ernest Cline"},
    new Book { Id = 4, Title = "The Martian", Author = "Andy Weir"},
};