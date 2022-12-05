
namespace DO;
/// <summary>
/// This stuct describes a single product in the store - type DO
/// </summary>
public struct Product
{
    /// <summary>
    /// Unique ID number of a product
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Product Name
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// The product category
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// The price of the product
    /// </summary>
    public Category? Category { get; set; }
    /// <summary>
    /// Product quantity in stock
    /// </summary>
    public int InStock { get; set; }

    /// <summary>
    /// A function that prints all the product details
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Tools.ToStringProperty(this);
    }
//    => $@"
//Product ID: {ID}
//Name: {Name}
//Category: {Category}
//Price: {Price}
//Amount in stock: {InStock}
//";

}
