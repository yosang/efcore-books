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

    // Helper delegate method
    // Returns T (whatever the method using this has decided T is)
    // Takes one parameter cb of type Func<T> (Func<T> is a parameterless func that returns T)
    private T Db<T>(Func<T> cb)
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
            Console.WriteLine("Something went wrong: " + e.Message);
            throw;
        }
    }

    // - Reading from the database

    // Books table
    public List<Book> getAllBooks() => Db(() => _ctx.Books.Include(e => e.Authors).Include(e => e.Category).ToList());

    public Book getBookById(int id) => Db(() => _ctx.Books.Where(e => e.ID == id).Single());

    public Book? getOldestBook() => Db(() => _ctx.Books.OrderByDescending(e => e.PublicationYear).FirstOrDefault());

    public List<Book> getBookInYearRange(int min, int max) => Db(() => _ctx.Books.Where(e => e.PublicationYear > min && e.PublicationYear < max).ToList());

    public int getAveragePublicationYear() => Db(() => (int)_ctx.Books.Average(e => e.PublicationYear));

    public void addBook(Book entry) => Db(() =>
    {
        var exists = _ctx.Books.Any(e => e.Title.Trim().ToLower() == entry.Title.Trim().ToLower());

        if (exists) throw new InvalidOperationException("This book already exists");

        _ctx.Books.Add(entry);
        _ctx.SaveChanges();

        return true;
    });

    public void updateBook(int bookId, Book data) => Db(() =>
    {
        var book = _ctx.Books.Find(bookId);
        if (book == null) throw new InvalidOperationException($"Unable to find book with id: {bookId}");

        if (!string.IsNullOrWhiteSpace(data.Title)) book.Title = data.Title;
        if (data.PublicationYear != 0) book.PublicationYear = data.PublicationYear;
        if (data.CategoryId != 0) book.CategoryId = data.CategoryId;

        _ctx.SaveChanges();

        return true;
    });

    public void deleteBook(int bookId) => Db(() =>
    {
        var book = _ctx.Books.Find(bookId);
        if (book == null) throw new InvalidOperationException($"Unable to find book with id: {bookId}");

        _ctx.Books.Remove(book);
        _ctx.SaveChanges();

        return true;
    });

    // Categories table

    public List<Category> getAllCategories() => Db(() => _ctx.Categories.Include(e => e.Books).ToList());

    public Category? getCategoryById(int id) => Db(() => _ctx.Categories.Find(id));

    public void addCategory(Category entry) => Db(() =>
    {
        var exists = _ctx.Categories.Any(e => e.Name == entry.Name);
        if (exists) throw new InvalidOperationException("This category already exists");

        _ctx.Categories.Add(entry);
        _ctx.SaveChanges();

        return true;
    });

    public void updateCategory(int id, Category data) => Db(() =>
    {
        var cat = _ctx.Categories.Find(id);
        if (cat == null) throw new InvalidOperationException($"Unable to find category with id: {id}");

        if (!string.IsNullOrWhiteSpace(data.Name)) cat.Name = data.Name;

        _ctx.SaveChanges();

        return true;
    });

    public void deleteCategory(int id) => Db(() =>
    {
        var cat = _ctx.Categories.Find(id);
        if (cat == null) throw new InvalidOperationException($"Unable to find category with id: {id}");

        _ctx.Categories.Remove(cat);
        _ctx.SaveChanges();

        return true;
    });

    // Author table

    public List<Author> getAllAuthors() => Db(() => _ctx.Authors.Include(e => e.Books).ToList());
    public Author? getAuthorById(int id) => Db(() => _ctx.Authors.Find(id));
    public List<Author> getAuthorsWhereFirstNameStartsWith(string pattern) => Db(() => _ctx.Authors.Where(e => EF.Functions.Like(e.FirstName, pattern + "%")).ToList());
    public List<Author> getAuthorsWhereLastNameEndWith(string pattern) => Db(() => _ctx.Authors.Where(e => EF.Functions.Like(e.LastName, "%" + pattern)).ToList());

    public Author addAuthor(Author entry) => Db(() =>
    {
        var exists = _ctx.Authors.Any(e => e.FirstName.Trim().ToLower() == entry.FirstName.Trim().ToLower()
                                            && e.LastName.Trim().ToLower() == entry.LastName.Trim().ToLower());

        if (exists) throw new InvalidOperationException("This author exists already");

        var newEntry = _ctx.Authors.Add(entry);
        _ctx.SaveChanges();

        return entry;
    });

    public void updateAuthor(int authorId, Author data) => Db(() =>
    {
        var author = _ctx.Authors.Find(authorId);
        if (author == null) throw new InvalidOperationException($"Unable to find author with id {authorId}");


        if (!string.IsNullOrWhiteSpace(data.FirstName)) author.FirstName = data.FirstName;
        if (!string.IsNullOrWhiteSpace(data.LastName)) author.LastName = data.LastName;

        _ctx.SaveChanges();
        return true;
    });
    public void deleteAuthor(int authorId) => Db(() =>
    {
        var author = _ctx.Authors.Find(authorId);
        if (author == null) throw new InvalidOperationException($"Unable to find author with id {authorId}");

        _ctx.Authors.Remove(author);
        _ctx.SaveChanges();

        return true;
    });

    // BookAuthor table
    public void addBookAuthor(BookAuthor entry) => Db(() =>
        {
            // Check if the entry already exists
            var exists = _ctx.BookAuthor.Any(e => e.BookId == entry.BookId && e.AuthorId == entry.AuthorId);
            if (exists) throw new InvalidOperationException("This entry already exists");

            _ctx.BookAuthor.Add(entry);
            _ctx.SaveChanges();

            return true;
        });

}