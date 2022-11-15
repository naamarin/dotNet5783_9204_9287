
using System;
using System.Diagnostics;
using System.Xml.Linq;

namespace DO;

/// <summary>
/// This struct describes a single customer order 
/// </summary>
public struct Order
{
    /// <summary>
    /// Unique ID number of an order
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// The customer's name
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// Customer's email
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// Customer's cell phone number
    /// </summary>
    public int CustomerNumber { get; set; }
    /// <summary>
    /// Shipping address
    /// </summary>
    public string? CustomerAdress { get; set; }
    /// <summary>
    /// Order creation date
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// delivery date
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// Date of delivery
    /// </summary>
    public DateTime? DeliveryDate { get; set; }

    /// <summary>
    /// A function that prints all the order details
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
ID: {ID},
Customer Name: {CustomerName}
Customer Email: {CustomerEmail}
Customer Number: 0{CustomerNumber}
customer Address: {CustomerAdress}
Order Date: {OrderDate}
Ship Date: {ShipDate}
Delivery Date: {DeliveryDate}
";
}

