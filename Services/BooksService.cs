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

    // Helper delegate methods

    // Returns T (whatever the method using this has decided T is)
    // Takes one parameter cb of type Func<T> (Func<T> is a parameterless func that returns T)
    private T ExecuteDb<T>(Func<T> cb)
    {
        try
        {
            return cb();
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine("Database operation failed: " + e.Message);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine("Something went wrong" + e.Message);
            throw;
        }
    }

    private void ExecuteDbVoid(Action cb)
    {
        try
        {
            cb();
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine("Database operation failed: " + e.Message);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine("Something went wrong" + e.Message);
            throw;
        }
    }

    // - Reading from the database

    // Books table
    public List<Book> getAllBooks() => ExecuteDb(() => _ctx.Books.Include(e => e.Authors).Include(e => e.Category).ToList());

    public Book getBookById(int id) => ExecuteDb(() => _ctx.Books.Where(e => e.ID == id).Single());

    public Book? getOldestBook() => ExecuteDb(() => _ctx.Books.OrderByDescending(e => e.PublicationYear).FirstOrDefault());

    public List<Book> getBookInYearRange(int min, int max) => ExecuteDb(() => _ctx.Books.Where(e => e.PublicationYear > min && e.PublicationYear < max).ToList());

    public int getAveragePublicationYear() => ExecuteDb(() => (int)_ctx.Books.Average(e => e.PublicationYear));

    public void addBook(Book entry) => ExecuteDbVoid(() =>
    {
        var exists = _ctx.Books.Any(e => e.Title.Trim().ToLower() == entry.Title.Trim().ToLower());

        if (exists) throw new InvalidOperationException("This book already exists");

        _ctx.Books.Add(entry);
        _ctx.SaveChanges();
    });

    public void updateBook() { }
    public void deleteBook() { }

    // Categories table

    public void getAllCategories() { }

    public void getCategoryById(int id) { }
    public void getBooksByCategory() { }

    public void addCategory(Category entry) { }
    public void updateCategory() { }
    public void deleteCategory() { }

    // Author table

    public void getAllAuthors() { }
    public void getAuthorById(int id) { }
    public void getAuthorWhereFirstNameStartsWith() { }
    public void getAuthorWhereLastNameStartsWith() { }
    public void getBooksByAuthor() { }

    public Author addAuthor(Author entry) => ExecuteDb(() =>
    {
        var exists = _ctx.Authors.Any(e => e.FirstName.Trim().ToLower() == entry.FirstName.Trim().ToLower()
                                            && e.LastName.Trim().ToLower() == entry.LastName.Trim().ToLower());

        if (exists) throw new InvalidOperationException("This author exists already");

        var newEntry = _ctx.Authors.Add(entry);
        _ctx.SaveChanges();

        return entry;
    });

    public void updateAuthor() { }
    public void deleteAuthor() { }

    // BookAuthor table
    public void addBookAuthor(BookAuthor entry) => ExecuteDbVoid(() =>
        {
            // Check if the entry already exists
            var exists = _ctx.BookAuthor.Any(e => e.BookId == entry.BookId && e.AuthorId == entry.AuthorId);
            if (exists) throw new InvalidOperationException("This entry already exists");

            _ctx.BookAuthor.Add(entry);
            _ctx.SaveChanges();
        });

}