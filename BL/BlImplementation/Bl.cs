using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;
using BlApi;
using BO;

public class Bl : IBl
{
    public Bl() { }
    public IOrder Order { get; set; } = new Order();
    public IProduct Product { get; set; } = new Product();
    public ICart cart { get; set; } = new Cart();
}
