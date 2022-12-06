using BlApi;
using BlImplementation;
using BO;
///לבדוק על החריגות

namespace BITest
{
    public class Program
    {
        public static IBl bl = new Bl();
        static Cart newCart = new Cart() { Items = new List<OrderItem>(), TotalPrice = 0 };
        static void OrderCheck()
        {
            char choice;
            int ID;
            Console.WriteLine("Enter your choice");
            Console.WriteLine($@"
press a for GetOrders for mannager ,
Press b for GetOrderById,
press c for UpdateOrderShip,
press d for UpdateDelivertOrder,
press e for OrderTracking,
press f for return to the menue");
            if (!char.TryParse(Console.ReadLine(), out choice)) throw new Exception("wrong input type");
            try
            {
                while (choice != 'f')
                {
                    switch (choice)
                    {
                        case 'a':
                            var v = bl.Order.OrderListForManager();
                            Console.WriteLine(String.Join(" ", v));//עובד 
                            break;
                        case 'b':
                            Console.WriteLine("Enter order Id");
                            if (!int.TryParse(Console.ReadLine(), out ID)) throw new FormatException("wrong input type");
                            Console.WriteLine(bl.Order.GetOrderById(ID));
                            break;
                        case 'c':
                            Console.WriteLine("Enter order Id");
                            if (!int.TryParse(Console.ReadLine(), out ID)) throw new FormatException("wrong input type");
                            Console.WriteLine(bl.Order.UpdateOrderShip(ID));
                            break;
                        case 'd':
                            Console.WriteLine("Enter order Id");
                            if (!int.TryParse(Console.ReadLine(), out ID)) throw new FormatException("wrong input type");
                            Console.WriteLine(bl.Order.UpdateDelivertOrder(ID));//פה קורס
                            break;
                        case 'e':
                            Console.WriteLine("Enter order Id");
                            if (!int.TryParse(Console.ReadLine(), out ID)) throw new FormatException("wrong input type");
                            Console.WriteLine(bl.Order.OrderTracking(ID));///עובד 
                            break;

                    }
                    char.TryParse(Console.ReadLine(), out choice);
                }


            }
            catch (BO.BlIdAlreadyExistException ex) { Console.WriteLine(ex); }
            catch (BO.BlIdDoNotExistException ex) { Console.WriteLine(ex); }
            catch (BO.BlIncorrectDateException ex) { Console.WriteLine(ex); }
            catch (BO.BlInvalidInputException ex) { Console.WriteLine(ex); }
            catch (BO.BlNullPropertyException ex) { Console.WriteLine(ex); }
            catch (BO.BlWrongCategoryException ex) { Console.WriteLine(ex); }
            catch (FormatException ex) { Console.WriteLine(ex); }
            catch (ArgumentException ex) { Console.WriteLine(ex); }








        }
        static void ProductCheck()
        {
            char choice;

            Console.WriteLine("Enter your choice");
            Console.WriteLine($@"
press a for Get Listed Products for mannager ,
Press b for Get Order By Id,
press c for Add Product,
press d for Delete Product,
press e for Update ProductData,
press f for Request Product Details For C,
press g for  Request Product DetailsFor M
press h for exit");
            if (!char.TryParse(Console.ReadLine(), out choice)) throw new FormatException("wrong input type");
            try
            {
                while (choice != 'h')
                {
                    switch (choice)

                    {
                        case 'a':
                            var lst = bl.Product.GetListedProducts();
                            foreach (var item in lst)
                                Console.WriteLine(item);
                            break;
                        case 'b':
                            int id;
                            Console.WriteLine("enter id of product:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new FormatException("wrong input type");
                            Console.WriteLine(bl.Product.GetProductsById(id));
                            break;

                        case 'c':
                            Product addProduct = new Product();
                            double price;
                            int category;
                            int stock;

                            Console.WriteLine("enter id of product:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new FormatException("wrong input type");
                            addProduct.ProductID = id;
                            Console.WriteLine("enter name of product:");
                            addProduct.Name = Console.ReadLine();
                            Console.WriteLine("enter price of product:");
                            if (!double.TryParse(Console.ReadLine(), out price)) throw new FormatException("wrong input type");
                            addProduct.Price = price;
                            Console.WriteLine("enter category of product:");
                            if (!int.TryParse(Console.ReadLine(), out category)) throw new FormatException("wrong input type");
                            addProduct.Category = (Category)(category);
                            Console.WriteLine("enter amount in stock of product:");
                            if (!int.TryParse(Console.ReadLine(), out stock)) throw new FormatException("wrong input type");
                            addProduct.InStock = stock;
                            bl.Product.AddProduct(addProduct);

                            break;
                        case 'd':
                            Console.WriteLine("enter id to delete product:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new FormatException("wrong input type");
                            bl.Product.DeleteProduct(id);
                            break;
                        case 'e':
                            Product updateProduct = new Product();
                            Console.WriteLine("enter id of product:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new FormatException("wrong input type");
                            updateProduct.ProductID = id;
                            Console.WriteLine("enter name of product:");
                            updateProduct.Name = Console.ReadLine();
                            Console.WriteLine("enter price of product:");
                            if (!double.TryParse(Console.ReadLine(), out price)) throw new FormatException("wrong input type");
                            updateProduct.Price = price;
                            Console.WriteLine("enter category of product:");
                            if (!int.TryParse(Console.ReadLine(), out category)) throw new FormatException("wrong input type");
                            updateProduct.Category = (Category)category;
                            Console.WriteLine("enter amount in stock of product:");
                            if (!int.TryParse(Console.ReadLine(), out stock)) throw new FormatException("wrong input type");
                            updateProduct.InStock = stock;
                            bl.Product.UpdateProductData(updateProduct);
                            break;
                        case 'f':
                            foreach (var item in bl.Product.GetListedProductsForC())
                                Console.WriteLine(item);
                            break;
                        case 'g':
                            Console.WriteLine("enter id of product:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new FormatException("wrong input type");
                            Console.WriteLine(bl.Product.RequestProductDetailsForC(id, newCart));
                            break;


                    }
                    char.TryParse(Console.ReadLine(), out choice);
                }


            }
            catch (BO.BlIdAlreadyExistException ex) { Console.WriteLine(ex); }
            catch (BO.BlIdDoNotExistException ex) { Console.WriteLine(ex); }
            catch (BO.BlIncorrectDateException ex) { Console.WriteLine(ex); }
            catch (BO.BlInvalidInputException ex) { Console.WriteLine(ex); }
            catch (BO.BlNullPropertyException ex) { Console.WriteLine(ex); }
            catch (BO.BlWrongCategoryException ex) { Console.WriteLine(ex); }
            catch (FormatException ex) { Console.WriteLine(ex); }
            catch (ArgumentException ex) { Console.WriteLine(ex); }

        }
        static void CartCheck()
        {
            char choice;
            Console.WriteLine("Enter your choice");
            Console.WriteLine($@"
press a for Add Product to the cart ,
Press b for Update Amount of product in cart ,
press c for Order Confirmation,
press f for return to the menue");
            if (!char.TryParse(Console.ReadLine(), out choice)) throw new Exception("wrong input type");
            try
            {
                while (choice != 'f')
                {
                    switch (choice)
                    {
                        case 'a':
                            int id, amount;

                            Console.WriteLine("enter id of product to add to cart:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Cart.AddProduct(newCart, id));

                            break;
                        case 'b':

                            Console.WriteLine("enter id of product to add to cart:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine("enter new amount of product:");
                            if (!int.TryParse(Console.ReadLine(), out amount)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Cart.UpdateAmount(newCart, id, amount));
                            break;

                        case 'c':
                            Console.WriteLine("please insert name:");
                            newCart.CustomerName = Console.ReadLine();
                            Console.WriteLine("please insert address:");
                            newCart.CustomerAddress = Console.ReadLine();
                            Console.WriteLine("please insert email address:");
                            newCart.CustomerEmail = Console.ReadLine();
                            bl.Cart.OrderConfirmation(newCart);
                            newCart = new Cart() { OrderItems = new List<OrderItem>(), Price = 0 };
                            break;

                    }
                    char.TryParse(Console.ReadLine(), out choice);
                }

            }
            catch (BO.BlIdAlreadyExistException ex) { Console.WriteLine(ex); }
            catch (BO.BlIdDoNotExistException ex) { Console.WriteLine(ex); }
            catch (BO.BlIncorrectDateException ex) { Console.WriteLine(ex); }
            catch (BO.BlInvalidInputException ex) { Console.WriteLine(ex); }
            catch (BO.BlNullPropertyException ex) { Console.WriteLine(ex); }
            catch (BO.BlWrongCategoryException ex) { Console.WriteLine(ex); }
            catch (FormatException ex) { Console.WriteLine(ex); }
            catch (ArgumentException ex) { Console.WriteLine(ex); }



        }

        static void Main(string[] args)
        {
            /// main program which check the function
            Program Program = new Program();
            char choice;
            Console.WriteLine("enter one of the following options");
            Console.WriteLine($@"
press 1 for Order,
press 2 for product,
press 3 for Cart,
press 4 for exit");
            char.TryParse(Console.ReadLine(), out choice);

            while (choice != '5')
            {

                switch (choice)
                {
                    case '1':
                        Program.OrderCheck();

                        break;
                    case '2':
                        Program.ProductCheck();

                        break;
                    case '3':
                        Program.CartCheck();
                        break;

                    case '4':
                        return;


                }
                Console.WriteLine($@"
press 1 for Order,
press 2 for Product,
press 3 for Cart,
press 4 for exit");
                char.TryParse(Console.ReadLine(), out choice);

            }
        }
    }
}
