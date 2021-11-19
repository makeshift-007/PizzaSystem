using PizzaSystem.Models;
using PizzaSystem.Models.Interfaces;
using PizzaSystem.Models.Interfaces.Data;
using PizzaSystem.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaSystem.Data
{
    public class OrderFileRepository : IOrderRepository
    {
        private readonly IFileDatabase fileDatabase;
        private const string repoPath = "OrderFile.json";
        private List<ConfirmedOrder> GetSavedOrders()
        {
            var data = fileDatabase.Read<List<ConfirmedOrder>>(repoPath);
            return data == null ? new List<ConfirmedOrder>() : data;
        }
        private void SaveOrders(List<ConfirmedOrder> orders)
        {
            fileDatabase.Write(repoPath, orders);
        }

        public OrderFileRepository(IFileDatabase fileDatabase)
        {
            this.fileDatabase = fileDatabase;
        }

        public void Delete(Guid id)
        {
            var orders = GetSavedOrders();
            if (orders.Any(m => m.OrderID == id))
            {
                orders.Remove(orders.FirstOrDefault(m => m.OrderID == id));
                SaveOrders(orders);
            }
        }

        public List<ConfirmedOrder> GetOrders() => GetSavedOrders();


        public ConfirmedOrder GetOrder(Guid id)
        {
            return GetSavedOrders().FirstOrDefault(m => m.OrderID == id);
        }

        public void InsertNew(ConfirmedOrder order)
        {
            var orders = GetSavedOrders();            
            orders.Add(order);
            SaveOrders(orders);
        }

        public void Update(ConfirmedOrder order)
        {
            var orders = GetSavedOrders();
            if (orders.Any(m => m.OrderID == order.OrderID))
            {
                orders.Remove(orders.FirstOrDefault(m => m.OrderID == order.OrderID));
            }
            orders.Add(order);
            SaveOrders(orders);
        }
    }
}
