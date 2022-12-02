using System;
using System.Collections.Generic;
using System.Linq;
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
                ID = doProduct?.ID ??  BO.NullPropertyException("Null producr ID"),
                Category = (BO.Category?)doProduct?.Category ?? throw new BO.BlWrongCategoryException("Product does not exist"),
                Price = doProduct?.Price ?? 0,
                Name = doProduct?.Name ?? "",
                StockCount = doProduct?.InStock ?? 0
            };
        }
        catch(DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Product already Exist", ex);
        }
    }
    public void AddProduct(BO.Product boProduct)
    {
        DO.Product doProduct = new DO.Product();
        if (boProduct.ID <= 0)
            throw new ArgumentException("Invalid ID");
        else
        {
            try
            {
                doProduct.ID = boProduct.ID;
            }
            catch (DO.DalAlreadyExistException daee)
            {
                Console.WriteLine("{0} Exception caught.", daee);
            }
        }
        if (boProduct.Name == null)
            throw new ArgumentException("Invalid name");
        else
            doProduct.Name = boProduct.Name;
        if (boProduct.Price <= 0)
            throw new ArgumentException("Invalid price");
        else
            doProduct.Price = boProduct.Price;
        if (boProduct.StockCount < 0)
            throw new ArgumentException("Invalid stock amount");
        else
            doProduct.InStock = boProduct.StockCount;
        doProduct.Category = (DO.Category)boProduct.Category;
        dal.Product.Add(doProduct);

    }
    public void RemoveProduct(int idProduct)
    {
        IEnumerable<DO.Order?> doOrders = dal.Order.GetAll();
        //from DO.OrderItem? doItem in dal.OrderItem.getAllOrderItems(doOrder.Value.ID)
        //where doItem.Value.ProductID == idProduct
        //select;

    }

}
