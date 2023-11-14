using Beauty_Art.Domains.Models;
using Beauty_Art.Payload.Request;

namespace Beauty_Art.Service.Interface
{
    public interface IOrderService
    {
        Task<Order> Payment(OrderRequest req);
    }
}
