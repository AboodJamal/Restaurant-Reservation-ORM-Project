using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestaurantReservation.Db;
using RestaurantReservation;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantReservation
{
    class Program
    {
        static async Task Main(string[] args)
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

                await SeedingMethods.SeedDatabaseIfEmpty(context);

                await Tests.ListEmployees(context);
                await Tests.GetReservationsByCustomer(context, 1);
                await Tests.ListOrdersAndMenuItems(context, 1);
                await Tests.ListOrderedMenuItems(context, 1);
                await Tests.CalculateAverageOrderAmount(context, 3);


                await CUD_OperationsTest.TestCustomerOperations(context);
                await CUD_OperationsTest.TestRestaurantOperations(context);
                await CUD_OperationsTest.TestTableOperations(context);
                await CUD_OperationsTest.TestMenuItemOperations(context);
                await CUD_OperationsTest.TestReservationOperations(context);


                await Tests.ListReservationsWithDetails(context);
                await Tests.ListEmployeesWithRestaurantDetails(context);

                var restaurantId = 1;
                decimal totalRevenue = await Tests.CalculateTotalRevenueForRestaurant(context,restaurantId); 
                Console.WriteLine($"Total Revenue for Restaurant ID {restaurantId}: {totalRevenue}");


            }

            Console.WriteLine("Database operations completed.");
        }

    }
}
