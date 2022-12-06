
using DO;
using DalApi;

namespace Dal;

internal class DalOrder : IOrder
{
    public int Add (Order order) 
    {
        order.ID = DataSource.Config.NextOrderNumber;
        DataSource.OrderList.Add(order);
        return order.ID;
    }

    public Order GetById(int id) 
    {
        if(! DataSource.OrderList.Exists(x=>x?.ID == id))
        {
            throw new DalDoesNotExistException("order not exists"); 
        }
        return (Order)DataSource.OrderList.Find(x => x?.ID == id);
    }

    public void Update(Order order)
    {
        if (!DataSource.OrderList.Exists(x => x?.ID == order.ID))
        {
            throw new DalDoesNotExistException("order not exists"); 
        }
        DataSource.OrderList.Remove(DataSource.OrderList.Find(x => x?.ID == order.ID));
        DataSource.OrderList.Add(order);
    }

    public void Delete(int id) 
    {
        if (!DataSource.OrderList.Exists(x => x?.ID == id))
        {
            throw new DalDoesNotExistException("order not exists");
        }
        DataSource.OrderList.Remove(DataSource.OrderList.Find(x => x?.ID == id));
    }

    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
    {
        if (filter == null)
        {
            return from Order? order in DataSource.OrderList
                   select order;
        }
        else
        {
            return from Order? order in DataSource.OrderList
                   where filter(order)
                   select order;
        }
    }
}
