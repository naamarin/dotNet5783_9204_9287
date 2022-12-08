using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T> where T : struct
    {
        /// <summary>
        /// function for adding
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Add(T item);

        /// <summary>
        /// function for get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(int id);

        /// <summary>
        /// function for update
        /// </summary>
        /// <param name="item"></param>
        public void Update(T item);

        /// <summary>
        /// function for delete
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id);

        /// <summary>
        /// function for get all
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<T?> GetAll(Func<T?,bool>? filter = null);
    }
}
