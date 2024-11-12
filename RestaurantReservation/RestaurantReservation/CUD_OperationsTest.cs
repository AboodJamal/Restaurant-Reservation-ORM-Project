using RestaurantReservation.Db;
using RestaurantReservation.Db.CUD_Services;
using RestaurantReservation.Services;
using System;
using System.Threading.Tasks;

namespace RestaurantReservation
{
    public static class CUD_OperationsTest
    {
        public static async Task TestCustomerOperations(RestaurantReservationDbContext context)
        {
            var newCustomer = new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890"
            };
            await CustomerService.CreateCustomer(context, newCustomer);
            Console.WriteLine("Customer created");

            var updatedCustomer = new Customer
            {
                CustomerId = newCustomer.CustomerId, // Using the created CustomerId
                FirstName = "John",
                LastName = "Smith",
                Email = "john.smith@example.com",
                PhoneNumber = "987-654-3210"
            };
            await CustomerService.UpdateCustomer(context, updatedCustomer.CustomerId, updatedCustomer);
            Console.WriteLine("Customer updated");

            await CustomerService.DeleteCustomer(context, updatedCustomer.CustomerId);
            Console.WriteLine("Customer deleted");
        }

        public static async Task TestRestaurantOperations(RestaurantReservationDbContext context)
        {
            var newRestaurant = new Restaurant
            {
                Name = "Gourmet Kitchen",
                Address = "123 Food Street",
                PhoneNumber = "555-1234",
                OpeningHours = "10:00 AM - 10:00 PM"
            };
            await RestaurantService.CreateRestaurant(context, newRestaurant);
            Console.WriteLine("Restaurant created");

            var updatedRestaurant = new Restaurant
            {
                RestaurantId = newRestaurant.RestaurantId, 
                Name = "Gourmet Kitchen Deluxe",
                Address = "456 Dining Avenue",
                PhoneNumber = "555-5678",
                OpeningHours = "11:00 AM - 11:00 PM"
            };
            await RestaurantService.UpdateRestaurant(context, updatedRestaurant.RestaurantId, updatedRestaurant);
            Console.WriteLine("Restaurant updated");

            await RestaurantService.DeleteRestaurant(context, updatedRestaurant.RestaurantId);
            Console.WriteLine("Restaurant deleted");
        }

        public static async Task TestTableOperations(RestaurantReservationDbContext context)
        {
            var newTable = new Table
            {
                RestaurantId = 1, 
                Capacity = 4
            };
            await TableService.CreateTable(context, newTable);
            Console.WriteLine("Table created");

            var updatedTable = new Table
            {
                TableId = newTable.TableId, 
                RestaurantId = 1, 
                Capacity = 6
            };
            await TableService.UpdateTable(context, updatedTable.TableId, updatedTable);
            Console.WriteLine("Table updated");

            await TableService.DeleteTable(context, updatedTable.TableId);
            Console.WriteLine("Table deleted");
        }

        public static async Task TestMenuItemOperations(RestaurantReservationDbContext context)
        {
            var newMenuItem = new MenuItem
            {
                RestaurantId = 1, 
                Name = "Spaghetti Carbonara",
                Description = "Classic Italian pasta dish",
                Price = 12.99m
            };
            await MenuItemService.CreateMenuItem(context, newMenuItem);
            Console.WriteLine("MenuItem created");

            var updatedMenuItem = new MenuItem
            {
                ItemId = newMenuItem.ItemId, 
                RestaurantId = 1,
                Name = "Spaghetti Carbonara Deluxe",
                Description = "Classic Italian pasta with extra cheese",
                Price = 14.99m
            };
            await MenuItemService.UpdateMenuItem(context, updatedMenuItem.ItemId, updatedMenuItem);
            Console.WriteLine("MenuItem updated");

            await MenuItemService.DeleteMenuItem(context, updatedMenuItem.ItemId);
            Console.WriteLine("MenuItem deleted");
        }

        public static async Task TestReservationOperations(RestaurantReservationDbContext context)
        {
            var newReservation = new Reservation
            {
                CustomerId = 1, 
                RestaurantId = 1, 
                TableId = 1, 
                ReservationDate = DateTime.Now.AddDays(1),
                PartySize = 4
            };
            await ReservationService.CreateReservation(context, newReservation);
            Console.WriteLine("Reservation created");

            var updatedReservation = new Reservation
            {
                ReservationId = newReservation.ReservationId, 
                CustomerId = 1,
                RestaurantId = 1,
                TableId = 1,
                ReservationDate = DateTime.Now.AddDays(2),
                PartySize = 5
            };
            await ReservationService.UpdateReservation(context, updatedReservation.ReservationId, updatedReservation);
            Console.WriteLine("Reservation updated");

            await ReservationService.DeleteReservation(context, updatedReservation.ReservationId);
            Console.WriteLine("Reservation deleted");
        }

        public static async Task TestEmployeeOperations(RestaurantReservationDbContext context)
        {
            var newEmployee = new Employee
            {
                RestaurantId = 1, 
                FirstName = "Alice",
                LastName = "Johnson",
                Position = "Waiter"
            };
            await EmployeeService.CreateEmployee(context, newEmployee);
            Console.WriteLine("Employee created");

            var updatedEmployee = new Employee
            {
                EmployeeId = newEmployee.EmployeeId,
                RestaurantId = 1,
                FirstName = "Alice",
                LastName = "Smith",
                Position = "Manager"
            };
            await EmployeeService.UpdateEmployee(context, updatedEmployee.EmployeeId, updatedEmployee);
            Console.WriteLine("Employee updated");

            await EmployeeService.DeleteEmployee(context, updatedEmployee.EmployeeId);
            Console.WriteLine("Employee deleted");
        }

        public static async Task TestOrderItemOperations(RestaurantReservationDbContext context)
        {
            var newOrderItem = new OrderItem
            {
                OrderId = 1, 
                ItemId = 1, 
                Quantity = 2
            };
            await OrderItemService.CreateOrderItem(context, newOrderItem);
            Console.WriteLine("OrderItem created");

            var updatedOrderItem = new OrderItem
            {
                OrderItemId = newOrderItem.OrderItemId,
                OrderId = 1,
                ItemId = 1,
                Quantity = 3
            };
            await OrderItemService.UpdateOrderItem(context, updatedOrderItem.OrderItemId, updatedOrderItem);
            Console.WriteLine("OrderItem updated");

            await OrderItemService.DeleteOrderItem(context, updatedOrderItem.OrderItemId);
            Console.WriteLine("OrderItem deleted");
        }

        public static async Task TestOrderOperations(RestaurantReservationDbContext context)
        {
            var newOrder = new Order
            {
                ReservationId = 1, 
                EmployeeId = 1, 
                OrderDate = DateTime.Now,
                TotalAmount = 35.50m
            };
            await OrderService.CreateOrder(context, newOrder);
            Console.WriteLine("Order created");

            var updatedOrder = new Order
            {
                OrderId = newOrder.OrderId,
                ReservationId = 1,
                EmployeeId = 1,
                OrderDate = DateTime.Now.AddHours(1),
                TotalAmount = 40.00m
            };
            await OrderService.UpdateOrder(context, updatedOrder.OrderId, updatedOrder);
            Console.WriteLine("Order updated");

            await OrderService.DeleteOrder(context, updatedOrder.OrderId);
            Console.WriteLine("Order deleted");
        }
    }


}

