using Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.Interface
{
    public interface IPaymentService
    {
        Task<List<Payment>> GetAllPayment();
        Task<Payment> GetPaymentById(string id);
        Task<int> TotalSale();
    }
}
