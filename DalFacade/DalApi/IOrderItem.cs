using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    public interface IOrderItem : ICrud<OrderItem>
    {
        public List<OrderItem?> getAllOrderItems(int id);
        public OrderItem? getOrderItems(int idProduct, int idOrder);
    }
}
