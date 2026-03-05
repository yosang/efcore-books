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
        service.addBookAuthor(newEntry);

        // Get some books
        var books = service.getAllBooks();
        foreach (var b in books)
        {
            Console.WriteLine($"Book: {b.ID} - {b.Title}");
            Console.WriteLine($"Category {b.Category?.Name}");
            Console.Write($"Authors: ");
            foreach (var a in b.Authors)
            {
                Console.Write($"{a.FirstName} {a.LastName}");
                if (b.Authors.Count > 1) Console.Write(", ");
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}