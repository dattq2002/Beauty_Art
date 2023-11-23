using Services.Entity;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.Interface
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(OrderModel req);
        Task<Order> GetOrderById(string id);
        Task<List<Order>> GetAllOrder();
    }
}
