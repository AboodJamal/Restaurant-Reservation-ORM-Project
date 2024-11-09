namespace RestaurantReservation.Db
{
    public class Customer
    {
        public int CustomerId { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }

    public class Reservation
    {
        public int ReservationId { get; set; } // Primary Key
        public int CustomerId { get; set; } // Foreign Key
        public int RestaurantId { get; set; } // Foreign Key
        public int TableId { get; set; } // Foreign Key
        public DateTime ReservationDate { get; set; }
        public int PartySize { get; set; }

        public Customer Customer { get; set; }
        public Restaurant Restaurant { get; set; }
        public Table Table { get; set; }

        public ICollection<Order> Orders { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; } // Primary Key
        public int ReservationId { get; set; } // Foreign Key
        public int EmployeeId { get; set; } // Foreign Key
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public Reservation Reservation { get; set; }
        public Employee Employee { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }

    public class Employee
    {
        public int EmployeeId { get; set; } // Primary Key
        public int RestaurantId { get; set; } // Foreign Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }

        public Restaurant Restaurant { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

    public class MenuItem
    {
        public int ItemId { get; set; } // Primary Key
        public int RestaurantId { get; set; } // Foreign Key
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Restaurant Restaurant { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }


    public class OrderItem
    {
        public int OrderItemId { get; set; } // Primary Key
        public int OrderId { get; set; } // Foreign Key
        public int ItemId { get; set; } // Foreign Key
        public int Quantity { get; set; }

        public Order Order { get; set; }
        public MenuItem MenuItem { get; set; }
    }

    public class Table
    {
        public int TableId { get; set; } // Primary Key
        public int RestaurantId { get; set; } // Foreign Key
        public int Capacity { get; set; }

        public Restaurant Restaurant { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }


    public class Restaurant
    {
        public int RestaurantId { get; set; } // Primary Key
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string OpeningHours { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
        public ICollection<Table> Tables { get; set; }
    }



}
