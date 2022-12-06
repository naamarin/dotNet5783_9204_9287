
using DO;
using DalApi;
using System.Security.Cryptography;

namespace Dal;

internal class DalProduct : IProduct
{
    /// <summary>
    ///Function to create a new product
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public int Add(Product product)
    {
        if (DataSource.ProductList.Exists(x => x.Value.ID == product.ID))
            throw new DalAlreadyExistException("ID Product already exsists");
        DataSource.ProductList.Add(product);
        return product.ID;
    }
    /// <summary>
    /// A function to return an existing product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product GetById(int id)
    {
        if (!DataSource.ProductList.Exists(x => x?.ID == id))
        {
            throw new DalDoesNotExistException("product not exists");
        }
        return (Product)DataSource.ProductList.Find(x => x?.ID == id);
    }
    /// <summary>
    /// Product update function
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product product)
    {
        if (!DataSource.ProductList.Exists(x => x?.ID == product.ID))
        {
            throw new DalDoesNotExistException("product not exists");
        }
        DataSource.ProductList.Remove(DataSource.ProductList.Find(x => x?.ID == product.ID));
        DataSource.ProductList.Add(product);
    }
    /// <summary>
    /// A function to delete an existing product
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        if (!DataSource.ProductList.Exists(x => x?.ID == id))
        {
            throw new DalDoesNotExistException("product not exists");
        }
        DataSource.ProductList.Remove(DataSource.ProductList.Find(x => x?.ID == id));
    }

    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        if (filter == null)
        {
            return from Product? product in DataSource.ProductList
                   select product;
        }
        else
        {
            return from Product? product in DataSource.ProductList
                   where filter(product)
                   select product;
        }
    }
}

