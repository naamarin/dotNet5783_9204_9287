using DO;
using System.IO;

namespace Dal;

internal static class DataSource
{
    // internal static DataSource DSInstance { get; } = new DataSource();

    static DataSource()
    {
        s_Initialize();
    }
    private static readonly Random s_rand = new();

    internal static class Config
    {
        internal static int s_startOrderNumber = 1000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => s_nextOrderNumber++; }

        internal static int s_startOrderItemNumber = 10000;
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int NextOrderItemNumber { get => s_nextOrderItemNumber++; }
    }

    internal static List<Product?> ProductList { get; } = new List<Product?>();

    internal static List<Order?> OrderList { get; } = new List<Order?>();

    internal static List<OrderItem?> OrderItemsList { get; } = new List<OrderItem?>();

    private static void s_Initialize()
    {
        createAndInitProducts();
        createAndInitOrders();
        createAndInitOrderItems();
    }

    private static void createAndInitProducts()
    {
        int[] ids = { 9621542, 7586215, 4658921, 9359824, 6587469, 2589457, 5462876, 1548256,
             8548756, 8457953, 2548756, 3254815, 4568248, 7546285, 3658456 ,7546258,1582654,2458638};
        string[] names = {"Ranch","Crispy Schnitzel","Vegan Ranch","Sausage in a bun","Schnitzel Kids","Ranch Kids","Fries","Salad",
        "Onion Rings","Shokopay","Ice Cream","Souffle","Coca-Cola","Cola Zero","Fanta","Ketchup","Barbecue","The Thousand Islands" };
        double[] prices = { 55, 55, 50, 34, 34, 34, 20, 15, 25, 14, 9, 9, 13, 13, 13, 0.5, 0.5, 0.5 };
        int[] amounts = { 41, 36, 62, 31, 43, 20, 45, 32, 8, 13, 24, 0, 30, 25, 12, 40, 25, 0 };
        string[] category = { "Meals","Meals","Meals","Children","Children","Children","Extras","Extras","Extras",
        "Desserts","Desserts","Desserts","Drinks","Drinks","Drinks","Sauces","Sauces","Sauces"};
        for (int i= 0; i < ids.Length; i++)
        {
            Product product = new Product();
            product.ID = ids[i];
            product.Name = names[i];
            product.InStock = amounts[i];
            product.Price = prices[i];
            product.Category = category[i];
            ProductList.Add(product);
        }
    }
    private static void createAndInitOrders()
    {
        string[] firstNames = { "Naama", "Shira", "Avigail", "David", "Roi", "Noa", "Shimon", "Yael","Sari",
        "Rivka","Gili","Meir","Ita","Tohar","Sara","Shirel","Chen","Shay","Maor", "Itamar","Shir","Reuven",
        "Efrat","Hadas","Ruty","Shiran","Adel","Raheli"};

        string[] lastNames = { "Levi","Cohen","Shvizer","Melech","Klayn","Mory","Yaakov","Mauda","Zilber","Shor",
        "Raych","Kotzk","Shteren","Nahari","Peri","Hofi","Aviv","Shalom","Zvili","Rabi","Golan","Chen","Tzur",
        "Zilber", "Levi","Pur","Avraham","Peri"};

        string[] address = { "Rashi 6","Harif 20","Ben Tsetah 8","Hritba 6","Shammai 40","Yehuda Hanasi 18",
        "Ben Kisma 19","Ben Zakai 100","Ben Uziel 90", "Hillel 37", "Hathor 7", "Nissim Gaon 36", "Reef 11",
        "Shemaya 7", "Ben Kisma 5", "Zamir 8", "Ben Tsetah 46" , "Shimon Hatzadik 15", "Yehuda Hanasi 2",
        "Rab'd 31","Ben Zakai 29","Shamai 52","Avtalion 9","Ibn Gvirol 6","Mayiri 42","Haran 4","Yosef Karo 10","Tena 13"};
        int indexDelivery = 0;
        int indexShip = 0;
        //DateTime[] dates = { }
        for (int i = 0; i <= 25; i++)
        {
            Order order = new Order();
            order.ID = Config.NextOrderNumber;
            string firstName = firstNames[rand.Next(0, 27)];
            string lastName = lastNames[rand.Next(0, 27)];
            order.CustomerName = firstName + " " + lastName;
            order.CustomerEmail = firstName + lastName + order.ID + "@gmail.com";
            order.CustomerAdress = address[rand.Next(0, 27)] + "Elad";
            int hour = rand.Next(1,12);
            int minute = rand.Next(1,59);
            int second = rand.Next(1,59);
            order.OrderDate = new DateTime(2022, 11, 14, hour, minute, second);
            if (indexShip < 18)
            {
                order.ShipDate = new DateTime(2022, 11, 14, hour, minute + 30, second + 2);
                indexShip++;
            }
            if (indexDelivery < 5)
            {
                order.DeliveryDate = new DateTime(2022, 11, 14, hour, minute + 50, second + 12);
                indexDelivery++;
            }
            OrderList.Add(order);
        }
    }
    private static void createAndInitOrderItems()
    {
        int[] productsIds = { 9621542, 7586215, 4658921, 9359824, 6587469, 2589457, 5462876, 1548256,
             8548756, 8457953, 2548756, 3254815, 4568248, 7546285, 3658456 ,7546258,1582654,2458638};
        double[] prices = { 55, 55, 50, 34, 34, 34, 20, 15, 25, 14, 9, 9, 13, 13, 13, 0.5, 0.5, 0.5 };
        int[] amounts = { 2, 1, 1, 1, 2, 3, 1, 1, 2, 3, 2, 2, 1, 1, 1, 1, 2, 1, 2, 1, 1, 1, 1, 1, 1, 1, 4,
            3, 2, 3, 2, 3, 2, 2, 1, 1, 2, 4, 4, 1, 1, 4, 2, 3, 2, 2, 4, 3, 3 };
        for (int i = 0; i <= 49; i++)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.ID = Config.NextOrderItemNumber;
            orderItem.OrderID = rand.Next(1000, 1025);
            int rando = rand.Next(0, productsIds.Length);
            orderItem.ProductID = productsIds[rando];
            orderItem.Amount = amounts[i];
            orderItem.Price = prices[rando]*orderItem.Amount;            OrderItemsList.Add(orderItem);
        }
    }
    static readonly Random rand = new Random();

}