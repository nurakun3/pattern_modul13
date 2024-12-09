using System;
using System.Collections.Generic;

namespace OnlineCourseManagement
{
    public class Course
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
        public bool IsEnrolled { get; set; }
        public bool IsPaid { get; set; }
        public bool HasCompletedCourse { get; set; }
        public bool HasCertificate { get; set; }
    }

    public class OnlineCourseSystem
    {
        private List<Course> courses = new List<Course>();
        private List<User> users = new List<User>();

        public OnlineCourseSystem()
        {
            courses.Add(new Course { Name = "C# Programming", IsAvailable = true });
            courses.Add(new Course { Name = "Data Science", IsAvailable = false });
        }

        public void RegisterCourse(User user, string courseName)
        {
            var course = courses.Find(c => c.Name == courseName);
            if (course != null && course.IsAvailable)
            {
                user.IsEnrolled = true;
                Console.WriteLine($"User {user.Name} has been enrolled in the course: {courseName}");
            }
            else
            {
                Console.WriteLine($"Course {courseName} is not available.");
            }
        }

        public void ProcessPayment(User user, string paymentMethod)
        {
            if (!user.IsEnrolled)
            {
                Console.WriteLine("User is not enrolled in any course.");
                return;
            }

            Console.WriteLine($"Processing payment for {user.Name} using {paymentMethod}...");
            Random random = new Random();
            user.IsPaid = random.Next(0, 2) == 1;

            if (user.IsPaid)
            {
                Console.WriteLine("Payment successful. Access to the course has been granted.");
            }
            else
            {
                Console.WriteLine("Payment failed. Please retry.");
            }
        }

        public void StartCourse(User user)
        {
            if (!user.IsPaid)
            {
                Console.WriteLine("User has not paid for the course.");
                return;
            }

            Console.WriteLine($"{user.Name} has started the course.");
        }

        public void CompleteCourse(User user)
        {
            if (!user.IsPaid)
            {
                Console.WriteLine("User has not paid for the course.");
                return;
            }

            Console.WriteLine($"{user.Name} has completed the course.");
            user.HasCompletedCourse = true;
        }

        public void IssueCertificate(User user)
        {
            if (user.HasCompletedCourse)
            {
                user.HasCertificate = true;
                Console.WriteLine($"Certificate has been issued to {user.Name}.");
            }
            else
            {
                Console.WriteLine($"{user.Name} has not completed the course. Certificate cannot be issued.");
            }
        }

        public void SaveCompletionData(User user)
        {
            if (user.HasCertificate)
            {
                Console.WriteLine($"Completion data for {user.Name} has been saved to the database.");
            }
            else
            {
                Console.WriteLine("No certificate issued. Completion data not saved.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var courseSystem = new OnlineCourseSystem();
            var user = new User { Name = "Alice" };

            courseSystem.RegisterCourse(user, "C# Programming");
            courseSystem.ProcessPayment(user, "Credit Card");

            if (user.IsPaid)
            {
                courseSystem.StartCourse(user);
                courseSystem.CompleteCourse(user);
            }

            courseSystem.IssueCertificate(user);
            courseSystem.SaveCompletionData(user);
        }
    }
}