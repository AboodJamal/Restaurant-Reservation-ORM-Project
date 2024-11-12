using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RestaurantReservation.Db
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RestaurantReservationDbContext>
    {
        public RestaurantReservationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RestaurantReservationDbContext>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new RestaurantReservationDbContext(optionsBuilder.Options);
        }
    }
}
