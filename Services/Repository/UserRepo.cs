using Microsoft.EntityFrameworkCore;
using Services.Entity;
using Services.Repository;
using Services.Service;
using Services.Service.Interface;
using System;

namespace Services.Repository
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        private readonly AppDBContext context;
        public UserRepo(AppDBContext context, ICurrentTimeService currentTime, IClaimsServices claimsServices) : base(context, currentTime, claimsServices)
        {
            this.context = context;
        }
        public async Task<bool> ExistEmail(string email)
        {
            return await context.Users.AnyAsync(x => x.Email == email);
        }
        public Task<User?> Login(string email, string password)
        {
            var user = context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email) && x.Password.Equals(password));
            return user;
        }
        public async Task<string> AutoIncreamentId()
        {
            var result = await context.Users.MaxAsync(x => x.WalletId) + 1;
            var maxWalletId = await context.Wallets.MaxAsync(x => x.Id);
            List<Wallet> existingWalletIds = await context.Wallets.Where(x => x.Id==result).ToListAsync();
            var check = existingWalletIds.Any() ? result : maxWalletId;
            return check;
        }
    }
}
