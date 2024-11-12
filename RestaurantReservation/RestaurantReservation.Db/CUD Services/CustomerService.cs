using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;

namespace RestaurantReservation.Services
{
    public class CustomerService
    {
        public static async Task CreateCustomer(RestaurantReservationDbContext context, Customer customer)
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
            Console.WriteLine("Customer created successfully.");
        }

        public static async Task UpdateCustomer(RestaurantReservationDbContext context, int customerId, Customer updatedCustomer)
        {
            var customer = await context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                customer.FirstName = updatedCustomer.FirstName;
                customer.LastName = updatedCustomer.LastName;
                customer.Email = updatedCustomer.Email;
                customer.PhoneNumber = updatedCustomer.PhoneNumber;
                await context.SaveChangesAsync();
                Console.WriteLine("Customer updated successfully.");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
        public static async Task DeleteCustomer(RestaurantReservationDbContext context, int customerId)
        {
            var customer = await context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                context.Customers.Remove(customer);
                await context.SaveChangesAsync();
                Console.WriteLine("Customer deleted successfully.");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
    }
}
