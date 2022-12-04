
using DO;

namespace Dal;

public class DalProduct
{
    /// <summary>
    ///Function to create a new product
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public int Add(Product product)
    {
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
            throw new Exception("product not exists");
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
            throw new Exception("product not exists");
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
            throw new Exception("product not exists");
        }
        DataSource.ProductList.Remove(DataSource.ProductList.Find(x => x?.ID == id));
    }
    /// <summary>
    /// Function to return all products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll()
    {
        List<Product?> newList = new List<Product?>();
        for (int i = 0; i < DataSource.ProductList.Count; i++)
        {
            newList.Add(DataSource.ProductList[i]);
        }
        return newList;
    }
}

