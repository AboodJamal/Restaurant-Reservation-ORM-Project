using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestaurantReservation.Db;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())  
            .AddJsonFile("appsettings.json")  
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<RestaurantReservationDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        using (var context = new RestaurantReservationDbContext(optionsBuilder.Options))
        {
            context.Database.Migrate(); 
        }

        Console.WriteLine("Database operations completed.");
    }
}
