using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

internal class Cart : BlApi.ICart
{
    DalApi.IDal? dal = DalApi.Factory.Get();
    //int orderItemIDs = 0;
    /// <summary>
    /// function for adding an item to the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productID"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlProductDoesNotExsist"></exception>
    public void AddItemToCart(BO.Cart cart, int productID, int amnt)
    {
        DO.Product product;
        try
        {
            product = dal?.Product.GetById(productID) ?? throw new NullReferenceException("id does not exist");
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlProductDoesNotExsist("Product not Exist", ex);
        }
        if (cart == null)
            cart = new BO.Cart() { Items = new List<BO.OrderItem>()};
        if (cart.Items == null)
            cart.Items = new List<BO.OrderItem>();
        IEnumerable<BO.OrderItem?> oi = from BO.OrderItem? o in cart.Items where o.ProductID == productID select o;
        if (!oi.Any())
        {
            if (product.InStock > 0)
            {
                cart.Items = cart.Items.Append(new BO.OrderItem
                {
                    Name = product.Name,
                    Amount = amnt,
                    ProductID = productID,
                    ID = cart.Items.Count(),
                    Price = product.Price,
                    TotalPrice = product.Price * amnt,
                });
                cart.TotalPrice += product.Price * amnt;
            }
            else
                throw new BO.BlIdAlreadyExistException("The product amount in stock is 0");
        }
        else
        {
            UpdateCart(cart, productID, oi.First().Amount+amnt );
        }
    }

    public IEnumerable<BO.OrderItem> RemoveOrderItem(BO.Cart cart, int productID)
    {
        IEnumerable<BO.OrderItem?> oi = from BO.OrderItem? o in cart.Items
                                        where o.ProductID == productID
                                        select o;
        cart.TotalPrice -= oi.First().TotalPrice;
        cart.Items = from BO.OrderItem boOrderItem in cart.Items
                     where boOrderItem.ProductID != productID
                     select boOrderItem;
        return from BO.OrderItem boOrderItem in cart.Items
               where boOrderItem.ProductID != productID
               select boOrderItem;

    }

    /// <summary>
    /// function for update the amount of the items in cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productID"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlProductDoesNotExsist"></exception>
    public IEnumerable<BO.OrderItem> UpdateCart(BO.Cart cart, int productID, int amount)
    {
        DO.Product product;
        try
        {
            product = dal?.Product.GetById(productID) ?? throw new NullReferenceException("id does not exist");
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
            //return cart;
        }
        else
        {
            cart.Items = from BO.OrderItem boOrderItem in cart.Items
                         where boOrderItem.ProductID != productID
                         select boOrderItem;
            //return cart;
        }
        return cart.Items;
    }

    /// <summary>
    /// function for paid and make order
    /// </summary>
    /// <param name="cart"></param>
    /// <exception cref="BO.BlClientDeatalesNotValid"></exception>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    public int MakeOrder(BO.Cart cart)
    {
        if (cart.CustomerName == "" || cart.CustomerAddress == "" || cart.CustomerEmail == "")
            throw new BO.BlClientDeatalesNotValid("Invalid client details");
        DO.Order order=new DO.Order() { CustomerName = cart.CustomerName ,CustomerEmail=cart.CustomerEmail,CustomerAdress=cart.CustomerAddress,
        OrderDate=DateTime.Now,DeliveryDate=null,ShipDate=null};
        int oID = dal?.Order.Add(order) ?? throw new NullReferenceException("id does not exist");
        DO.Product p = new DO.Product();
        if (!cart.Items.Any())
            throw new BO.BlClientDeatalesNotValid("There is no items");
        foreach (BO.OrderItem oi in cart.Items)
        {
            p = dal.Product.GetById(oi.ProductID);
            if (p.InStock == 0)
                throw new BO.BlClientDeatalesNotValid("the item is not in stock");
            dal.OrderItem.Add(new DO.OrderItem() { OrderID = oID, ProductID = oi.ProductID, Amount = oi.Amount, Price = oi.Price });
            //p=dal.Product.GetById(oi.ProductID);
            p.InStock -= oi.Amount;
            dal.Product.Update(p);
        }
        //((List<BO.OrderItem?>)(cart.Items)).Clear();
        cart.TotalPrice = 0;
        cart.CustomerAddress = "";
        cart.CustomerEmail = "";
        cart.CustomerName = "";
        return oID;
    }
}
