using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface ICart
{
    /// <summary>
    /// function for adding an item to the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productID"></param>
    /// <returns></returns>
    public void AddItemToCart(BO.Cart cart, int productID, int amnt);

    /// <summary>
    /// function for update the amount of the items in cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productID"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public BO.Cart UpdateCart(BO.Cart cart, int productID, int amount);

    public void RemoveOrderItem(BO.Cart cart, int productID);


    /// <summary>
    /// function for paid and make order
    /// </summary>
    /// <param name="cart"></param>
    public int MakeOrder(BO.Cart cart);
}
