using DO;
using DalApi;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// Function to create a product on order
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    public int Add(OrderItem orderItem)
    {
        orderItem.ID = DataSource.Config.NextOrderItemNumber;
        DataSource.OrderItemsList.Add(orderItem);
        return orderItem.ID;
    }

    /// <summary>
    /// Function to return a product in an order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception">throw Exception when id is not found</exception>
    public OrderItem GetById(int id)
    {
        if (!DataSource.OrderItemsList.Exists(x => x?.ID == id))
        {
            throw new DalDoesNotExistException("order item not exists");
        }
        return (OrderItem)DataSource.OrderItemsList.Find(x => x?.ID == id);
    }

    /// <summary>
    /// A function to update a product in an order
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    /// <exception cref="Exception">throw Exception when id is not found</exception>
    public void Update(OrderItem orderItem)
    {
        if (!DataSource.OrderItemsList.Exists(x => x?.ID == orderItem.ID))
        {
            throw new DalDoesNotExistException("order item not exists");
        }
        DataSource.OrderItemsList.Remove(DataSource.OrderItemsList.Find(x => x?.ID == orderItem.ID));
        DataSource.OrderItemsList.Add(orderItem);
    }

    /// <summary>
    /// Function to delete a product from the order
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception">throw Exception when id is not found</exception>
    public void Delete(int id)
    {
        if (!DataSource.OrderItemsList.Exists(x => x?.ID == id))
        {
            throw new DalDoesNotExistException("order item not exists");
        }
        DataSource.OrderItemsList.Remove(DataSource.OrderItemsList.Find(x => x?.ID == id));
    }

    /// <summary>
    /// Function to return all products in the order
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
    {
        if (filter == null)
        {
            return from OrderItem? orderItem in DataSource.OrderItemsList
                   select orderItem;
        }
        else
        {
            return from OrderItem? orderItem in DataSource.OrderItemsList
                   where filter(orderItem)
                   select orderItem;
        }
    }

    /// <summary>
    /// function for get all the items in the order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// function for get items of the order
    /// </summary>
    /// <param name="idProduct"></param>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    /// <exception cref="DalDoesNotExistException"></exception>
    public OrderItem? getOrderItems(int idProduct, int idOrder)
    {
        OrderItem? orderItem = null;
        for (int i = 0; i < DataSource.OrderItemsList.Count; i++)
        {
            if (DataSource.OrderItemsList[i]?.OrderID == idOrder && DataSource.OrderItemsList[i]?.ProductID == idProduct)
                orderItem = DataSource.OrderItemsList[i];
        }
        if (orderItem == null)
        {
            throw new DalDoesNotExistException("order item not exists");
        }
        return orderItem;
    }
}
