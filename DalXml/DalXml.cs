using DalApi;
using DO;
using System.Diagnostics;
using DalXml;
using Dal;

namespace Dal;

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    public IProduct Product => new DalProduct();
}