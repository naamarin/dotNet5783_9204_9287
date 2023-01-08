using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// Product in the store - type BO
/// </summary>
public class Product
{
    /// <summary>
    /// product ID
    /// </summary>
    public int ID { get; set; } 

    /// <summary>
    /// Product name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// product price
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Product category
    /// </summary>
    public Category Category { get; set; }
    
    /// <summary>
    /// The product amount
    /// </summary>
    public int StockCount { get; set; }

    /// <summary>
    /// Product Image
    /// </summary>
    public string? ImageRelativeName { get; set; }

    /// <summary>
    /// print details ot product
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
