using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// Helper entity of a product item (representing a product for the catalog) for the catalog screen - with the list of products that are shown to the buyer - type BO
/// </summary>
public class ProductItem
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
    /// product's amount in the cart
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Availability - check if the product availble
    /// </summary>
    public bool InStock { get; set; }

    public override string ToString()
    {
         return this.ToStringProperty( );
    }
}
