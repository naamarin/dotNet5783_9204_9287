using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlApi;
using BO;

namespace BlImplementation;

internal class Order : IOrder
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
            throw new BlNullPropertyException("Invalid ID");
        DO.Order? o = new DO.Order?();
        try
        {
            DO.Order? o = dal.Order.GetById(id);
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.BlOrderDoesNotExsist("There is no order wuth this ID", ex);
        }
        IEnumerable<BO.OrderItem?> list = from DO.OrderItem oi in dal.OrderItem.getAllOrderItems(id)
                                   select new BO.OrderItem
                                   {
                                       ID=oi.ID,
                                       Name = (dal.Product.GetById(oi.ProductID)).Name,
                                       ProductID = oi.ProductID,
                                       Price = oi.Price,
                                       TotalPrice = oi.Price * oi.Amount,
                                       Amount = oi.Amount,
                                   };
        return new BO.Order()
        {
            ID = o.Value.ID,
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
}
