using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// A cart to which you add the products you want to order - type BO
/// </summary>
public class Cart
{
    /// <summary>
    /// The name of the customer who use the cart
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// The email of the customer who use the cart
    /// </summary>
    public string? CustomerEmail { get; set; }

    /// <summary>
    /// The address of the customer who use the cart
    /// </summary>
    public string? CustomerAddress { get; set; }

    /// <summary>
    /// Item in the cart
    /// </summary>
    public IEnumerable<OrderItem?> Items { get; set; }

    /// <summary>
    /// The total price of all the items in the cart
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// prints cart details
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
