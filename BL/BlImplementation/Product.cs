using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;


internal class Product : BlApi.IProduct
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    /// <summary>
    /// function for get list of all the products (for the manager)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public IEnumerable<BO.ProductForList> GetListProducts()
    {
        return from DO.Product? doProduct in dal?.Product.GetAll() ?? throw new NullReferenceException("Empty list")
               select new BO.ProductForList
               {
                   ID = doProduct?.ID ?? throw new NullReferenceException("Missing ID"),
                   Name = doProduct?.Name ?? throw new NullReferenceException("Missing name"),
                   Category = (BO.Category?)doProduct?.Category ?? throw new NullReferenceException("Missing category"),
                   Price = doProduct?.Price ?? 0
               };
    }

    /// <summary>
    /// function for get list of all the products with selected category
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public IEnumerable<BO.ProductForList> GetListProductsByCategory(BO.Category c)
    {
        return from DO.Product? doProduct in dal?.Product.GetAll() ?? throw new NullReferenceException("Empty list")
               where doProduct?.Category == (DO.Category)c
               select new BO.ProductForList
               {
                   ID = doProduct?.ID ?? throw new NullReferenceException("Missing ID"),
                   Name = doProduct?.Name ?? throw new NullReferenceException("Missing name"),
                   Category = (BO.Category?)doProduct?.Category ?? throw new NullReferenceException("Missing category"),
                   Price = doProduct?.Price ?? 0
               };
    }

    /// <summary>
    /// function for get product by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    /// <exception cref="BO.BlWrongCategoryException"></exception>
    /// <exception cref="BO.BlProductDoesNotExsist"></exception>
    public BO.Product GetById(int id)
    {
        try
        {
            DO.Product? doProduct = dal?.Product.GetById(id) ?? throw new NullReferenceException("id does not exists");
            return new BO.Product()
            {
                ID = doProduct?.ID ?? throw new BO.BlNullPropertyException("Null producr ID"),
                Category = (BO.Category?)doProduct?.Category ?? throw new BO.BlWrongCategoryException("Product does not exist"),
                Price = doProduct?.Price ?? 0,
                Name = doProduct?.Name ?? "",
                StockCount = doProduct?.InStock ?? 0
            };
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlProductDoesNotExsist("Product not Exist", ex);
        }
    }

    /// <summary>
    /// function for add product
    /// </summary>
    /// <param name="boProduct"></param>
    /// <exception cref="ArgumentException"></exception>
    public void AddProduct(BO.Product? boProduct)
    {
        DO.Product doProduct = new DO.Product()
        {
            ID = boProduct?.ID ?? throw new ArgumentException("Invalid ID"),
            Name = boProduct?.Name ?? throw new ArgumentException("Invalid name"),
            Price = boProduct?.Price ?? throw new ArgumentException("Invalid price"),
            InStock = boProduct?.StockCount ?? throw new ArgumentException("Invalid stock amount"),
            Category=(DO.Category)boProduct?.Category,//?? throw new ArgumentException("Invalid category"),
        };
        dal?.Product.Add(doProduct);
    }

    /// <summary>
    /// a temp function for check if the product is exist
    /// </summary>
    /// <param name="order"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool isExist(DO.Order order, int id)
    {
        try
        {
            DO.OrderItem? orderItem = dal?.OrderItem.getOrderItems(order.ID, id);
        }
        catch(DO.DalDoesNotExistException ex)
        {
            return false;
        }
        return true;    
    }

    /// <summary>
    /// function for delete product
    /// </summary>
    /// <param name="idProduct"></param>
    /// <exception cref="BO.BlProductDoesNotExsist"></exception>
    public void RemoveProduct(int idProduct)
    {
    
        try
        {
            DO.Product product = dal?.Product.GetById(idProduct) ?? throw new NullReferenceException("id does not exist");
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlProductDoesNotExsist("Product does not exist", ex);
        }
        IEnumerable<DO.OrderItem?> odl = from DO.Order doOrders in dal.Order.GetAll() where isExist(doOrders, idProduct) select dal.OrderItem.getOrderItems(doOrders.ID, idProduct);

        if (!odl.Any())
            dal.Product.Delete(idProduct);
    }

    /// <summary>
    /// function for update the details of product
    /// </summary>
    /// <param name="boProduct"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="BO.BlProductDoesNotExsist"></exception>
    public void UpdateProduct(BO.Product boProduct)
    {
        if (boProduct.ID < 0)
            throw new ArgumentException("Invalid ID");
        if (boProduct.Name == "")
            throw new ArgumentException("Invalid name");
        if (boProduct.Price < 0)
            throw new ArgumentException("Invalid price");
        if (boProduct.StockCount <= 0)
            throw new ArgumentException("Invalid stock amount");
        DO.Product doProduct = new DO.Product()
        {
            ID = boProduct.ID, //?? throw new ArgumentException("Invalid ID"),
            Name = boProduct.Name,// ?? throw new ArgumentException("Invalid name"),
            Price = boProduct.Price,// ?? throw new ArgumentException("Invalid price"),
            InStock = boProduct.StockCount,// ?? throw new ArgumentException("Invalid stock amount"),
            Category = (DO.Category)boProduct?.Category,//?? throw new ArgumentException("Invalid category"),
        };
        try
        {
            dal?.Product.Update(doProduct);
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.BlProductDoesNotExsist("Product canot be upgrade because its not exsist", ex);
        }
    }

    /// <summary>
    /// function for get catalog of all the products (for the client)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public IEnumerable<BO.ProductItem> Catalog()
    {
        return from DO.Product doProduct in dal?.Product.GetAll() ?? throw new NullReferenceException("Empty list")
               select new BO.ProductItem()
            {
                ID = doProduct.ID,
                Name = doProduct.Name,
                Price = doProduct.Price,
                Category = (BO.Category?)doProduct.Category ?? throw new NullReferenceException("Missing category"),
                Amount = doProduct.InStock,
                InStock = doProduct.InStock > 0,
            };
    }

    /// <summary>
    /// function for get details of all the products
    /// </summary>
    /// <param name="productID"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    /// <exception cref="NullReferenceException"></exception>
    public BO.ProductItem ProductDeatails( int productID)
    {
        DO.Product product;
        try
        {
            product = dal?.Product.GetById(productID) ?? throw new NullReferenceException("id does not exists");
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Product not Exist", ex);
        }
        return new BO.ProductItem
        {
            ID = productID,
            Name = product.Name,
            Price = product.Price,
            Amount = product.InStock,
            Category = (BO.Category?)product.Category ?? throw new NullReferenceException("Missing category"),
            InStock = product.InStock > 0,
        };
    }
}
