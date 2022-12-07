using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IProduct
{
    public IEnumerable<BO.ProductForList> GetListProducts();
    public BO.Product GetById(int id);
    public void AddProduct(BO.Product? boProduct);
    public void RemoveProduct(int idProduct);
    public void UpdateProduct(BO.Product boProduct);
    public IEnumerable<BO.ProductItem> Catalog();
    public BO.ProductItem ProductDeatails(int productID);
}
