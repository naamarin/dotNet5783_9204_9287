// See https://aka.ms/new-console-template for more information
//using Dal;
//using DalApi;
using DO;
using System.Diagnostics;

namespace DalTest
{
    public class program
    {
        static DalApi.IDal? dal = DalApi.Factory.Get();

        static void Main(string[] args)
        {
            double price;
            string action, category;
            int id, id2, inStock, amount, customerNumber;
            Order order;
            Product product;
            OrderItem orderItem;
            OrderItem? or;
            Console.WriteLine($@"Enter one of the options:
press 0 to exit
press 1 for Order
press 2 for Product
press 3 for OrderItem");
            string user_choice = Console.ReadLine();
            while (user_choice != "0")
            {
                try
                {
                    switch (user_choice)
                    {
                        case ("1"):
                            Console.WriteLine($@"Choose one of the following actions:
press 1 to Add an order
press 2 to View an existing order
press 3 to View all orders
press 4 to Update an existing order
press 5 to Delete an order");
                            action = Console.ReadLine();
                            switch (action)
                            {
                                case "1":
                                    order = new Order();
                                    Console.WriteLine("Enter your full name: ");
                                    order.CustomerName = Console.ReadLine();
                                    Console.WriteLine("Enter your number: ");
                                    int.TryParse(Console.ReadLine(), out customerNumber);
                                    order.CustomerNumber = customerNumber;
                                    Console.WriteLine("Enter your email: ");
                                    order.CustomerEmail = Console.ReadLine();
                                    Console.WriteLine("Enter your address: ");
                                    order.CustomerAdress = Console.ReadLine();
                                    order.OrderDate = DateTime.Now;
                                    id = dal?.Order.Add(order) ?? throw new NullReferenceException("id does not exist"); 
                                    Console.WriteLine($@"Your order ID is: {id}");
                                    break;
                                case "2":
                                    Console.WriteLine("Enter order ID: ");
                                    int.TryParse(Console.ReadLine(), out id);
                                    order = dal?.Order.GetById(id) ?? throw new NullReferenceException("id does not exist"); 
                                    Console.WriteLine(order);
                                    break;
                                case "3":
                                    IEnumerable<Order?> list = dal?.Order.GetAll() ?? throw new NullReferenceException("empty list");
                                    foreach (var item in list)
                                    {
                                        Console.WriteLine(item);
                                    }
                                    break;
                                case "4":
                                    order = new Order();
                                    Console.WriteLine("Enter your order ID: ");
                                    if (int.TryParse(Console.ReadLine(), out id))
                                        order.ID = id;
                                    else
                                        throw new NullReferenceException("id does not exist");
                                    Console.WriteLine("Enter your full name: ");
                                    order.CustomerName = Console.ReadLine();
                                    Console.WriteLine("Enter your number: ");
                                    int.TryParse(Console.ReadLine(), out customerNumber);
                                    order.CustomerNumber = customerNumber;
                                    Console.WriteLine("Enter your email: ");
                                    order.CustomerEmail = Console.ReadLine();
                                    Console.WriteLine("Enter your address: ");
                                    order.CustomerAdress = Console.ReadLine();
                                    order.OrderDate = DateTime.Now;
                                    dal?.Order.Update(order);
                                    break;
                                case "5":
                                    Console.WriteLine("Enter order ID: ");
                                    if (!int.TryParse(Console.ReadLine(), out id))
                                        throw new NullReferenceException("id does not exist");
                                    dal?.Order.Delete(id);
                                    break;
                            };
                            break;
                        case ("2"):
                            Console.WriteLine($@"Choose one of the following actions:
press 1 to Add an Product
press 2 to View an existing product
press 3 to View all products
press 4 to Update an existing product
press 5 to Deleting an product");
                            action = Console.ReadLine();
                            switch (action)
                            {
                                case "1":
                                    product = new Product();
                                    Console.WriteLine("Enter product ID: ");
                                    if (int.TryParse(Console.ReadLine(), out id))
                                        product.ID = id;
                                    else
                                        throw new NullReferenceException("id does not valid");
                                    Console.WriteLine("Enter product name: ");
                                    product.Name = Console.ReadLine();
                                    Console.WriteLine("Enter product price: ");
                                    if (Double.TryParse(Console.ReadLine(), out price))
                                        product.Price = price;
                                    else
                                        throw new NullReferenceException("Price does not valid");
                                    Console.WriteLine("Enter product category: ");
                                    category = Console.ReadLine();
                                    product.Category = (Category)int.Parse(category);
                                    Console.WriteLine("Enter product amount in stock: ");
                                    if (int.TryParse(Console.ReadLine(), out inStock))
                                        product.InStock = inStock;
                                    else
                                        throw new NullReferenceException("Amount does not valid");
                                    id = dal?.Product.Add(product) ?? throw new NullReferenceException("id does not exist");
                                    break;
                                case "2":
                                    Console.WriteLine("Enter product ID: ");
                                    if (!int.TryParse(Console.ReadLine(), out id))
                                        throw new NullReferenceException("id does not valid");
                                    product = dal?.Product.GetById(id) ?? throw new NullReferenceException("id does not exist");
                                    Console.WriteLine(product);
                                    break;
                                case "3":
                                    IEnumerable<Product?> list = dal?.Product.GetAll() ?? throw new NullReferenceException("empty list");
                                    foreach (var item in list)
                                    {
                                        Console.WriteLine(item);
                                    }
                                    break;
                                case "4":
                                    product = new Product();
                                    Console.WriteLine("Enter product ID: ");
                                    if (int.TryParse(Console.ReadLine(), out id))
                                        product.ID = id;
                                    else
                                        throw new NullReferenceException("id does not valid");
                                    Console.WriteLine("Enter product name: ");
                                    product.Name = Console.ReadLine();
                                    Console.WriteLine("Enter product price: ");
                                    if (Double.TryParse(Console.ReadLine(), out price))
                                        product.Price = price;
                                    else
                                        throw new NullReferenceException("Price does not valid");
                                    Console.WriteLine("Enter product category: ");
                                    product.Category = (DO.Category)int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter product amount in stock: ");
                                    if (int.TryParse(Console.ReadLine(), out inStock))
                                        product.InStock = inStock;
                                    else
                                        throw new NullReferenceException("Amount does not valid");
                                    dal?.Product.Update(product);
                                    break;
                                case "5":
                                    Console.WriteLine("Enter order ID: ");
                                    if (!int.TryParse(Console.ReadLine(), out id))
                                        throw new NullReferenceException("id does not valid");
                                    dal?.Product.Delete(id);
                                    break;
                            };
                            break;
                        case ("3"):
                            Console.WriteLine($@"Choose one of the following actions:
press 1 to Add an OrderItem
press 2 to View an existing OrderItem
press 3 to View all OrderItem
press 4 to Update an existing OrderItem
press 5 to Deleting an OrderItem
press 6 to display all items belonging to the same order
press 7 to Find an item on the order");
                            action = Console.ReadLine();
                            switch (action)
                            {
                                case "1":
                                    orderItem = new OrderItem();
                                    Console.WriteLine("Enter order ID: ");
                                    if (int.TryParse(Console.ReadLine(), out id))
                                        orderItem.OrderID = id;
                                    else
                                        throw new NullReferenceException("order ID does not valid");
                                    Console.WriteLine("Enter product ID: ");
                                    if (int.TryParse(Console.ReadLine(), out id))
                                        orderItem.ProductID = id;
                                    else
                                        throw new NullReferenceException("product ID does not valid");
                                    Console.WriteLine("Enter amount of this product: ");
                                    if (int.TryParse(Console.ReadLine(), out amount))
                                        orderItem.Amount = amount;
                                    else
                                        throw new NullReferenceException("amount does not valid");
                                    product = dal?.Product.GetById(orderItem.ProductID) ?? throw new NullReferenceException("id does not exist");
                                    orderItem.Price = Convert.ToDouble(orderItem.Amount) * product.Price;
                                    id = dal.OrderItem.Add(orderItem);
                                    Console.WriteLine($@"Your orderItem ID is: {id}");
                                    break;
                                case "2":
                                    Console.WriteLine("Enter orderItem ID: ");
                                    int.TryParse(Console.ReadLine(), out id);
                                    orderItem = dal?.OrderItem.GetById(id) ?? throw new NullReferenceException("id does not exist");
                                    Console.WriteLine(orderItem);
                                    break;
                                case "3":
                                    IEnumerable<OrderItem?> list = dal?.OrderItem.GetAll() ?? throw new NullReferenceException("empty list");
                                    foreach (var item in list)
                                    {
                                        Console.WriteLine(item);
                                    }
                                    break;
                                case "4":
                                    orderItem = new OrderItem();
                                    Console.WriteLine("Enter order ID: ");
                                    int.TryParse(Console.ReadLine(), out id);
                                    orderItem.OrderID = id;
                                    Console.WriteLine("Enter product ID: ");
                                    int.TryParse(Console.ReadLine(), out id);
                                    orderItem.ProductID = id;
                                    Console.WriteLine("Enter amount of this product: ");
                                    int.TryParse(Console.ReadLine(), out amount);
                                    orderItem.Amount = amount;
                                    product = dal?.Product.GetById(orderItem.ProductID) ?? throw new NullReferenceException("id does not exist");
                                    product.InStock--;
                                    orderItem.Price = Convert.ToDouble(orderItem.Amount) * product.Price;
                                    dal.OrderItem.Update(orderItem);
                                    break;
                                case "5":
                                    Console.WriteLine("Enter orderItem ID: ");
                                    int.TryParse(Console.ReadLine(), out id);
                                    dal?.OrderItem.Delete(id);
                                    break;
                                case "6":
                                    Console.WriteLine("Enter order ID: ");
                                    int.TryParse(Console.ReadLine(), out id);
                                    list = dal?.OrderItem.getAllOrderItems(id) ?? throw new NullReferenceException("empty list");
                                    foreach (var item in list)
                                    {
                                        Console.WriteLine(item);
                                    }
                                    break;
                                case "7":
                                    Console.WriteLine("Enter order ID: ");
                                    int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine("Enter product ID: ");
                                    int.TryParse(Console.ReadLine(), out id2);
                                    or = dal?.OrderItem.getOrderItems(id2, id);
                                    Console.WriteLine(or);
                                    break;
                            };
                            break;
                    };

                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                }
                Console.WriteLine($@"Enter one of the options:
press 0 to exit
press 1 for Order
press 2 for Product
press 3 for OrderItem");
                user_choice = Console.ReadLine();
            }
            // static 
        }
    }
}