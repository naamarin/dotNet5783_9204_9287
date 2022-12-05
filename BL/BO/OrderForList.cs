using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// List order helper entity for the order list screen - type BO
/// </summary>
public class OrderForList
{
    /// <summary>
    /// ID's order
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// The name of the castomer who did the order
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// The order status
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// The amount of the Item in the order
    /// </summary>
    public int AmountOfItems { get; set; }

    /// <summary>
    /// The total price of the order
    /// </summary>
    public double TotalPrice { get; set; }

    //public override string ToString()
    //{
    //   // return this.ToStringProperty();
    //}
}
