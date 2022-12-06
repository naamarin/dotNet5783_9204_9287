using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using BlApi;
using BO;
using DO;

namespace BlImplementation;

internal class Product : IProduct
{
    DalApi.IDal dal = new Dal.DalList();
    public IEnumerable<BO.ProductForList> GetListProducts()
    {
        return from DO.Product? doProduct in dal.Product.GetAll()
               select new BO.ProductForList
               {
                   ID = doProduct?.ID ?? throw new NullReferenceException("Missing ID"),
                   Name = doProduct?.Name ?? throw new NullReferenceException("Missing name"),
                   Category = (BO.Category?)doProduct?.Category ?? throw new NullReferenceException("Missing category"),
                   Price = doProduct?.Price ?? 0
               };
    }
    public BO.Product GetById(int id)
    {
        try
        {
            DO.Product? doProduct = dal.Product.GetById(id);
            return new BO.Product()
            {
                ID = doProduct?.ID ?? throw new BO.BlNullPropertyException("Null producr ID"),
                Category = (BO.Category?)doProduct?.Category ?? throw new BO.BlWrongCategoryException("Product does not exist"),
                Price = doProduct?.Price ?? 0,
                Name = doProduct?.Name ?? "",
                StockCount = doProduct?.InStock ?? 0
            };
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Product already Exist", ex);
        }
    }
    public void AddProduct(BO.Product? boProduct)
    {
        DO.Product doProduct = new DO.Product()
        {
            ID = boProduct?.ID ?? throw new ArgumentException("Invalid ID"),
            Name = boProduct?.Name ?? throw new ArgumentException("Invalid name"),
            Price = boProduct?.Price ?? throw new ArgumentException("Invalid price"),
            InStock = boProduct?.StockCount ?? throw new ArgumentException("Invalid stock amount"),
        };
        dal.Product.Add(doProduct);
    }

    //        if (boProduct.ID <= 0)
    //            throw new ArgumentException("Invalid ID");
    //        else
    //        {
    //            try
    //            {
    //                doProduct.ID = boProduct.ID;
    //            }
    //            catch (DO.DalAlreadyExistException daee)
    //{
    //    Console.WriteLine("{0} Exception caught.", daee);
    //}

    public void RemoveProduct(int idProduct)
    {
        //***************************************************************************************

        IEnumerable<DO.OrderItem> odl = from DO.Order? doOrders in dal.Order.GetAll() from List<DO.OrderItem?> t in dal.OrderItem.getAllOrderItems(doOrders.Value.ID) from DO.OrderItem g in t where g.ProductID == idProduct select g;
        //List<DO.OrderItem?> orderItems = from o in doOrders select from t in dal.OrderItem.getAllOrderItems(o.Value.ID) where t.Value.ProductID == idProduct select;
        if (odl.Count() > 0)
            dal.Product.Delete(idProduct);
    }

    public void UpdateProduct(BO.Product boProduct)
    {
        if (boProduct.ID < 0)
            throw new ArgumentException("Invalid ID");
        if (boProduct.Name != "")
            throw new ArgumentException("Invalid name");
        if (boProduct.Price > 0)
            throw new ArgumentException("Invalid price");
        if (boProduct.StockCount >= 0)
            throw new ArgumentException("Invalid stock amount");
        DO.Product doProduct = new DO.Product()
        {
            ID = boProduct.ID, //?? throw new ArgumentException("Invalid ID"),
            Name = boProduct.Name,// ?? throw new ArgumentException("Invalid name"),
            Price = boProduct.Price,// ?? throw new ArgumentException("Invalid price"),
            InStock = boProduct.StockCount,// ?? throw new ArgumentException("Invalid stock amount"),
        };
        try
        {
            dal.Product.Update(doProduct);
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.BlProductDoesNotExsist("Product canot be upgrade because its not exsist", ex);
        }
    }

}
