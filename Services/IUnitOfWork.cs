

using Services.Repository;
using Services.Repository.Interface;

namespace Services
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangeAsync();
        public IUserRepo userRepo { get; }
        public IWalletRepo walletRepo { get; }
        public ICategoryRepo categoryRepo { get; }
        public ICourseRepo courseRepo { get; }
        public IChapterRepo chapterRepo { get; }
        public IOrderRepo orderRepo { get; }
        public IPaymentRepo paymentRepo { get; }
        public IUserCourseRepo userCourseRepo { get; }
    }
}
