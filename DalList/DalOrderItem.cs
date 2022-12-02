
using DO;
using DalApi;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    public int Add(OrderItem orderItem)
    {
        orderItem.ID = DataSource.Config.NextOrderItemNumber;
        DataSource.OrderItemsList.Add(orderItem);
        return orderItem.ID;
    }

    public OrderItem GetById(int id)
    {
        if (!DataSource.OrderItemsList.Exists(x => x?.ID == id))
        {
            throw new DalDoesNotExistException("order item not exists");
        }
        return (OrderItem)DataSource.OrderItemsList.Find(x => x?.ID == id);
    }

    public void Update(OrderItem orderItem)
    {
        if (!DataSource.OrderItemsList.Exists(x => x?.ID == orderItem.ID))
        {
            throw new DalDoesNotExistException("order item not exists");
        }
        DataSource.OrderItemsList.Remove(DataSource.OrderItemsList.Find(x => x?.ID == orderItem.ID));
        DataSource.OrderItemsList.Add(orderItem);
    }

    public void Delete(int id)
    {
        if (!DataSource.OrderItemsList.Exists(x => x?.ID == id))
        {
            throw new DalDoesNotExistException("order item not exists");
        }
        DataSource.OrderItemsList.Remove(DataSource.OrderItemsList.Find(x => x?.ID == id));
    }

    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
    {
        List<OrderItem?> newList = new List<OrderItem?>();
        for (int i = 0; i < DataSource.OrderItemsList.Count; i++)
        {
            newList.Add(DataSource.OrderItemsList[i]);
        }
        return newList;
    }

    public List<OrderItem?> getAllOrderItems(int id)
    {
        List<OrderItem?> newList = new List<OrderItem?>();
        for (int i = 0; i < DataSource.OrderItemsList.Count; i++)
        {
            if(DataSource.OrderItemsList[i]?.OrderID == id)
                newList.Add(DataSource.OrderItemsList[i]);
        }
        return newList;
    }

    public OrderItem? getOrderItems(int idProduct, int idOrder)
    {
        OrderItem? orderItem = null;
        for (int i = 0; i < DataSource.OrderItemsList.Count; i++)
        {
            if (DataSource.OrderItemsList[i]?.OrderID == idOrder && DataSource.OrderItemsList[i]?.ProductID == idProduct)
                orderItem = DataSource.OrderItemsList[i];
        }
        if (orderItem == null)
            throw new DalDoesNotExistException("order item not exists");
        return orderItem;
    }
}
