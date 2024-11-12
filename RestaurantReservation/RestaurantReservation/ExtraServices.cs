using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation
{
    public class ExtraServices
    {
        public static async Task ListManagers(RestaurantReservationDbContext context)
        {
            var managers = await context.Employees
                                         .Where(e => e.Position.Equals("Manager", StringComparison.OrdinalIgnoreCase))
                                         .ToListAsync();

            if (managers.Any())
            {
                Console.WriteLine("Managers:");
                foreach (var manager in managers)
                {
                    Console.WriteLine($"ID: {manager.EmployeeId}, Name: {manager.FirstName} {manager.LastName}");
                }
            }
            else
            {
                Console.WriteLine("No managers found.");
            }
        }

        public static async Task GetReservationsByCustomer(RestaurantReservationDbContext context, int customerId)
        {
            var reservationsOfSpecificCustomer = await context.Reservations
                                            .Where(r => r.CustomerId == customerId)
                                            .ToListAsync();

            if (reservationsOfSpecificCustomer.Any())
            {
                Console.WriteLine($"Reservations for Customer ID: {customerId}");
                foreach (var reservation in reservationsOfSpecificCustomer)
                {
                    Console.WriteLine($"Reservation ID: {reservation.ReservationId}, Date: {reservation.ReservationDate}, Party Size: {reservation.PartySize}");
                }
            }
            else
            {
                Console.WriteLine("No reservations found for this customer.");
            }
        }
        public static async Task ListOrdersAndMenuItems(RestaurantReservationDbContext context, int reservationId)
        {
            var ordersOfSpecificReservation = await context.Orders
                                      .Where(o => o.ReservationId == reservationId)
                                      .Include(o => o.OrderItems) 
                                      .ThenInclude(oi => oi.MenuItem) 
                                      .ToListAsync();

            if (ordersOfSpecificReservation.Any())
            {
                Console.WriteLine($"Orders for Reservation ID: {reservationId}");
                foreach (var order in ordersOfSpecificReservation)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}, Total Amount: {order.TotalAmount}");
                    foreach (var orderItem in order.OrderItems)
                    {
                        Console.WriteLine($"  MenuItem: {orderItem.MenuItem.Name}, Quantity: {orderItem.Quantity}, Price: {orderItem.MenuItem.Price}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No orders found for this reservation.");
            }
        }



        public static async Task ListOrderedMenuItems(RestaurantReservationDbContext context, int reservationId)
        {
            var orderItemsOfSpecificReservation = await context.OrderItems
                                          .Where(oi => oi.Order.ReservationId == reservationId)
                                          .Include(oi => oi.MenuItem)
                                          .ToListAsync();

            if (orderItemsOfSpecificReservation.Any())
            {
                Console.WriteLine($"Menu items ordered for Reservation ID: {reservationId}");
                foreach (var orderItem in orderItemsOfSpecificReservation)
                {
                    Console.WriteLine($"MenuItem: {orderItem.MenuItem.Name}, Quantity: {orderItem.Quantity}, Price: {orderItem.MenuItem.Price}");
                }
            }
            else
            {
                Console.WriteLine("No menu items ordered for this reservation.");
            }
        }


        public static async Task CalculateAverageOrderAmount(RestaurantReservationDbContext context, int employeeId)
        {
            var ordersOfSpecificEmp = await context.Orders
                                      .Where(o => o.EmployeeId == employeeId)
                                      .ToListAsync();

            if (ordersOfSpecificEmp.Any())
            {
                var averageAmount = ordersOfSpecificEmp.Average(o => o.TotalAmount);
                Console.WriteLine($"Average order amount for Employee ID {employeeId}: {averageAmount:C2}");
            }
            else
            {
                Console.WriteLine("No orders found for this employee.");
            }
        }


    }
}
