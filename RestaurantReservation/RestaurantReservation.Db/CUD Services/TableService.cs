using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;

namespace RestaurantReservation.Services
{
    public class TableService
    {
        public static async Task CreateTable(RestaurantReservationDbContext context, Table table)
        {
            context.Tables.Add(table);
            await context.SaveChangesAsync();
            Console.WriteLine("Table created successfully.");
        }

        public static async Task UpdateTable(RestaurantReservationDbContext context, int tableId, Table updatedTable)
        {
            var table = await context.Tables.FindAsync(tableId);
            if (table != null)
            {
                table.Capacity = updatedTable.Capacity;
                await context.SaveChangesAsync();
                Console.WriteLine("Table updated successfully.");
            }
            else
            {
                Console.WriteLine("Table not found.");
            }
        }

        public static async Task DeleteTable(RestaurantReservationDbContext context, int tableId)
        {
            var table = await context.Tables.FindAsync(tableId);
            if (table != null)
            {
                context.Tables.Remove(table);
                await context.SaveChangesAsync();
                Console.WriteLine("Table deleted successfully.");
            }
            else
            {
                Console.WriteLine("Table not found.");
            }
        }
    }
}
