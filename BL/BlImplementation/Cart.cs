using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using BlApi;
//using BO;
//using Dal;
//using DalApi;

namespace BlImplementation;

internal class Cart : BlApi.ICart
{
    DalApi.IDal dal = new Dal.DalList();

    public BO.Cart AddItemToCart(BO.Cart cart, int productID)
    {
        DO.Product product;
        try
        {
            product = dal.Product.GetById(productID);
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Product not Exist", ex);
        }
        IEnumerable<BO.OrderItem?> oi = from BO.OrderItem? o in cart.Items where o.ProductID == productID select o;
        if (!oi.Any())
        {
            if (product.InStock > 0)
            {
                cart.Items = cart.Items.Append(new BO.OrderItem
                {
                    Name = product.Name,
                    Amount = 1,
                    ProductID = productID,
                    ID = 0,
                    Price = product.Price,
                    TotalPrice = product.Price,
                });
                cart.TotalPrice += product.Price;
            }
        }
        else
        {
            cart = UpdateCart(cart, productID, 1);
        }
        return cart;
    }

    public BO.Cart UpdateCart(BO.Cart cart, int productID, int amount)
    {
        DO.Product product;
        try
        {
            product = dal.Product.GetById(productID);
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Product not Exist", ex);
        }
        if (amount != 0)
        {
            IEnumerable<BO.OrderItem?> boOrder = from BO.OrderItem boOrderItem in cart.Items
                                                 where boOrderItem.ProductID == productID
                                                 select new BO.OrderItem
                                                 {
                                                     ID = boOrderItem.ID,
                                                     Name = boOrderItem.Name,
                                                     Price = boOrderItem.Price,
                                                     ProductID = productID,
                                                     Amount = amount,
                                                     TotalPrice = amount * boOrderItem.Price,
                                                 };
            if (boOrder.Any())
            {
                //cart.Items = from BO.OrderItem boOrderItem in boOrder
                //             select new BO.OrderItem
                //             {
                //                 ID = boOrderItem.ID,
                //                 Name = boOrderItem.Name,
                //                 Price = boOrderItem.Price,
                //                 ProductID = productID,
                //                 Amount = amount,
                //                 TotalPrice = amount * boOrderItem.Price,
                //             };
            }
            cart.TotalPrice -= (from BO.OrderItem boOrderItem in boOrder select boOrderItem.TotalPrice).Sum();
            cart.TotalPrice += amount * (from BO.OrderItem boOrderItem in boOrder select boOrderItem.Price).Sum();
            return cart;
        }
        else
        {
            cart.Items = from BO.OrderItem boOrderItem in cart.Items
                         where boOrderItem.ProductID != productID
                         select boOrderItem;
            return cart;
        }
    }
    public void MakeOrder(BO.Cart cart)
    {
        if (cart.CustomerName == "" || cart.CustomerAddress == "" || cart.CustomerEmail == "")
            throw new BO.BlClientDeatalesNotValid("Invalid client details");
        DO.Order order=new DO.Order() { CustomerName = cart.CustomerName ,CustomerEmail=cart.CustomerEmail,CustomerAdress=cart.CustomerAddress,
        OrderDate=DateTime.Now,DeliveryDate=null,ShipDate=null};
        int oID = dal.Order.Add(order);
        DO.Product p = new DO.Product();
        foreach (BO.OrderItem oi in cart.Items)
        {
            if (p.InStock == 0)
                throw new BO.BlNullPropertyException("the item is not in stock");
            dal.OrderItem.Add(new DO.OrderItem() { OrderID = oID, ProductID = oi.ProductID, Amount = oi.Amount });
            p=dal.Product.GetById(oi.ProductID);
            p.InStock = oi.Amount;
            dal.Product.Update(p);
        }

    }
}
