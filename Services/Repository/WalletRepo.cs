using Services.Entity;
using Services.Repository.Interface;
using Services.Service.Interface;
using Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class WalletRepo : GenericRepo<Wallet>, IWalletRepo
    {
        private readonly AppDBContext context;
        public WalletRepo(AppDBContext context, ICurrentTimeService currentTime, IClaimsServices claimsServices) : base(context, currentTime, claimsServices)
        {
            this.context = context;
        }

    }
}
