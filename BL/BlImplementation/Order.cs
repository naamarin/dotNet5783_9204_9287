using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Linq.Expressions;
using BO;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    DalApi.IDal dal = new Dal.DalList();

    /// <summary>
    /// a temp function for getting the status of the order
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
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
    /// <summary>
    /// function for get order by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlOrderDoesNotExsist"></exception>

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

    /// <summary>
    /// function for get list of all the orders (for the manager)
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList?> OrderListForManager()
    {
        IEnumerable<BO.OrderForList?> odl2 = from DO.Order doOrders in dal.Order.GetAll()
                                             select new BO.OrderForList
                                             {
                                                 ID = doOrders.ID,
                                                 CustomerName = doOrders.CustomerName,
                                                 AmountOfItems = dal.OrderItem.getAllOrderItems(doOrders.ID).Count(),
                                                 Status = Statu(doOrders),
                                                TotalPrice = (from DO.OrderItem oi in dal.OrderItem.getAllOrderItems(doOrders.ID) select oi.Price*oi.Amount).Sum(),
                                            };
        return odl2;
    }

    /// <summary>
    /// function for get all the details of the order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    /// <exception cref="BO.BlOrderDoesNotExsist"></exception>
    /// <exception cref="BlOrderId"></exception>
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
            ID = o?.ID ?? throw new BlOrderId("Order does not exist"),
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

    /// <summary>
    /// function for update the ship date of order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlOrderDoesNotExsist"></exception>
    /// <exception cref="BlOrderId"></exception>
    /// <exception cref="BO.BlOrderAlreadyDelivered"></exception>
    public BO.Order OrderShipUpdate(int id)
    {
        DO.Order? order;
        try
        {
            order = dal.Order.GetById(id);
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.BlOrderDoesNotExsist("The order does not exists", ex);
        }
        BO.OrderStatus orderStatus = Statu(order);
        if (orderStatus != BO.OrderStatus.Shipped && orderStatus != BO.OrderStatus.Delivered)
        {
            DO.Order or = new DO.Order
            {
                ID = order?.ID ?? throw new BlOrderId("Order does not exist"),
                CustomerName = order?.CustomerName,
                CustomerAdress = order?.CustomerAdress,
                CustomerEmail = order?.CustomerEmail,
                CustomerNumber = order?.CustomerNumber ?? throw new BlOrderId("Custumer number invalid"),
                OrderDate = order?.OrderDate,
                ShipDate = DateTime.Now,
                DeliveryDate = order?.DeliveryDate,
            };
            dal.Order.Update(or);
            BO.Order boOrder = new BO.Order
            {
                ID = order?.ID ?? throw new BlOrderId("Order does not exist"),
                CustomerName = order?.CustomerName,
                CustomerAddress = order?.CustomerAdress,
                CustomerEmail = order?.CustomerEmail,
                OrderDate = order?.OrderDate,
                Status = Statu(order),
                ShipDate = DateTime.Now,
                DeliveryDate = null,
                PaymentDate = order?.OrderDate,
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

    /// <summary>
    /// function for update delivery date of order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlOrderDoesNotExsist"></exception>
    /// <exception cref="BlOrderId"></exception>
    /// <exception cref="BO.BlOrderAlreadyDelivered"></exception>
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
                ID = order?.ID ?? throw new BlOrderId("Order does not exist"),
                CustomerName = order?.CustomerName,
                CustomerAdress = order?.CustomerAdress,
                CustomerEmail = order?.CustomerEmail,
                CustomerNumber = order?.CustomerNumber ?? throw new BlOrderId ("Custumer number invalid"),
                OrderDate = order?.OrderDate,
                ShipDate = order?.ShipDate,
                DeliveryDate = DateTime.Now,
            };
            dal.Order.Update(or);
            BO.Order boOrder = new BO.Order
            {
                ID = order?.ID ?? throw new BlOrderId("Order does not exist"),
                CustomerName = order?.CustomerName,
                CustomerAddress = order?.CustomerAdress,
                CustomerEmail = order?.CustomerEmail,
                OrderDate = order?.OrderDate,
                Status = Statu(order),
                ShipDate = order?.ShipDate,
                DeliveryDate = DateTime.Now,
                PaymentDate = order?.OrderDate,
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

    /// <summary>
    /// function for tracking order
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlOrderDoesNotExsist"></exception>
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
                Tracking = new List<Tuple<DateTime?, string>> { new Tuple<DateTime ?, string>( order?.OrderDate, "The order has been created" ),
                 new Tuple<DateTime ?, string>(order?.ShipDate,"The order is sent"), new Tuple<DateTime ?, string>(order?.DeliveryDate,"The order has been delivered") },

            };
        }
        else if (Statu(order) == BO.OrderStatus.Shipped)
        {
            return new BO.OrderTracking
            {
                ID = order.Value.ID,
                Status = Statu(order),
                Tracking = new List<Tuple<DateTime?, string>> { new Tuple<DateTime ?, string>( order?.OrderDate, "The order has been created" ),
                 new Tuple<DateTime ?, string>(order?.ShipDate,"The order is sent") },

            };
        }
        else
        {
            return new BO.OrderTracking
            {
                ID = order.Value.ID,
                Status = Statu(order),
                Tracking = new List<Tuple<DateTime?, string>> { new Tuple<DateTime?, string>( order?.OrderDate , "The order has been created" ) },
               
            };
        }
    }
}
