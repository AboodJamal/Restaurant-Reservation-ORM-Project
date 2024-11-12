using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;

namespace RestaurantReservation.Services
{
    public class ReservationService
    {
        public static async Task CreateReservation(RestaurantReservationDbContext context, Reservation reservation)
        {
            context.Reservations.Add(reservation);
            await context.SaveChangesAsync();
            Console.WriteLine("Reservation created successfully.");
        }

        public static async Task UpdateReservation(RestaurantReservationDbContext context, int reservationId, Reservation updatedReservation)
        {
            var reservation = await context.Reservations.FindAsync(reservationId);
            if (reservation != null)
            {
                reservation.ReservationDate = updatedReservation.ReservationDate;
                reservation.PartySize = updatedReservation.PartySize;
                reservation.CustomerId = updatedReservation.CustomerId;
                reservation.RestaurantId = updatedReservation.RestaurantId;
                reservation.TableId = updatedReservation.TableId;
                await context.SaveChangesAsync();
                Console.WriteLine("Reservation updated successfully.");
            }
            else
            {
                Console.WriteLine("Reservation not found.");
            }
        }

        public static async Task DeleteReservation(RestaurantReservationDbContext context, int reservationId)
        {
            var reservation = await context.Reservations.FindAsync(reservationId);
            if (reservation != null)
            {
                context.Reservations.Remove(reservation);
                await context.SaveChangesAsync();
                Console.WriteLine("Reservation deleted successfully.");
            }
            else
            {
                Console.WriteLine("Reservation not found.");
            }
        }
    }
}
