using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation
{
    internal class Tests
    {
        public static async Task ListEmployees(RestaurantReservationDbContext context)
        {
            var employees = await context.Employees.ToListAsync();

            Console.WriteLine("Employees:");
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName}, Position: {employee.Position}");
            }
            Console.WriteLine();
        }

        public static async Task GetReservationsByCustomer(RestaurantReservationDbContext context, int customerId)
        {
            var reservations = await context.Reservations
                .Where(r => r.CustomerId == customerId)
                .Include(r => r.Restaurant)
                .Include(r => r.Table)
                .ToListAsync();

            Console.WriteLine($"Reservations for Customer {customerId}:");
            foreach (var reservation in reservations)
            {
                Console.WriteLine($"Reservation ID: {reservation.ReservationId}, Restaurant: {reservation.Restaurant.Name}, Table: {reservation.Table.Capacity} seats, Date: {reservation.ReservationDate}");
            }
            Console.WriteLine();
        }

        public static async Task ListOrdersAndMenuItems(RestaurantReservationDbContext context, int reservationId)
        {
            var orders = await context.Orders
                .Where(o => o.ReservationId == reservationId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();

            Console.WriteLine($"Orders and Menu Items for Reservation {reservationId}:");
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.OrderId}, Employee: {order.Employee.FirstName} {order.Employee.LastName}, Total Amount: {order.TotalAmount}");
                foreach (var orderItem in order.OrderItems)
                {
                    Console.WriteLine($"- Item: {orderItem.MenuItem.Name}, Quantity: {orderItem.Quantity}, Price: {orderItem.MenuItem.Price}");
                }
            }
            Console.WriteLine();
        }

        public static async Task ListOrderedMenuItems(RestaurantReservationDbContext context, int reservationId)
        {
            var orderedMenuItems = await context.OrderItems
                .Where(oi => oi.Order.ReservationId == reservationId)
                .Include(oi => oi.MenuItem)
                .ToListAsync();

            Console.WriteLine($"Menu Items ordered in Reservation {reservationId}:");
            foreach (var orderItem in orderedMenuItems)
            {
                Console.WriteLine($"- Item: {orderItem.MenuItem.Name}, Quantity: {orderItem.Quantity}, Price: {orderItem.MenuItem.Price}");
            }
            Console.WriteLine();
        }

        public static async Task CalculateAverageOrderAmount(RestaurantReservationDbContext context, int employeeId)
        {
            var averageAmount = await context.Orders
                .Where(o => o.EmployeeId == employeeId)
                .AverageAsync(o => o.TotalAmount);

            Console.WriteLine($"Average Order Amount for Employee {employeeId}: {averageAmount:C}");
            Console.WriteLine();
        }
    }
}
