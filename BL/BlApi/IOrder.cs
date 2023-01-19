using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// function for get list of all the orders (for the manager)
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList?> OrderListForManager();

    /// <summary>
    /// function for get order by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order GetById(int id);

    /// <summary>
    /// function for get all the details of the order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order OrderDeatals(int id);

    /// <summary>
    /// function for update the ship date of order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order OrderShipUpdate(int id);

    /// <summary>
    /// function for update delivery date of order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order OrderDeliveryUpdate(int id);

    /// <summary>
    /// function for tracking order
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    public BO.OrderTracking TrackingOrder(int orderID);

    /// <summary>
    /// u[date client details
    /// </summary>
    /// <param name="o"></param>
    public void UpdateDeatails(BO.Order o);
}
