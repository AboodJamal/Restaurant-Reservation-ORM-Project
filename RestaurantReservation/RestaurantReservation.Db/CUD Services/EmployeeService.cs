using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;

namespace RestaurantReservation.Services
{
    public class EmployeeService
    {
        public static async Task CreateEmployee(RestaurantReservationDbContext context, Employee employee)
        {
            context.Employees.Add(employee);
            await context.SaveChangesAsync();
            Console.WriteLine("Employee created successfully.");
        }

        public static async Task UpdateEmployee(RestaurantReservationDbContext context, int employeeId, Employee updatedEmployee)
        {
            var employee = await context.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                employee.FirstName = updatedEmployee.FirstName;
                employee.LastName = updatedEmployee.LastName;
                employee.Position = updatedEmployee.Position;
                await context.SaveChangesAsync();
                Console.WriteLine("Employee updated successfully.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        public static async Task DeleteEmployee(RestaurantReservationDbContext context, int employeeId)
        {
            var employee = await context.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                await context.SaveChangesAsync();
                Console.WriteLine("Employee deleted successfully.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }
    }
}
