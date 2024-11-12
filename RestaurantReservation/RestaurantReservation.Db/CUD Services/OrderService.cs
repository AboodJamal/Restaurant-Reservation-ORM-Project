using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;

namespace RestaurantReservation.Services
{
    public class OrderService
    {
        public static async Task CreateOrder(RestaurantReservationDbContext context, Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            Console.WriteLine("Order created successfully.");
        }

        public static async Task UpdateOrder(RestaurantReservationDbContext context, int orderId, Order updatedOrder)
        {
            var order = await context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.OrderDate = updatedOrder.OrderDate;
                order.TotalAmount = updatedOrder.TotalAmount;
                order.EmployeeId = updatedOrder.EmployeeId;
                await context.SaveChangesAsync();
                Console.WriteLine("Order updated successfully.");
            }
            else
            {
                Console.WriteLine("Order not found.");
            }
        }

        public static async Task DeleteOrder(RestaurantReservationDbContext context, int orderId)
        {
            var order = await context.Orders.FindAsync(orderId);
            if (order != null)
            {
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
                Console.WriteLine("Order deleted successfully.");
            }
            else
            {
                Console.WriteLine("Order not found.");
            }
        }

    }
}
