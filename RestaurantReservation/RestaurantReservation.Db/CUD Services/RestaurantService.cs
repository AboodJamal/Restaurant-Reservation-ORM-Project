using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;

namespace RestaurantReservation.Services
{
    public class RestaurantService
    {
        public static async Task CreateRestaurant(RestaurantReservationDbContext context, Restaurant restaurant)
        {
            context.Restaurants.Add(restaurant);
            await context.SaveChangesAsync();
            Console.WriteLine("Restaurant created successfully.");
        }

        public static async Task UpdateRestaurant(RestaurantReservationDbContext context, int restaurantId, Restaurant updatedRestaurant)
        {
            var restaurant = await context.Restaurants.FindAsync(restaurantId);
            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Address = updatedRestaurant.Address;
                restaurant.PhoneNumber = updatedRestaurant.PhoneNumber;
                restaurant.OpeningHours = updatedRestaurant.OpeningHours;
                await context.SaveChangesAsync();
                Console.WriteLine("Restaurant updated successfully.");
            }
            else
            {
                Console.WriteLine("Restaurant not found.");
            }
        }

        public static async Task DeleteRestaurant(RestaurantReservationDbContext context, int restaurantId)
        {
            var restaurant = await context.Restaurants.FindAsync(restaurantId);
            if (restaurant != null)
            {
                context.Restaurants.Remove(restaurant);
                await context.SaveChangesAsync();
                Console.WriteLine("Restaurant deleted successfully.");
            }
            else
            {
                Console.WriteLine("Restaurant not found.");
            }
        }
    }
}
