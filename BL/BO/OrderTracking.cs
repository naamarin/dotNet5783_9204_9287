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
    /// ?
    /// </summary>
    public List<Tuple<DateTime,string>>? Tracking { get; set; }

    //public override string ToString()
    //{
    //   // return this.ToStringProperty();
    //}
}
