using Services.Entity;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.Interface
{
    public interface IWalletService
    {
        Task<Wallet> CreateWallets(WalletModel req);
        Task<bool> UpdateWallets(WalletModel req, string id);
        Task<bool> DeleteWallets(string id);
        Task<Wallet> GetWalletById(string id);
        Task<List<Wallet>> GetWalletsAsync();

    }
}
