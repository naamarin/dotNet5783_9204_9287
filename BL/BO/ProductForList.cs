using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// For the product list screen and catalog screen - type BO
/// </summary>
public class ProductForList
{
    /// <summary>
    /// Product ID
    /// </summary>
    public int ID { get; set; } 

    /// <summary>
    /// Product name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Product price
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Product category
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// Product Image
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// print details of all the product
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
         return this.ToStringProperty();
    }
}
