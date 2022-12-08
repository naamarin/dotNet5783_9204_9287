using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using BO;
using System.Security.Cryptography;
using System.Linq.Expressions;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    DalApi.IDal dal = new Dal.DalList();
    public BO.OrderStatus Statu(DO.Order? o)
    {
        if (o?.DeliveryDate == null)
        {
            if (o?.ShipDate == null)
                return BO.OrderStatus.Ordered;
            else
                return BO.OrderStatus.Shipped;
        }
        else
            return BO.OrderStatus.Delivered;
    }

    public BO.Order GetById(int id)
    {
        DO.Order order;
        try
        {
            order = dal.Order.GetById(id);
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.BlOrderDoesNotExsist("No such order", ex);
        }
        IEnumerable<BO.OrderItem?> boItems = from DO.OrderItem doOrderItem in dal.OrderItem.getAllOrderItems(id)
                                             select new BO.OrderItem()
                                             {
                                                 ID =doOrderItem.ID,
                                                 Name = (dal.Product.GetById(doOrderItem.ProductID)).Name,
                                                 ProductID = doOrderItem.ProductID,
                                                 Amount = doOrderItem.Amount,
                                                 Price = doOrderItem.Price,
                                                 TotalPrice = doOrderItem.Amount* doOrderItem.Price,
                                             };
        IEnumerable<double> sum = from BO.OrderItem bo in boItems  
                                  select bo.TotalPrice;
        return new BO.Order
        {
            ID = id,
            CustomerName = order.CustomerName,
            CustomerAddress = order.CustomerAdress,
            CustomerEmail = order.CustomerEmail,
            ShipDate = order.ShipDate,
            DeliveryDate = order.DeliveryDate,  
            Status = Statu(order),
            OrderDate = order.OrderDate,
            PaymentDate = order.OrderDate,
            Items = boItems,
            TotalPrice = sum.Sum(),
        };
    }

    public IEnumerable<BO.OrderForList?> OrderListForManager()
    {
        IEnumerable<BO.OrderForList?> odl2 = from DO.Order? doOrders in dal.Order.GetAll()
                                             select new BO.OrderForList
                                             {
                                                 ID = doOrders.Value.ID,
                                                 CustomerName = doOrders.Value.CustomerName,
                                                 AmountOfItems = dal.OrderItem.getAllOrderItems(doOrders.Value.ID).Count(),
                                                 Status = Statu(doOrders),
                                                TotalPrice = (from DO.OrderItem oi in dal.OrderItem.getAllOrderItems(doOrders.Value.ID) select oi.Price*oi.Amount).Sum(),
                                            };
        return odl2;
    }
    public BO.Order OrderDeatals(int id)
    {
        if (id <= 0)
            throw new BO.BlNullPropertyException("Invalid ID");
        DO.Order? o = new DO.Order?();
        try
        {
            o = dal.Order.GetById(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlOrderDoesNotExsist("There is no order wuth this ID", ex);
        }
        IEnumerable<BO.OrderItem?> list = from DO.OrderItem oi in dal.OrderItem.getAllOrderItems(id)
                                          select new BO.OrderItem
                                          {
                                              ID = oi.ID,
                                              Name = (dal.Product.GetById(oi.ProductID)).Name,
                                              ProductID = oi.ProductID,
                                              Price = oi.Price,
                                              TotalPrice = oi.Price * oi.Amount,
                                              Amount = oi.Amount,
                                          };
        return new BO.Order()
        {
            ID = o.Value.ID, //*****************************************
            CustomerName = o?.CustomerName,
            CustomerEmail = o?.CustomerEmail,
            CustomerAddress = o?.CustomerAdress,
            Items = list,
            ShipDate = o?.ShipDate,
            DeliveryDate = o?.DeliveryDate,
            OrderDate = o?.OrderDate,
            Status = Statu(o),
            TotalPrice = (from BO.OrderItem orIt in list select orIt.TotalPrice).Sum(),
            PaymentDate = o?.OrderDate,
        };
    }
    public BO.Order OrderShipUpdate(int id)
    {
        DO.Order? order;
        try
        {
            order = dal.Order.GetById(id);
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.BlOrderDoesNotExsist("The order does  not exists", ex);
        }
        BO.OrderStatus orderStatus = Statu(order);
        if (orderStatus != BO.OrderStatus.Shipped && orderStatus != BO.OrderStatus.Delivered)
        {
            DO.Order or = new DO.Order
            {
                ID = order.Value.ID,
                CustomerName = order.Value.CustomerName,
                CustomerAdress = order.Value.CustomerAdress,
                CustomerEmail = order.Value.CustomerEmail,
                CustomerNumber = order.Value.CustomerNumber,
                OrderDate = order.Value.OrderDate,
                ShipDate = DateTime.Now,
                DeliveryDate = order.Value.DeliveryDate,
            };
            dal.Order.Update(or);
            BO.Order boOrder = new BO.Order
            {
                ID = order.Value.ID,
                CustomerName = order.Value.CustomerName,
                CustomerAddress = order.Value.CustomerAdress,
                CustomerEmail = order.Value.CustomerEmail,
                OrderDate = order.Value.OrderDate,
                Status = Statu(order),
                ShipDate = DateTime.Now,
                DeliveryDate = null,
                PaymentDate = order.Value.OrderDate,
            };
            boOrder.Items = from DO.OrderItem doOrderItem in dal.OrderItem.getAllOrderItems(id)
                            select new BO.OrderItem
                            {
                                ID = doOrderItem.ID,
                                ProductID = doOrderItem.ProductID,
                                Name = (dal.Product.GetById(doOrderItem.ProductID)).Name,
                                Amount = doOrderItem.Amount,
                                Price = doOrderItem.Price,
                                TotalPrice = doOrderItem.Amount * doOrderItem.Price,
                            };
            boOrder.TotalPrice = (from BO.OrderItem boOrderItem in boOrder.Items select boOrderItem.TotalPrice).Sum();
            return boOrder;
        }
        else
            throw new BO.BlOrderAlreadyDelivered("Order already delivered");
    }
    public BO.Order OrderDeliveryUpdate(int id)
    {
        DO.Order? order;
        try
        {
            order = dal.Order.GetById(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlOrderDoesNotExsist("The order does  not exists", ex);
        }
        BO.OrderStatus orderStatus = Statu(order);
        if (orderStatus != BO.OrderStatus.Delivered)
        {
            DO.Order or = new DO.Order
            {
                ID = order.Value.ID,
                CustomerName = order.Value.CustomerName,
                CustomerAdress = order.Value.CustomerAdress,
                CustomerEmail = order.Value.CustomerEmail,
                CustomerNumber = order.Value.CustomerNumber,
                OrderDate = order.Value.OrderDate,
                ShipDate = order.Value.ShipDate,
                DeliveryDate = DateTime.Now,
            };
            dal.Order.Update(or);
            BO.Order boOrder = new BO.Order
            {
                ID = order.Value.ID,
                CustomerName = order.Value.CustomerName,
                CustomerAddress = order.Value.CustomerAdress,
                CustomerEmail = order.Value.CustomerEmail,
                OrderDate = order.Value.OrderDate,
                Status = Statu(order),
                ShipDate = order.Value.ShipDate,
                DeliveryDate = DateTime.Now,
                PaymentDate = order.Value.OrderDate,
            };
            boOrder.Items = from DO.OrderItem doOrderItem in dal.OrderItem.getAllOrderItems(id)
                            select new BO.OrderItem
                            {
                                ID = doOrderItem.ID,
                                ProductID = doOrderItem.ProductID,
                                Name = (dal.Product.GetById(doOrderItem.ProductID)).Name,
                                Amount = doOrderItem.Amount,
                                Price = doOrderItem.Price,
                                TotalPrice = doOrderItem.Amount * doOrderItem.Price,
                            };
            boOrder.TotalPrice = (from BO.OrderItem boOrderItem in boOrder.Items select boOrderItem.TotalPrice).Sum();
            return boOrder;
        }
        else
            throw new BO.BlOrderAlreadyDelivered("Order already delivered");
    }
    public BO.OrderTracking TrackingOrder(int orderID)
    {
        DO.Order? order;
        try
        {
            order = dal.Order.GetById(orderID);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlOrderDoesNotExsist("The order does  not exists", ex);
        }
        if (Statu(order) == BO.OrderStatus.Delivered)
        {
            return new BO.OrderTracking
            {
                ID = order.Value.ID,
                Status = Statu(order),
                Tracking = new List<Tuple<DateTime, string>> { new Tuple<DateTime, string>( order.Value.OrderDate.Value, "The order has been created" ),
                 new Tuple<DateTime, string>(order.Value.ShipDate.Value,"The order is sent"), new Tuple<DateTime, string>(order.Value.DeliveryDate.Value,"The order has been delivered") },

            };
        }
        else if (Statu(order) == BO.OrderStatus.Shipped)
        {
            return new BO.OrderTracking
            {
                ID = order.Value.ID,
                Status = Statu(order),
                Tracking = new List<Tuple<DateTime, string>> { new Tuple<DateTime, string>( order.Value.OrderDate.Value, "The order has been created" ),
                 new Tuple<DateTime, string>(order.Value.ShipDate.Value,"The order is sent") },

            };
        }
        else
        {
            return new BO.OrderTracking
            {
                ID = order.Value.ID,
                Status = Statu(order),
                Tracking = new List<Tuple<DateTime, string>> { new Tuple<DateTime, string>( order.Value.OrderDate.Value, "The order has been created" ) },
               
            };
        }
    }
}
