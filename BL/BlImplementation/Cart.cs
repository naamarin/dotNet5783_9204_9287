using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

internal class Cart : BlApi.ICart
{
    DalApi.IDal dal = new Dal.DalList();
    /// <summary>
    /// function for adding an item to the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productID"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlProductDoesNotExsist"></exception>
    public BO.Cart AddItemToCart(BO.Cart cart, int productID)
    {
        DO.Product product;
        try
        {
            product = dal.Product.GetById(productID);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlProductDoesNotExsist("Product not Exist", ex);
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
            cart = UpdateCart(cart, productID, oi.First().Amount+1 );
        }
        return cart;
    }

    /// <summary>
    /// function for update the amount of the items in cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productID"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlProductDoesNotExsist"></exception>
    public BO.Cart UpdateCart(BO.Cart cart, int productID, int amount)
    {
        DO.Product product;
        try
        {
            product = dal.Product.GetById(productID);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlProductDoesNotExsist("Product not Exist", ex);
        }
        if (amount != 0)
        {
            cart.TotalPrice -= (from BO.OrderItem boOrderItem in cart.Items where boOrderItem.ProductID == productID select boOrderItem.TotalPrice).Sum();
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
                cart.Items = from BO.OrderItem boOrderItem in cart.Items
                             where boOrderItem.ProductID != productID
                             select boOrderItem;
                cart.Items = cart.Items.Append(boOrder.First());
            }
            else
            {
                cart.Items = cart.Items.Append(new BO.OrderItem
                {
                    ID = 0,
                    Name = product.Name,
                    Price = product.Price,
                    ProductID = productID,
                    Amount = amount,
                    TotalPrice = amount * product.Price,
                });
            }
            cart.TotalPrice += amount*product.Price;
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

    /// <summary>
    /// function for paid and make order
    /// </summary>
    /// <param name="cart"></param>
    /// <exception cref="BO.BlClientDeatalesNotValid"></exception>
    /// <exception cref="BO.BlNullPropertyException"></exception>
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
