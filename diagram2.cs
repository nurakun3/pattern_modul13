using System;
using System.Collections.Generic;

namespace EventBookingSystem
{
    public class Venue
    {
        public string Name { get; set; }
        public List<string> AvailableDates { get; set; }
    }

    public class Booking
    {
        public string ClientName { get; set; }
        public string Venue { get; set; }
        public string Date { get; set; }
        public bool IsPaid { get; set; }
        public bool IsConfirmed { get; set; }
    }

    public class Task
    {
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class EventBookingSystem
    {
        private List<Venue> venues = new List<Venue>();
        private List<Booking> bookings = new List<Booking>();
        private List<Task> tasks = new List<Task>();

        public EventBookingSystem()
        {
            venues.Add(new Venue
            {
                Name = "Hall A",
                AvailableDates = new List<string> { "2024-12-01", "2024-12-05" }
            });

            venues.Add(new Venue
            {
                Name = "Hall B",
                AvailableDates = new List<string> { "2024-12-10", "2024-12-15" }
            });
        }

        public bool CheckVenueAvailability(string venue, string date)
        {
            foreach (var v in venues)
            {
                if (v.Name == venue && v.AvailableDates.Contains(date))
                {
                    Console.WriteLine($"Venue {venue} is available on {date}.");
                    return true;
                }
            }

            Console.WriteLine($"Venue {venue} is not available on {date}.");
            return false;
        }

        public void CreateBooking(string clientName, string venue, string date)
        {
            if (!CheckVenueAvailability(venue, date)) return;

            var booking = new Booking
            {
                ClientName = clientName,
                Venue = venue,
                Date = date,
                IsPaid = false,
                IsConfirmed = false
            };

            bookings.Add(booking);
            Console.WriteLine($"Booking created for {clientName} at {venue} on {date}.");
        }

        public void ProcessPayment(string clientName)
        {
            foreach (var booking in bookings)
            {
                if (booking.ClientName == clientName && !booking.IsPaid)
                {
                    booking.IsPaid = true;
                    Console.WriteLine($"Payment processed for {clientName}.");
                    NotifyAdmin(booking.Venue);
                    return;
                }
            }

            Console.WriteLine($"No pending payment found for {clientName}.");
        }

        public void NotifyAdmin(string venue)
        {
            Console.WriteLine($"Admin notified for venue {venue} to prepare tasks.");
            PrepareTasks();
        }

        public void PrepareTasks()
        {
            tasks.Add(new Task { Description = "Decorations", IsCompleted = false });
            tasks.Add(new Task { Description = "Catering", IsCompleted = false });
            tasks.Add(new Task { Description = "Audio/Visual Setup", IsCompleted = false });
            Console.WriteLine("Tasks prepared for the event.");
        }

        public void CompleteTask(string description)
        {
            foreach (var task in tasks)
            {
                if (task.Description == description && !task.IsCompleted)
                {
                    task.IsCompleted = true;
                    Console.WriteLine($"Task '{description}' completed.");
                    return;
                }
            }

            Console.WriteLine($"Task '{description}' not found or already completed.");
        }

        public void ConfirmBooking(string clientName)
        {
            foreach (var booking in bookings)
            {
                if (booking.ClientName == clientName && booking.IsPaid)
                {
                    booking.IsConfirmed = true;
                    Console.WriteLine($"Booking for {clientName} is confirmed.");
                    return;
                }
            }

            Console.WriteLine($"Booking for {clientName} could not be confirmed.");
        }

        public void RequestFeedback(string clientName)
        {
            Console.WriteLine($"Feedback request sent to {clientName}.");
        }

        public void CollectFeedback(string clientName, string feedback)
        {
            Console.WriteLine($"Feedback from {clientName}: {feedback}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var bookingSystem = new EventBookingSystem();

            bookingSystem.CreateBooking("Alice", "Hall A", "2024-12-01");
            bookingSystem.ProcessPayment("Alice");
            bookingSystem.CompleteTask("Decorations");
            bookingSystem.CompleteTask("Catering");
            bookingSystem.ConfirmBooking("Alice");
            bookingSystem.RequestFeedback("Alice");
            bookingSystem.CollectFeedback("Alice", "Great service!");
        }
    }
}