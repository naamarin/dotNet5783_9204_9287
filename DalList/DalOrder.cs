
using DO;

namespace Dal;

public class DalOrder
{
    /// <summary>
    /// /// <summary>
    /// Function to create a new order
    /// </summary>
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public int Add (Order order) 
    {
        order.ID = DataSource.Config.NextOrderNumber;
        DataSource.OrderList.Add(order);
        return order.ID;
    }
    /// <summary>
    /// A function to return an existing order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order GetById(int id) 
    {
        if(! DataSource.OrderList.Exists(x=>x?.ID == id))
        {
            throw new Exception("order not exists"); 
        }
        return (Order)DataSource.OrderList.Find(x => x?.ID == id);
    }
    /// <summary>
    /// Order update function
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order order)
    {
        if (!DataSource.OrderList.Exists(x => x?.ID == order.ID))
        {
            throw new Exception("order not exists"); 
        }
        DataSource.OrderList.Remove(DataSource.OrderList.Find(x => x?.ID == order.ID));
        DataSource.OrderList.Add(order);
    }
    /// <summary>
    /// Function to delete an existing order
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id) 
    {
        if (!DataSource.OrderList.Exists(x => x?.ID == id))
        {
            throw new Exception("order not exists");
        }
        DataSource.OrderList.Remove(DataSource.OrderList.Find(x => x?.ID == id));
    }
    /// <summary>
    /// Function to return all orders
    /// </summary>
    /// <returns></returns>
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
