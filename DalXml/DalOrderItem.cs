using DalApi;
//using DalXml;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    readonly string s_orderItems = "orderItems";
    public int Add(OrderItem orderItem)
    {
        List<OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<OrderItem>(s_orderItems);
        orderItem.ID = Config.NextOrderItemNumber();
        orderItemList.Add(orderItem);
        Config.premotOrderItemNumber(orderItem.ID + 1);
        XMLTools.SaveListToXMLSerializer(orderItemList, s_orderItems);
        return orderItem.ID;
    }

    public void Delete(int id)
    {
        List<OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<OrderItem>(s_orderItems);
        if (!orderItemList.Exists(x => x?.ID == id))
        {
            throw new DalDoesNotExistException("order item not exists");
        }
        orderItemList.Remove(orderItemList.Find(x => x?.ID == id));
        XMLTools.SaveListToXMLSerializer(orderItemList, s_orderItems);
    }

    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
    {
        List<OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<OrderItem>(s_orderItems);
        if (filter == null)
        {
            return from OrderItem? orderItem in orderItemList
                   select orderItem;
        }
        else
        {
            return from OrderItem? orderItem in orderItemList
                   where filter(orderItem)
                   select orderItem;
        }
    }

    public List<OrderItem?> getAllOrderItems(int id)
    {
        List<OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<OrderItem>(s_orderItems);
        List<OrderItem?> newList = new List<OrderItem?>();
        for (int i = 0; i < orderItemList.Count; i++)
        {
            if (orderItemList[i]?.OrderID == id)
                newList.Add(orderItemList[i]);
        }
        return newList;
    }

    public OrderItem GetById(int id)
    {
        List<OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<OrderItem>(s_orderItems);
        if (!orderItemList.Exists(x => x?.ID == id))
        {
            throw new DalDoesNotExistException("order item not exists");
        }
        return (OrderItem)orderItemList.Find(x => x?.ID == id);
    }

    public OrderItem? getOrderItems(int idProduct, int idOrder)
    {
        List<OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<OrderItem>(s_orderItems);
        OrderItem? orderItem = null;
        for (int i = 0; i < orderItemList.Count; i++)
        {
            if (orderItemList[i]?.OrderID == idOrder && orderItemList[i]?.ProductID == idProduct)
                orderItem = orderItemList[i];
        }
        if (orderItem == null)
        {
            throw new DalDoesNotExistException("order item not exists");
        }
        return orderItem;
    }

    public void Update(OrderItem orderItem)
    {
        List<OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<OrderItem>(s_orderItems);
        if (!orderItemList.Exists(x => x?.ID == orderItem.ID))
        {
            throw new DalDoesNotExistException("order item not exists");
        }
        orderItemList.Remove(orderItemList.Find(x => x?.ID == orderItem.ID));
        orderItemList.Add(orderItem);
        orderItemList.Sort((x, y) => x.Value.ID.CompareTo(y.Value.ID));
        XMLTools.SaveListToXMLSerializer(orderItemList, s_orderItems);
    }
}
