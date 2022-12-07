using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface ICart
{
    public BO.Cart AddItemToCart(BO.Cart cart, int productID);
    public BO.Cart UpdateCart(BO.Cart cart, int productID, int amount);
    public void MakeOrder(BO.Cart cart);
}
