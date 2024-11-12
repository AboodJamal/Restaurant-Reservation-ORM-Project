using RestaurantReservation.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation
{
    public class SeedingMethods
    {
        public static async Task SeedDatabaseIfEmpty(RestaurantReservationDbContext context)
        {
            if (!context.Customers.Any())
            {
                context.Customers.AddRange(
                    new Customer { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890" },
                    new Customer { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "0987654321" }
                );

                context.Restaurants.AddRange(
                    new Restaurant { Name = "Restaurant A", Address = "Location A", PhoneNumber = "111-222-3333", OpeningHours = "9AM - 9PM" },
                    new Restaurant { Name = "Restaurant B", Address = "Location B", PhoneNumber = "444-555-6666", OpeningHours = "10AM - 10PM" }
                );

                context.Tables.AddRange(
                    new Table { Capacity = 4, RestaurantId = 1 },
                    new Table { Capacity = 6, RestaurantId = 2 }
                );

                context.Employees.AddRange(
                    new Employee { FirstName = "Mark", LastName = "Jones", Position = "Manager", RestaurantId = 1 },
                    new Employee { FirstName = "Sara", LastName = "Taylor", Position = "Waiter", RestaurantId = 2 }
                );

                context.MenuItems.AddRange(
                    new MenuItem { Name = "Burger", Description = "Delicious beef burger", Price = 5.99M, RestaurantId = 1 },
                    new MenuItem { Name = "Pizza", Description = "Cheese pizza", Price = 7.99M, RestaurantId = 2 }
                );

                context.SaveChanges();
            }
        }

    }
}
