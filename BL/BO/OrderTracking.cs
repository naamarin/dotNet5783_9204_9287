using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// For order tracking screen - type BO
/// </summary>
public class OrderTracking
{
    /// <summary>
    /// ID order
    /// </summary>
    public int ID { get; set; }
    
    /// <summary>
    /// The status order
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// contauns couple of date and details
    /// </summary>
    
    public List<Tuple<DateTime?,string>>? Tracking { get; set; }
    /// <summary>
    /// print tracking of the order
    /// </summary>
    /// <returns></returns>
    
    public override string ToString()
    {
         return this.ToStringProperty();
    }
}
