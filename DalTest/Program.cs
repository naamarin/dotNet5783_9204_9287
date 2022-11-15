// See https://aka.ms/new-console-template for more information
using Dal;
using DO;

namespace DalTest
{
    public class program
    {
        private static DalOrder DalOrderObj = new DalOrder();
        private static DalOrderItem DalOrderItemObj = new DalOrderItem();
        private static DalProduct DalProductObj = new DalProduct();

        static void Main(string[] args)
        {
            string action;
            int id, id2;
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
                                    order.CustomerNumber = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter your email: ");
                                    order.CustomerEmail = Console.ReadLine();
                                    Console.WriteLine("Enter your address: ");
                                    order.CustomerAdress = Console.ReadLine();
                                    order.OrderDate = DateTime.Now;
                                    id = DalOrderObj.Add(order);
                                    Console.WriteLine($@"Your order ID is: {id}");
                                    break;
                                case "2":
                                    Console.WriteLine("Enter order ID: ");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    order = DalOrderObj.GetById(id);
                                    Console.WriteLine(order);
                                    break;
                                case "3":
                                    IEnumerable<Order?> list = DalOrderObj.GetAll();
                                    foreach (var item in list)
                                    {
                                        Console.WriteLine(item);
                                    }
                                    break;
                                case "4":
                                    order = new Order();
                                    Console.WriteLine("Enter your order ID: ");
                                    order.ID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter your full name: ");
                                    order.CustomerName = Console.ReadLine();
                                    Console.WriteLine("Enter your number: ");
                                    order.CustomerNumber = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter your email: ");
                                    order.CustomerEmail = Console.ReadLine();
                                    Console.WriteLine("Enter your address: ");
                                    order.CustomerAdress = Console.ReadLine();
                                    order.OrderDate = DateTime.Now;
                                    DalOrderObj.Update(order);
                                    break;
                                case "5":
                                    Console.WriteLine("Enter order ID: ");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    DalOrderObj.Delete(id);
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
                                    product.ID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter product name: ");
                                    product.Name = Console.ReadLine();
                                    Console.WriteLine("Enter product price: ");
                                    product.Price = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter product category: ");
                                    product.Category = Console.ReadLine();
                                    Console.WriteLine("Enter product amount in stock: ");
                                    product.InStock = Convert.ToInt32(Console.ReadLine());
                                    id = DalProductObj.Add(product);
                                    break;
                                case "2":
                                    Console.WriteLine("Enter product ID: ");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    product = DalProductObj.GetById(id);
                                    Console.WriteLine(product);
                                    break;
                                case "3":
                                    IEnumerable<Product?> list = DalProductObj.GetAll();
                                    foreach (var item in list)
                                    {
                                        Console.WriteLine(item);
                                    }
                                    break;
                                case "4":
                                    product = new Product();
                                    Console.WriteLine("Enter product ID: ");
                                    product.ID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter product name: ");
                                    product.Name = Console.ReadLine();
                                    Console.WriteLine("Enter product price: ");
                                    product.Price = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter product category: ");
                                    product.Category = Console.ReadLine();
                                    Console.WriteLine("Enter product amount in stock: ");
                                    product.InStock = Convert.ToInt32(Console.ReadLine());
                                    DalProductObj.Update(product);
                                    break;
                                case "5":
                                    Console.WriteLine("Enter order ID: ");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    DalProductObj.Delete(id);
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
                                    orderItem.OrderID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter product ID: ");
                                    orderItem.ProductID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter amount of this product: ");
                                    orderItem.Amount = Convert.ToInt32(Console.ReadLine());
                                    product = DalProductObj.GetById(orderItem.ProductID);
                                    orderItem.Price = Convert.ToDouble(orderItem.Amount) * product.Price.Value;
                                    id = DalOrderItemObj.Add(orderItem);
                                    Console.WriteLine($@"Your orderItem ID is: {id}");
                                    break;
                                case "2":
                                    Console.WriteLine("Enter orderItem ID: ");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    orderItem = DalOrderItemObj.GetById(id);
                                    Console.WriteLine(orderItem);
                                    break;
                                case "3":
                                    IEnumerable<OrderItem?> list = DalOrderItemObj.GetAll();
                                    foreach (var item in list)
                                    {
                                        Console.WriteLine(item);
                                    }
                                    break;
                                case "4":
                                    orderItem = new OrderItem();
                                    Console.WriteLine("Enter order ID: ");
                                    orderItem.OrderID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter product ID: ");
                                    orderItem.ProductID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter amount of this product: ");
                                    orderItem.Amount = Convert.ToInt32(Console.ReadLine());
                                    product = DalProductObj.GetById(orderItem.ProductID);
                                    orderItem.Price = Convert.ToDouble(orderItem.Amount) * product.Price.Value;
                                    DalOrderItemObj.Update(orderItem);
                                    break;
                                case "5":
                                    Console.WriteLine("Enter orderItem ID: ");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    DalOrderItemObj.Delete(id);
                                    break;
                                case "6":
                                    Console.WriteLine("Enter order ID: ");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    list = DalOrderItemObj.getAllOrderItems(id);
                                    foreach (var item in list)
                                    {
                                        Console.WriteLine(item);
                                    }
                                    break;
                                case "7":
                                    Console.WriteLine("Enter order ID: ");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Enter product ID: ");
                                    id2 = Convert.ToInt32(Console.ReadLine());
                                    or = DalOrderItemObj.getOrderItems(id2, id);
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