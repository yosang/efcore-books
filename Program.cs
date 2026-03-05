using books.context;
using books.models;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main()
    {
        using var ctx = new BookContext();

        var newEntry = new BookAuthor { BookId = 1, AuthorId = 2 };
        var exists = ctx.BookAuthor.Any(b => b.BookId == newEntry.BookId && b.AuthorId == newEntry.AuthorId);

        if (!exists)
        {
            ctx.BookAuthor.Add(newEntry);
            ctx.SaveChanges();
        }


        var books = ctx.Books.Include(e => e.Authors).Include(e => e.Category);
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