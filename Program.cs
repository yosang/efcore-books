using books.context;
using books.models;
using books.services;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main()
    {
        // Initializing BookService through Dependency Injection
        var service = new BookService(new BookContext());

        // Add a new BookAuthor relationship entry
        var newEntry = new BookAuthor { BookId = 1, AuthorId = 3 };
        // service.addBookAuthor(newEntry);

        // Get some books with its categories and all its authors
        var books = service.getAllBooks();
        // foreach (var b in books)
        // {
        //     Console.WriteLine($"Book: {b.ID} - {b.Title}");
        //     Console.WriteLine($"Category {b.Category?.Name}");
        //     Console.Write($"Authors: ");
        //     foreach (var a in b.Authors)
        //     {
        //         Console.Write($"{a.FirstName} {a.LastName}");
        //         if (b.Authors.Count > 1) Console.Write(", ");
        //     }
        //     Console.WriteLine("");
        //     Console.WriteLine("");
        // }

        // Get a book by its ID
        // var book = service.getBookById(1);
        // Console.WriteLine($"{book.ID} - {book.Title} - {book.PublicationYear}");

        // Get oldest book
        // var oldest = service.getOldestBook();
        // Console.WriteLine($"{oldest?.Title} - {oldest?.PublicationYear}");

        // Get books in year range
        // var betweenYears = service.getBookInYearRange(2000, 2020);
        // foreach (var b in betweenYears)
        // {
        //     Console.WriteLine($"{b.ID} - {b.Title} {b.PublicationYear}");
        // }

        // Get averagePublicationYear
        // var averagePublicationYear = service.getAveragePublicationYear();

        // Console.WriteLine(averagePublicationYear);

        // Add a new book and a new author, category is already added (non fiction id 6)
        var taraWestover = service.addAuthor(new Author { FirstName = "Tara", LastName = "Westover" });
        service.addBook(new Book() { Title = "Educated", CategoryId = 6, Authors = new List<Author> { taraWestover } });

        // We intentionally left publication year as 0 above, so we can update it now
    }
}