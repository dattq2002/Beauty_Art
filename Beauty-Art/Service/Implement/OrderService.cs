using AutoMapper;
using Beauty_Art.API.Services;
using Beauty_Art.API.Utils;
using Beauty_Art.Domains.Models;
using Beauty_Art.Enums;
using Beauty_Art.Payload.Request;
using Beauty_Art.Repository.Interfaces;
using Beauty_Art.Service.Interface;

namespace Beauty_Art.Service.Implement
{
    public class OrderService : BaseService<OrderService>, IOrderService
    {
        public OrderService(IUnitOfWork<BEAUTIFUL_ARTSContext> unitOfWork, ILogger<OrderService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<Order> Payment(OrderRequest req)
        {
            var time = DateTime.Now;
            User user = await _unitOfWork.GetRepository<User>()
                .SingleOrDefaultAsync(predicate: x => x.Id.Equals(req.User_Id) && !x.Deflag);
            UserWallet userWallet = await _unitOfWork.GetRepository<UserWallet>()
                .SingleOrDefaultAsync(predicate: x => x.UserId.Equals(req.User_Id) &&(bool) !x.DeFlag);
            if (user == null)
            {
                return null;
            }
            int quantity = 0;
            int price = 0;
            int sum = 0;
            foreach(var item in req.CourseOrder)
            {
                Course course = await _unitOfWork.GetRepository<Course>()
                .SingleOrDefaultAsync(predicate: x => x.Id.Equals(item.CourseId) && !x.Deflag);
                if(course == null)
                {
                    return null;
                }
                quantity += item.quanity;
                price = item.price * item.quanity;
                sum += price;
            }
            Order order = new Order()
            {
                Id = Guid.NewGuid(),
                Name = $"Đơn hàng trong ngày {time}",
                Quantity = quantity,
                Price = req.TotalPrice,
                UserId = user.Id,
                Deflag = false,
                InsDate = DateTime.Now,
                UpsDate = DateTime.Now,
                Status = OrderStatus.Status.Pending.GetDescriptionFromEnum()
            };
            await _unitOfWork.GetRepository<Order>().InsertAsync(order);
            await _unitOfWork.CommitAsync();
            bool action =await CreateAction(req, time, userWallet);
            if(action)
            {
                bool wallet = await UpdateWallet(req, time, userWallet);
                if(wallet)
                {
                    bool updateAction = await UpdateAction(req, time, userWallet);
                    if(updateAction)
                    {
                        foreach (var item in req.CourseOrder)
                        {
                            Course course = await _unitOfWork.GetRepository<Course>()
                            .SingleOrDefaultAsync(predicate: x => x.Id.Equals(item.CourseId) && !x.Deflag);
                            if (course == null)
                            {
                                return null;
                            }
                            bool courseInOrder = await CreateCourseInOrder(order, course);
                            if(!courseInOrder)
                            {
                                return null;
                            }
                        }
                        order.Status = OrderStatus.Status.Completed.GetDescriptionFromEnum();
                        order.UpsDate = DateTime.Now;
                        _unitOfWork.GetRepository<Order>().UpdateAsync(order);
                        int check = await _unitOfWork.CommitAsync();
                        return order;
                    }
                }
            }
            return null;
        }

        private async Task<bool> CreateAction(OrderRequest req, DateTime date, UserWallet userWallet)
        {
            UserAction userAction = new UserAction()
            {
                Id = Guid.NewGuid(),
                Name = $"Đơn hàng trong ngày {date}",
                InsDate = DateTime.Now,
                UpsDate = DateTime.Now,
                ActionValue = req.TotalPrice,
                UserWalletId = userWallet.Id,
                Status = ActionStatus.Status.Fail.GetDescriptionFromEnum(),
            };
            await _unitOfWork.GetRepository<UserAction>().InsertAsync(userAction);
            int check = await _unitOfWork.CommitAsync();
            return check > 0 ? true : false;
        }
        private async Task<bool> UpdateWallet(OrderRequest req, DateTime date, UserWallet userWallet)
        {
            userWallet.Balance -= req.TotalPrice;
            userWallet.UpsDate = DateTime.Now;
            _unitOfWork.GetRepository<UserWallet>().UpdateAsync(userWallet);
            int check = await _unitOfWork.CommitAsync();
            return check > 0 ? true : false;
        }
        private async Task<bool> UpdateAction(OrderRequest req, DateTime date, UserWallet userWallet)
        {
            UserAction userAction = await _unitOfWork.GetRepository<UserAction>()
                .SingleOrDefaultAsync(predicate: x => x.UserWalletId.Equals(userWallet.Id));
            userAction.Status = ActionStatus.Status.Success.GetDescriptionFromEnum();
            userAction.UpsDate = DateTime.Now;
            _unitOfWork.GetRepository<UserAction>().UpdateAsync(userAction);
            int check = await _unitOfWork.CommitAsync();
            return check > 0 ? true: false;
        }
        private async Task<bool> CreateCourseInOrder(Order order, Course course)
        {
            CourseInOrder courseInOrder = new CourseInOrder()
            {
                Id = Guid.NewGuid(),
                CourseId = course.Id,
                OrderId = order.Id,
                InsDate = DateTime.Now,
                UpsDate = DateTime.Now,
                Deflag = false,
                Status = OrderStatus.Status.Completed.GetDescriptionFromEnum()
            };
            await _unitOfWork.GetRepository<CourseInOrder>().InsertAsync(courseInOrder);
            int check = await _unitOfWork.CommitAsync();
            return check > 0 ? true: false;
        }
    }
}
