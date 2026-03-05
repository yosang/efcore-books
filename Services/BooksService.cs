using books.context;
using books.models;
using Microsoft.EntityFrameworkCore;

namespace books.services;

public class BookService
{
    private readonly BookContext _ctx;

    public BookService(BookContext ctx)
    {
        _ctx = ctx ?? throw new ArgumentNullException("DbContext is null");
    }

    // - Reading from the database

    // Books table
    public List<Book> getAllBooks()
    {

        try
        {
            return _ctx.Books.Include(e => e.Authors).Include(e => e.Category).ToList();
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine("Unable to save to the database", e.Message);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine("Something went wrong", e.Message);
            throw;
        }

    }
    // Categories table

    // ---- Inserting to the database

    // Books table

    // Categories table

    // BookAuthor table
    public void addBookAuthor(BookAuthor data)
    {
        try
        {
            // Check if the entry already exists
            var exists = _ctx.BookAuthor.Any(e => e.BookId == data.BookId && e.AuthorId == data.AuthorId);
            if (exists) throw new InvalidOperationException("This entry already exists");

            _ctx.BookAuthor.Add(data);
            _ctx.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine("Unable to update database", e.Message);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine("Something went wrong", e.Message);
            throw;
        }


    }
}