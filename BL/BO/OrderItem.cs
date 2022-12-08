using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// Single item from order - type BO
/// </summary>
public class OrderItem
{
    /// <summary>
    /// OrderItme ID's
    /// </summary>
    public int ID { get; set; } 

    /// <summary>
    /// The name of the product
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The product ID
    /// </summary>
    public int ProductID { get; set; }

    /// <summary>
    /// The product price
    /// </summary>
    public double Price { get; set; }  

    /// <summary>
    /// Amount of the product
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Amount * Price
    /// </summary>
    public double TotalPrice { get; set; }
    /// <summary>
    /// print all the items in the order
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
         return this.ToStringProperty();
    }
}
