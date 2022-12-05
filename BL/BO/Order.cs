using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// Order of products made by the customer - type BO
/// </summary>
public class Order
{
    /// <summary>
    /// The order ID
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// The name of the customer that create this order
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// The email of the customer that create this order
    /// </summary>
    public string? CustomerEmail { get; set; }

    /// <summary>
    /// The address to which the order should be sent
    /// </summary>
    public string? CustomerAddress { get; set; }

    /// <summary>
    /// The date which the order created
    /// </summary>
    public DateTime? OrderDate { get; set; }

    /// <summary>
    /// Order status. such as - shipped, delivered
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// The date which the paiment was done 
    /// </summary>
    public DateTime? PaymentDate { get; set; }

    /// <summary>
    /// The date that the order shipped
    /// </summary>
    public DateTime? ShipDate { get; set; }

    /// <summary>
    /// The date that the order delivered
    /// </summary>
    public DateTime? DeliveryDate { get; set; }

    /// <summary>
    /// List of the items in the order
    /// </summary>
    public IEnumerable<OrderItem?> Items { get; set; }

    /// <summary>
    /// The price of all the items in the order
    /// </summary>
    public double TotalPrice { get; set; }

    //public override string ToString()
    //{
    //   // return this.ToStringProperty();
    //}
}
