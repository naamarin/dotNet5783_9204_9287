using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalProduct : IProduct
{
    readonly string s_producs = "products";
    public int Add(Product product)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_producs);
        if (products.Exists(x => x.Value.ID == product.ID))
            throw new DalAlreadyExistException("ID Product already exsists");
        products.Add(product);
        products.Sort((x, y) => x.Value.Category.Value.CompareTo(y.Value.Category.Value));
        XMLTools.SaveListToXMLSerializer(products, s_producs);
        return product.ID;
    }

    public void Delete(int id)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_producs);
        if (!products.Exists(x => x?.ID == id))
        {
            throw new DalDoesNotExistException("product not exists");
        }
        products.Remove(products.Find(x => x?.ID == id));
        XMLTools.SaveListToXMLSerializer(products, s_producs);
    }

    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_producs);
        if (filter == null)
        {
            return from Product? product in products
                   select product;
        }
        else
        {
            return from Product? product in products
                   where filter(product)
                   select product;
        }
    }

    public Product GetById(int id)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_producs);
        if (!products.Exists(x => x?.ID == id))
        {
            throw new DalDoesNotExistException("product not exists");
        }
        return (Product)products.Find(x => x?.ID == id);
    }

    public void Update(Product product)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_producs);
        if (!products.Exists(x => x?.ID == product.ID))
        {
            throw new DalDoesNotExistException("product not exists");
        }
        products.Remove(products.Find(x => x?.ID == product.ID));
        products.Add(product);
        products.Sort((x, y) => x.Value.Category.Value.CompareTo(y.Value.Category.Value));
        XMLTools.SaveListToXMLSerializer(products, s_producs);
    }
}
