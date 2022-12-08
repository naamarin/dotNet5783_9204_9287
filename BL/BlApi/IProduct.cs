using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IProduct
{
    /// <summary>
    /// function for get list of all the products (for the manager)
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.ProductForList> GetListProducts();

    /// <summary>
    /// function for get product by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Product GetById(int id);

    /// <summary>
    /// function for add product
    /// </summary>
    /// <param name="boProduct"></param>
    public void AddProduct(BO.Product? boProduct);

    /// <summary>
    /// function for delete product
    /// </summary>
    /// <param name="idProduct"></param>
    public void RemoveProduct(int idProduct);

    /// <summary>
    /// function for update the details of product
    /// </summary>
    /// <param name="boProduct"></param>
    public void UpdateProduct(BO.Product boProduct);

    /// <summary>
    /// function for get catalog of all the products (for the client)
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.ProductItem> Catalog();

    /// <summary>
    /// function for get details of all the products
    /// </summary>
    /// <param name="productID"></param>
    /// <returns></returns>
    public BO.ProductItem ProductDeatails(int productID);
}
