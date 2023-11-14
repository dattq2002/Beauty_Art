using Beauty_Art.Domains.Models;
using Beauty_Art.Payload.Request;

namespace Beauty_Art.Service.Interface
{
    public interface IWalletTypeService
    {
        Task<bool> DeleteWallet(Guid Id);
        Task<WalletType> CreateWallet(WalletTypeRequest req);
    }
}
