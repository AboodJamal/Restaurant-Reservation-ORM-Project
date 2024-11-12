using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.CUD_Services
{
    public class OrderItemService
    {
        public static async Task CreateOrderItem(RestaurantReservationDbContext context, OrderItem newOrderItem)
        {
            context.OrderItems.Add(newOrderItem);
            await context.SaveChangesAsync();
            Console.WriteLine("OrderItem created successfully.");
        }

        public static async Task UpdateOrderItem(RestaurantReservationDbContext context, int orderItemId, OrderItem updatedOrderItem)
        {
            var orderItem = await context.OrderItems.FindAsync(orderItemId);
            if (orderItem != null)
            {
                orderItem.OrderId = updatedOrderItem.OrderId;
                orderItem.ItemId = updatedOrderItem.ItemId;
                orderItem.Quantity = updatedOrderItem.Quantity;
                await context.SaveChangesAsync();
                Console.WriteLine("OrderItem updated successfully.");
            }
            else
            {
                Console.WriteLine("OrderItem not found.");
            }
        }

        public static async Task DeleteOrderItem(RestaurantReservationDbContext context, int orderItemId)
        {
            var orderItem = await context.OrderItems.FindAsync(orderItemId);
            if (orderItem != null)
            {
                context.OrderItems.Remove(orderItem);
                await context.SaveChangesAsync();
                Console.WriteLine("OrderItem deleted successfully.");
            }
            else
            {
                Console.WriteLine("OrderItem not found.");
            }
        }


    }
}
