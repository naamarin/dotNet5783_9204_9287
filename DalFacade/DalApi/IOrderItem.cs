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
        /// <summary>
        /// function for get all the order items
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<OrderItem?> getAllOrderItems(int id);

        /// <summary>
        /// function for get order items
        /// </summary>
        /// <param name="idProduct"></param>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        public OrderItem? getOrderItems(int idProduct, int idOrder);
    }
}
