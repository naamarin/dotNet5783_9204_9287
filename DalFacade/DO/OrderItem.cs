
using System.Xml.Linq;

namespace DO;
/// <summary>
/// This struct describes a single item on the order
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// Unique ID number of an item in the order
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// ID number of the product
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// ID number of the order
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// Price per unit
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Product amount 
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// A function that prints all the details of the item in the order
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
    ID: {ID}
    Product ID: {ProductID} 
    Order ID: {OrderID}
    Price: {Price}
    Amount: {Amount}
    ";
   

}
