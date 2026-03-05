using books.models;

namespace books.data;

public class Seeds
{
    public List<Book> Books { get; } = null!;
    public List<Category> Categories { get; } = null!;
    public List<Author> Authors { get; } = null!;
    public List<BookAuthor> BooksAuthors { get; } = null!;

    public Seeds()
    {
        Categories = new List<Category>()
        {
            new Category { ID = 1, Name = "Science Fiction" },
            new Category { ID = 2, Name = "Fantasy" },
            new Category { ID = 3, Name = "Mystery / Thriller" },
            new Category { ID = 4, Name = "Literary Fiction" },
            new Category { ID = 5, Name = "Historical Fiction" },
            new Category { ID = 6, Name = "Non-Fiction" },
            new Category { ID = 7, Name = "Young Adult" },
            new Category { ID = 8, Name = "Horror" }
        };

        Authors = new List<Author>()
        {
            new Author { ID = 1, FirstName = "Ursula K.", LastName = "Le Guin",  },
            new Author { ID = 2, FirstName = "Neil", LastName = "Gaiman",  },
            new Author { ID = 3, FirstName = "Brandon", LastName = "Sanderson",  },
            new Author { ID = 4, FirstName = "Tana", LastName = "French",  },
            new Author { ID = 5, FirstName = "Ted", LastName = "Chiang",  },
            new Author { ID = 6, FirstName = "Maggie", LastName = "O'Farrell",  },
            new Author { ID = 7, FirstName = "Andy", LastName = "Weir",  },
            new Author { ID = 8, FirstName = "Shirley", LastName = "Jackson",  }
        };

        Books = new List<Book>()
        {
            new Book { ID = 1, Title = "The Left Hand of Darkness", PublicationYear = 1969,CategoryId = 1 },
            new Book { ID = 2, Title = "Neverwhere", PublicationYear = 1996,CategoryId = 2 },
            new Book { ID = 3, Title = "The Way of Kings", PublicationYear = 2010,CategoryId = 2 },
            new Book { ID = 4, Title = "The Martian", PublicationYear = 2011,CategoryId = 1 },
            new Book { ID = 5, Title = "Exhalation", PublicationYear = 2019,CategoryId = 1 },
            new Book { ID = 6, Title = "The Likeness", PublicationYear = 2008,CategoryId = 3 },
            new Book { ID = 7, Title = "The Haunting of Hill House", PublicationYear = 1959,CategoryId = 8 },
            new Book { ID = 8, Title = "Hamnet", PublicationYear = 2020,CategoryId = 5 }
        };

        BooksAuthors = new List<BookAuthor>()
        {
            new BookAuthor { BookId = 1, AuthorId = 1 },
            new BookAuthor { BookId = 2, AuthorId = 2 },
            new BookAuthor { BookId = 3, AuthorId = 3 },
            new BookAuthor { BookId = 4, AuthorId = 7 },
            new BookAuthor { BookId = 5, AuthorId = 5 },
            new BookAuthor { BookId = 6, AuthorId = 4 },
            new BookAuthor { BookId = 7, AuthorId = 8 },
            new BookAuthor { BookId = 8, AuthorId = 6 },
        };
    }
}