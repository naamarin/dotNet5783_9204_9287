
using DO;
using DalApi;
using System.Security.Cryptography;

namespace Dal;

internal class DalProduct : IProduct
{

    public int Add(Product product)
    {
        if (DataSource.ProductList.Exists(x => x.Value.ID == product.ID))
            throw new DalAlreadyExistException("ID Product already exsists");
        DataSource.ProductList.Add(product);
        return product.ID;
    }

    public Product GetById(int id)
    {
        if (!DataSource.ProductList.Exists(x => x?.ID == id))
        {
            throw new DalDoesNotExistException("product not exists");
        }
        return (Product)DataSource.ProductList.Find(x => x?.ID == id);
    }

    public void Update(Product product)
    {
        if (!DataSource.ProductList.Exists(x => x?.ID == product.ID))
        {
            throw new DalDoesNotExistException("product not exists");
        }
        DataSource.ProductList.Remove(DataSource.ProductList.Find(x => x?.ID == product.ID));
        DataSource.ProductList.Add(product);
    }

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
        return from pl in DataSource.ProductList select pl;
        //List<Product?> newList = new List<Product?>();
        //for (int i = 0; i < DataSource.ProductList.Count; i++)
        //{
        //    newList.Add(DataSource.ProductList[i]);
        //}
        //return newList;
    }
}

