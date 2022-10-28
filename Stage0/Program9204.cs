partial class Program
{
    static void Main(string[] args)
    {
        Welcome9204();
        Welcome9287();
        Console.ReadKey();
    }
    private static void Welcome9204()
    {
        Console.WriteLine("Enter your name: ");
        string name = Console.ReadLine();
        Console.WriteLine("{0}, welcome to my first console application", name);
    }
    static partial void Welcome9287();
    
}