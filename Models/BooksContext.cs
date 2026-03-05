using books.models;
using books.data;
using Microsoft.EntityFrameworkCore;
namespace books.context;

public class BookContext : DbContext
{

    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookAuthor> BookAuthor { get; set; } // We are explictly setting this repository to insert new records in the join table

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     => optionsBuilder.UseMySQL("server=localhost;database=books;user=testuser;password=p@ssword");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuring 1-M relationship from the dependant side (Book)
        // We could also configure it instead from the principal (Category), using HasMany and WithOne
        modelBuilder.Entity<Book>()
            .HasOne(e => e.Category)
            .WithMany(e => e.Books)
            .HasForeignKey(e => e.CategoryId)
            .IsRequired(); // Marks this relation as required: Every book must have a category assigned

        // Configuring M-M relationship, this creates a joint table
        modelBuilder.Entity<Book>()
            .HasMany(e => e.Authors)
            .WithMany(e => e.Books)
            .UsingEntity<BookAuthor>(); // Here we are setting name for the M-M joint table

        modelBuilder.Entity<Category>()
            .Property(e => e.Name)
            .IsRequired();

        // Initial Seeds
        var seeds = new Seeds();

        modelBuilder.Entity<Category>().HasData(seeds.Categories);
        modelBuilder.Entity<Book>().HasData(seeds.Books);
        modelBuilder.Entity<Author>().HasData(seeds.Authors);
        modelBuilder.Entity<BookAuthor>().HasData(seeds.BooksAuthors);

    }
}