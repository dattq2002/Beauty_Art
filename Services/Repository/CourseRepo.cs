using Microsoft.EntityFrameworkCore;
using Services.Commons;
using Services.Entity;
using Services.Repository.Interface;
using Services.Service;
using Services.Service.Interface;
using System;

namespace Services.Repository
{
    public class CourseRepo : GenericRepo<Course>, ICourseRepo
    {
        private readonly AppDBContext context;
        public CourseRepo(AppDBContext context, ICurrentTimeService currentTime, IClaimsServices claimsServices) : base(context, currentTime, claimsServices)
        {
            this.context = context;
        }
        public async Task<string> AutoIncreamentId()
        {
            var result = await context.Categories.MaxAsync(x => x.Id);
            var maxCategorytId = await context.Wallets.MaxAsync(x => x.Id);
            List<Wallet> existingCateGories = await context.Wallets.Where(x => x.Id == result).ToListAsync();
            var check = existingCateGories.Any() ? result : maxCategorytId;
            return check;
        }
    }
}
