using DalApi;
using DalXml;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalOrder : IOrder
{
    readonly string s_orders = "orders";
    public int Add(Order order)
    {
        List<Order?> orderList = XMLTools.LoadListFromXMLSerializer<Order>(s_orders);
        order.ID = Config.NextOrderNumber();
        orderList.Add(order);
        Config.premotOrderNumber(order.ID + 1);
        XMLTools.SaveListToXMLSerializer(orderList, s_orders);
        return order.ID;
    }

    public void Delete(int id)
    {
        List<Order?> orderList = XMLTools.LoadListFromXMLSerializer<Order>(s_orders);
        if (!orderList.Exists(x => x?.ID == id))
        {
            throw new DalDoesNotExistException("order not exists");
        }
        orderList.Remove(orderList.Find(x => x?.ID == id));
        XMLTools.SaveListToXMLSerializer(orderList, s_orders);
    }

    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
    {
        List<Order?> orderList = XMLTools.LoadListFromXMLSerializer<Order>(s_orders);
        if (filter == null)
        {
            return from Order? order in orderList
                   select order;
        }
        else
        {
            return from Order? order in orderList
                   where filter(order)
                   select order;
        }
    }

    public Order GetById(int id)
    {
        List<Order?> orderList = XMLTools.LoadListFromXMLSerializer<Order>(s_orders);
        if (!orderList.Exists(x => x?.ID == id))
        {
            throw new DalDoesNotExistException("order not exists");
        }
        return (Order)orderList.Find(x => x?.ID == id);
    }

    public void Update(Order order)
    {
        List<Order?> orderList = XMLTools.LoadListFromXMLSerializer<Order>(s_orders);
        if (!orderList.Exists(x => x?.ID == order.ID))
        {
            throw new DalDoesNotExistException("order not exists");
        }
        orderList.Remove(orderList.Find(x => x?.ID == order.ID));
        orderList.Add(order);
    }
}
