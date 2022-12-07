using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IOrder
{
    public IEnumerable<BO.OrderForList?> OrderListForManager();
    public BO.Order GetById(int id);
    public BO.Order OrderDeatals(int id);
    public BO.Order OrderShipUpdate(int id);
    public BO.Order OrderDeliveryUpdate(int id);
    public BO.OrderTracking TrackingOrder(int orderID);
}
