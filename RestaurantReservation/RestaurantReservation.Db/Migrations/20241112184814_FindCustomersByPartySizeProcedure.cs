using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class FindCustomersByPartySizeProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS FindCustomersByPartySize;");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE FindCustomersByPartySize (@PartySize INT)
            AS
            BEGIN
                SELECT c.CustomerId, c.FirstName, c.LastName , c.Email , c.PhoneNumber
                FROM Customers c
                JOIN Reservations r ON c.CustomerId = r.CustomerId
                WHERE r.PartySize > @PartySize
            END
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS FindCustomersByPartySize;");
        }
    }
}
