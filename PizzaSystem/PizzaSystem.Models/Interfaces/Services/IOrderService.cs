using PizzaSystem.Models.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSystem.Models.Interfaces.Services
{
    public interface IOrderService
    {
        Guid PlaceOrder(Order.Order order);
        ConfirmedOrder GetOrder(Guid orderId);
    }
}
