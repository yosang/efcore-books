using books.context;
using books.models;

public class Program
{
    public static void Main()
    {
        using var ctx = new BookContext();

        ctx.Database.EnsureCreated(); // For testing purposes, will convert to migrations

        ctx.Categories.AddRange(
            new Category() { Name = "Science Fiction" },
            new Category() { Name = "Roman" }
        );

        ctx.SaveChanges();
    }
}