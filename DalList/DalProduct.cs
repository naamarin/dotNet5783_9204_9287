
using DO;

namespace Dal;

public class DalProduct
{

    public int Add(Product product)
    {
        DataSource.ProductList.Add(product);
        return product.ID;
    }

    public Product GetById(int id)
    {
        if (!DataSource.ProductList.Exists(x => x?.ID == id))
        {
            throw new Exception("product not exists");
        }
        return (Product)DataSource.ProductList.Find(x => x?.ID == id);
    }

    public void Update(Product product)
    {
        if (!DataSource.ProductList.Exists(x => x?.ID == product.ID))
        {
            throw new Exception("product not exists");
        }
        DataSource.ProductList.Remove(DataSource.ProductList.Find(x => x?.ID == product.ID));
        DataSource.ProductList.Add(product);
    }

    public void Delete(int id)
    {
        if (!DataSource.ProductList.Exists(x => x?.ID == id))
        {
            throw new Exception("product not exists");
        }
        DataSource.ProductList.Remove(DataSource.ProductList.Find(x => x?.ID == id));
    }

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

