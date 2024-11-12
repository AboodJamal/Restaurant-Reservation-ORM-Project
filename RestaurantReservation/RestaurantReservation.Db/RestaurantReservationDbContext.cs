using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 
using System.Reflection.Emit;
using Microsoft.Data.SqlClient;

namespace RestaurantReservation.Db
{

    public class RestaurantReservationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<ReservationWithCustomerAndRestaurant> ReservationsWithCustomerAndRestaurant { get; set; }

        public DbSet<EmployeeWithRestaurantDetails> EmployeesWithRestaurantDetails { get; set; }

        public RestaurantReservationDbContext(DbContextOptions<RestaurantReservationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MenuItem>()
                .Property(m => m.Price)
                .HasPrecision(18, 2); 

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Restaurant)
                .WithMany(rest => rest.Reservations)
                .HasForeignKey(r => r.RestaurantId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Table)
                .WithMany(t => t.Reservations)
                .HasForeignKey(r => r.TableId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Reservation)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.ReservationId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(o => o.EmployeeId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.MenuItem)
                .WithMany(m => m.OrderItems)
                .HasForeignKey(oi => oi.ItemId);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Restaurant)
                .WithMany(r => r.Employees)
                .HasForeignKey(e => e.RestaurantId);

            modelBuilder.Entity<MenuItem>()
                .HasOne(m => m.Restaurant)
                .WithMany(r => r.MenuItems)
                .HasForeignKey(m => m.RestaurantId);

            modelBuilder.Entity<Table>()
                .HasOne(t => t.Restaurant)
                .WithMany(r => r.Tables)
                .HasForeignKey(t => t.RestaurantId);

            modelBuilder
        .Entity<ReservationWithCustomerAndRestaurant>()
        .HasNoKey() // Views don’t have a primary key
        .ToView("vw_ReservationsWithCustomerAndRestaurant");

            modelBuilder
    .Entity<EmployeeWithRestaurantDetails>()
    .HasNoKey()
    .ToView("vw_EmployeesWithRestaurantDetails");


            modelBuilder.Entity<Customer>().HasData(
        new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890" },
        new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "9876543210" },
        new Customer { CustomerId = 3, FirstName = "Robert", LastName = "Johnson", Email = "robert.johnson@example.com", PhoneNumber = "5555555555" },
        new Customer { CustomerId = 4, FirstName = "Emily", LastName = "Davis", Email = "emily.davis@example.com", PhoneNumber = "4444444444" },
        new Customer { CustomerId = 5, FirstName = "Michael", LastName = "Brown", Email = "michael.brown@example.com", PhoneNumber = "3333333333" }
    );

            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant { RestaurantId = 1, Name = "The Gourmet Kitchen", Address = "123 Food St, City", PhoneNumber = "1111111111", OpeningHours = "10 AM - 10 PM" },
                new Restaurant { RestaurantId = 2, Name = "Pizza Palace", Address = "456 Pizza Rd, City", PhoneNumber = "2222222222", OpeningHours = "11 AM - 11 PM" },
                new Restaurant { RestaurantId = 3, Name = "Sushi World", Address = "789 Sushi Ln, City", PhoneNumber = "3333333333", OpeningHours = "12 PM - 9 PM" },
                new Restaurant { RestaurantId = 4, Name = "Burger Joint", Address = "101 Burger Blvd, City", PhoneNumber = "4444444444", OpeningHours = "9 AM - 9 PM" },
                new Restaurant { RestaurantId = 5, Name = "Steakhouse", Address = "202 Meat Ave, City", PhoneNumber = "5555555555", OpeningHours = "1 PM - 10 PM" }
            );

            modelBuilder.Entity<Table>().HasData(
                new Table { TableId = 1, RestaurantId = 1, Capacity = 4 },
                new Table { TableId = 2, RestaurantId = 2, Capacity = 4 },
                new Table { TableId = 3, RestaurantId = 3, Capacity = 6 },
                new Table { TableId = 4, RestaurantId = 4, Capacity = 2 },
                new Table { TableId = 5, RestaurantId = 5, Capacity = 8 }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, RestaurantId = 1, FirstName = "Alice", LastName = "Williams", Position = "Manager" },
                new Employee { EmployeeId = 2, RestaurantId = 2, FirstName = "Bob", LastName = "Martinez", Position = "Waiter" },
                new Employee { EmployeeId = 3, RestaurantId = 3, FirstName = "Charlie", LastName = "Lee", Position = "Chef" },
                new Employee { EmployeeId = 4, RestaurantId = 4, FirstName = "David", LastName = "Wilson", Position = "Bartender" },
                new Employee { EmployeeId = 5, RestaurantId = 5, FirstName = "Eve", LastName = "Taylor", Position = "Manager" }
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { ReservationId = 1, CustomerId = 1, RestaurantId = 1, TableId = 1, ReservationDate = DateTime.Now.AddHours(-1), PartySize = 4 },
                new Reservation { ReservationId = 2, CustomerId = 2, RestaurantId = 2, TableId = 2, ReservationDate = DateTime.Now.AddHours(-2), PartySize = 2 },
                new Reservation { ReservationId = 3, CustomerId = 3, RestaurantId = 3, TableId = 3, ReservationDate = DateTime.Now.AddHours(-3), PartySize = 6 },
                new Reservation { ReservationId = 4, CustomerId = 4, RestaurantId = 4, TableId = 4, ReservationDate = DateTime.Now.AddHours(-4), PartySize = 2 },
                new Reservation { ReservationId = 5, CustomerId = 5, RestaurantId = 5, TableId = 5, ReservationDate = DateTime.Now.AddHours(-5), PartySize = 8 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, ReservationId = 1, EmployeeId = 1, OrderDate = DateTime.Now, TotalAmount = 50.00M },
                new Order { OrderId = 2, ReservationId = 2, EmployeeId = 2, OrderDate = DateTime.Now, TotalAmount = 35.50M },
                new Order { OrderId = 3, ReservationId = 3, EmployeeId = 3, OrderDate = DateTime.Now, TotalAmount = 75.00M },
                new Order { OrderId = 4, ReservationId = 4, EmployeeId = 4, OrderDate = DateTime.Now, TotalAmount = 28.25M },
                new Order { OrderId = 5, ReservationId = 5, EmployeeId = 5, OrderDate = DateTime.Now, TotalAmount = 90.00M }
            );

            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem { ItemId = 1, RestaurantId = 1, Name = "Gourmet Burger", Description = "Juicy beef patty with gourmet toppings", Price = 12.99M },
                new MenuItem { ItemId = 2, RestaurantId = 2, Name = "Margherita Pizza", Description = "Classic pizza with tomatoes and mozzarella", Price = 8.99M },
                new MenuItem { ItemId = 3, RestaurantId = 3, Name = "California Roll", Description = "Fresh sushi rolls with crab and avocado", Price = 10.99M },
                new MenuItem { ItemId = 4, RestaurantId = 4, Name = "Cheeseburger", Description = "Classic burger with melted cheese", Price = 9.99M },
                new MenuItem { ItemId = 5, RestaurantId = 5, Name = "Ribeye Steak", Description = "Succulent ribeye cooked to perfection", Price = 22.99M }
            );

            

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { OrderItemId = 1, OrderId = 1, ItemId = 1, Quantity = 2 },
                new OrderItem { OrderItemId = 2, OrderId = 2, ItemId = 2, Quantity = 1 },
                new OrderItem { OrderItemId = 3, OrderId = 3, ItemId = 3, Quantity = 3 },
                new OrderItem { OrderItemId = 4, OrderId = 4, ItemId = 4, Quantity = 2 },
                new OrderItem { OrderItemId = 5, OrderId = 5, ItemId = 5, Quantity = 1 }
            );

        }


    }


}
