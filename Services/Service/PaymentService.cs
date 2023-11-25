using AutoMapper;
using Services.Entity;
using Services.Model;
using Services.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<Payment>> GetAllPayment()
        {
            List<Payment> paymentList = (await _unitOfWork.paymentRepo.GetAllAsync()).ToList();
            return paymentList;
        }
        public async Task<Payment> GetPaymentById(string id)
        {
            Payment payment = await _unitOfWork.paymentRepo.GetEntityByIdAsync(id);
            return payment;
        }
        public async Task<TotalSale> TotalSale()
        {
            List<Payment> paymentList = (await _unitOfWork.paymentRepo.GetAllAsync()).ToList();
            int total = 0;
            int count = 0;
            foreach (var item in paymentList)
            {
                total += (int) item.Balance;
                count++;
            }
            TotalSale totalSale = new TotalSale()
            {
                Total = total,
                Count = count
            };
            return totalSale;
        }
    }
}
