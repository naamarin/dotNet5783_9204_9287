using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BO;

internal class Tools
{
    public static string ToStringProperty<T>(T t)
    {
        string str = "";
        foreach (PropertyInfo item in t.GetType().GetProperties())
        {
            if (item.GetType()==typeof(IEnumerable<T>))
            {
                str += "\n" + item.Name + ": ";
                item.ToString();
            }
            else
                str += "\n" + item.Name + ": " + item.GetValue(t, null);
        }
        return str;
    }
}
