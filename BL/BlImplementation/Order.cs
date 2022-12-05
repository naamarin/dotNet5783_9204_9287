using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlApi;    
using BO;
using DalApi;
using DO;

namespace BlImplementation;

internal class Order : IOrder
{
    DalApi.IDal dal = new Dal.DalList();

    public BO.Order getByIdToManager(int id)
    {
        if (id <= 0)
            throw new InvalidInputException("order ID " + id);
        DO.Order orderDo =new DO.Order();
        try
        {
           orderDo = dal.Order.GetById(id);
        }
        catch(Exception ex)
        {
            throw new BO.DalDoesNotExistException(ex);
        }
        BO.Order orderBo= new BO.Order();
        orderBo.ID = orderDo.ID;
        orderBo.CustomerName = orderDo.CustomerName;
        orderBo.CustomerAddress = orderDo.CustomerAdress;
        orderBo.OrderDate = orderDo.OrderDate;
        orderBo.CustomerEmail = orderDo.CustomerEmail;
        orderBo.ShipDate = orderDo.ShipDate;
        orderBo.DeliveryDate = orderDo.DeliveryDate;
        return orderBo;
    }
}
