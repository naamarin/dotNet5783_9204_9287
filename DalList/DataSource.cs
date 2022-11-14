using DO;

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

        internal static int s_startOrderItemNumber = 1000;
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int NextOrderItemNumber { get => s_nextOrderItemNumber++; }
    }

    internal static List<Product?> ProductList { get; }=new List<Product?>();

    internal static List<Order?> OrderList { get; }=new List<Order?>();

    internal static List<OrderItem?> OrderItemsList { get; }=new List<OrderItem?>();

    private static void s_Initialize()
    {
        createAndInitProducts();
        createAndInitOrders();
        createAndInitOrderItems();
    }

    private static void createAndInitProducts()
    {
        
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

        for(int i=0;i<=25;i++)
        {
            Order order = new Order();
            order.ID = Config.NextOrderNumber;
            string firstName = firstNames[rand.Next(0, 27)];
            string lastName = lastNames[rand.Next(0, 27)];
            order.CustomerName = firstName + " " + lastName;
            order.CustomerEmail = firstName + lastName + order.ID + "@gmail.com";
            order.CustomerAdress = address[rand.Next(0, 27)];
            OrderList.Add(order);
        }
    }
    private static void createAndInitOrderItems()
    {

    }
    static readonly Random rand= new Random();

}