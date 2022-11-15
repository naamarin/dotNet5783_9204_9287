
using DO;

namespace Dal;

public class DalOrder
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
            throw new Exception("order not exists"); 
        }
        return (Order)DataSource.OrderList.Find(x => x?.ID == id);
    }

    public void Update(Order order)
    {
        if (!DataSource.OrderList.Exists(x => x?.ID == order.ID))
        {
            throw new Exception("order not exists"); 
        }
        DataSource.OrderList.Remove(DataSource.OrderList.Find(x => x?.ID == order.ID));
        DataSource.OrderList.Add(order);
    }

    public void Delete(int id) 
    {
        if (!DataSource.OrderList.Exists(x => x?.ID == id))
        {
            throw new Exception("order not exists");
        }
        DataSource.OrderList.Remove(DataSource.OrderList.Find(x => x?.ID == id));
    }

    public IEnumerable<Order?> GetAll()
    {
        List<Order?> newList = new List<Order?>();
        for (int i = 0; i < DataSource.OrderList.Count; i++)
        {
            newList.Add(DataSource.OrderList[i]);
        }
        return newList;
    }
}
