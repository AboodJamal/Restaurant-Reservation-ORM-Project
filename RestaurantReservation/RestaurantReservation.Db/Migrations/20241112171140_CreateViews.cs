using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class CreateViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        CREATE VIEW vw_ReservationsWithCustomerAndRestaurant AS
        SELECT 
            r.ReservationId,
            r.ReservationDate,
            r.PartySize,
            c.CustomerId,
            c.FirstName AS CustomerFirstName,
            c.LastName AS CustomerLastName,
            c.Email AS CustomerEmail,
            rest.RestaurantId,
            rest.Name AS RestaurantName,
            rest.Address AS RestaurantAddress
        FROM Reservations r
        JOIN Customers c ON r.CustomerId = c.CustomerId
        JOIN Restaurants rest ON r.RestaurantId = rest.RestaurantId;
    ");

            migrationBuilder.Sql(@"
        CREATE VIEW vw_EmployeesWithRestaurantDetails AS
        SELECT 
            e.EmployeeId,
            e.FirstName AS EmployeeFirstName,
            e.LastName AS EmployeeLastName,
            e.Position,
            rest.RestaurantId,
            rest.Name AS RestaurantName,
            rest.Address AS RestaurantAddress,
            rest.PhoneNumber AS RestaurantPhoneNumber
        FROM Employees e
        JOIN Restaurants rest ON e.RestaurantId = rest.RestaurantId;
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS vw_ReservationsWithCustomerAndRestaurant;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS vw_EmployeesWithRestaurantDetails;");
        }
    }
}
