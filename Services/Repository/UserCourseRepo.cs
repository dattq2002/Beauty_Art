using Services.Entity;
using Services.Repository.Interface;
using Services.Service;
using Services.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class UserCourseRepo : GenericRepo<UserCourse>, IUserCourseRepo
    {
        private readonly AppDBContext context;
        public UserCourseRepo(AppDBContext context, ICurrentTimeService currentTime, IClaimsServices claimsServices) : base(context, currentTime, claimsServices)
        {
            this.context = context;
        }
    }
}
