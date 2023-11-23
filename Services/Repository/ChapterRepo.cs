using Services.Entity;
using Services.Service.Interface;
using Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Repository.Interface;

namespace Services.Repository
{
    public class ChapterRepo : GenericRepo<Chapter>, IChapterRepo
    {
        private readonly AppDBContext context;
        public ChapterRepo(AppDBContext context, ICurrentTimeService currentTime, IClaimsServices claimsServices) : base(context, currentTime, claimsServices)
        {
            this.context = context;
        }
    }
}
