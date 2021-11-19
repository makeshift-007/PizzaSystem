using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSystem.Models.Interfaces.Data
{
    public interface IOrderRepository
    {
        List<Order.ConfirmedOrder> GetOrders();
        Order.ConfirmedOrder GetOrder(Guid id);
        void InsertNew(Order.ConfirmedOrder order);
        void Update(Order.ConfirmedOrder order);
        void Delete(Guid id);
    }
}
