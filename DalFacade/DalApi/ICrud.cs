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
        public int Add(T item);
        public T GetById(int id);
        public void Update(T item);
        public void Delete(int id);
        public IEnumerable<T?> GetAll(Func<T?,bool>? filter = null);
    }
}
