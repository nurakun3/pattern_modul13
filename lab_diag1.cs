using System;
using System.Collections.Generic;

namespace EcommerceOrderManagement
{
    public class Product
    {
        public string Name { get; set; }
        public int Stock { get; set; }
    }

    public class Order
    {
        public List<Product> Products { get; set; }
        public bool IsPaid { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsDelivered { get; set; }
        public string DeliveryMethod { get; set; } // "Courier" or "Pickup"
    }

    public class EcommerceSystem
    {
        private List<Product> inventory = new List<Product>();

        public EcommerceSystem()
        {
            inventory.Add(new Product { Name = "Laptop", Stock = 10 });
            inventory.Add(new Product { Name = "Phone", Stock = 20 });
            inventory.Add(new Product { Name = "Headphones", Stock = 15 });
        }

        public bool CheckStock(Order order)
        {
            foreach (var orderProduct in order.Products)
            {
                var inventoryProduct = inventory.Find(p => p.Name == orderProduct.Name);
                if (inventoryProduct == null || inventoryProduct.Stock < orderProduct.Stock)
                {
                    Console.WriteLine($"Product {orderProduct.Name} is out of stock.");
                    return false;
                }
            }

            Console.WriteLine("All products are in stock.");
            return true;
        }

        public void ProcessPayment(Order order)
        {
            if (order.PaymentMethod == "Online")
            {
                Console.WriteLine("Processing online payment...");
                // Simulating payment gateway response
                Random random = new Random();
                order.IsPaid = random.Next(0, 2) == 1;

                if (order.IsPaid)
                {
                    Console.WriteLine("Payment successful.");
                }
                else
                {
                    Console.WriteLine("Payment failed. Please retry.");
                }
            }
            else
            {
                Console.WriteLine("Payment will be made upon delivery.");
                order.IsPaid = true;
            }
        }

        public void AssembleOrder(Order order)
        {
            Console.WriteLine("Assembling order...");
            foreach (var orderProduct in order.Products)
            {
                var inventoryProduct = inventory.Find(p => p.Name == orderProduct.Name);
                if (inventoryProduct != null)
                {
                    inventoryProduct.Stock -= orderProduct.Stock;
                }
            }
            Console.WriteLine("Order assembled.");
        }

        public void DeliverOrder(Order order)
        {
            if (order.DeliveryMethod == "Courier")
            {
                Console.WriteLine("Courier service notified for delivery.");
            }
            else
            {
                Console.WriteLine("Order is ready for pickup. Notification sent to client.");
            }

            order.IsDelivered = true;
        }

        public void CompleteOrder(Order order)
        {
            if (order.IsDelivered)
            {
                Console.WriteLine("Order delivered successfully. Sending receipt to client...");
                Console.WriteLine("Order completed.");
            }
            else
            {
                Console.WriteLine("Order not delivered yet.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ecommerceSystem = new EcommerceSystem();

            var order = new Order
            {
                Products = new List<Product>
                {
                    new Product { Name = "Laptop", Stock = 1 },
                    new Product { Name = "Phone", Stock = 2 }
                },
                PaymentMethod = "Online",
                DeliveryMethod = "Courier"
            };

            Console.WriteLine("Starting order processing...");

            if (ecommerceSystem.CheckStock(order))
            {
                ecommerceSystem.ProcessPayment(order);
                if (order.IsPaid)
                {
                    ecommerceSystem.AssembleOrder(order);
                    ecommerceSystem.DeliverOrder(order);
                    ecommerceSystem.CompleteOrder(order);
                }
            }
            else
            {
                Console.WriteLine("Order cannot be processed due to insufficient stock.");
            }
        }
    }
}