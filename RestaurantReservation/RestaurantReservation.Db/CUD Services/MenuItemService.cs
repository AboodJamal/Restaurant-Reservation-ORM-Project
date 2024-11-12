using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;

namespace RestaurantReservation.Services
{
    public class MenuItemService
    {
        public static async Task CreateMenuItem(RestaurantReservationDbContext context, MenuItem menuItem)
        {
            context.MenuItems.Add(menuItem);
            await context.SaveChangesAsync();
            Console.WriteLine("MenuItem created successfully.");
        }

        public static async Task UpdateMenuItem(RestaurantReservationDbContext context, int itemId, MenuItem updatedMenuItem)
        {
            var menuItem = await context.MenuItems.FindAsync(itemId);
            if (menuItem != null)
            {
                menuItem.Name = updatedMenuItem.Name;
                menuItem.Description = updatedMenuItem.Description;
                menuItem.Price = updatedMenuItem.Price;
                await context.SaveChangesAsync();
                Console.WriteLine("MenuItem updated successfully.");
            }
            else
            {
                Console.WriteLine("MenuItem not found.");
            }
        }

        public static async Task DeleteMenuItem(RestaurantReservationDbContext context, int itemId)
        {
            var menuItem = await context.MenuItems.FindAsync(itemId);
            if (menuItem != null)
            {
                context.MenuItems.Remove(menuItem);
                await context.SaveChangesAsync();
                Console.WriteLine("MenuItem deleted successfully.");
            }
            else
            {
                Console.WriteLine("MenuItem not found.");
            }
        }
    }
}
