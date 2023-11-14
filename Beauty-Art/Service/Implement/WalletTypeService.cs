using AutoMapper;
using Beauty_Art.API.Services;
using Beauty_Art.Domains.Models;
using Beauty_Art.Payload.Request;
using Beauty_Art.Repository.Interfaces;
using Beauty_Art.Service.Interface;

namespace Beauty_Art.Service.Implement
{
    public class WalletTypeService : BaseService<WalletTypeService>, IWalletTypeService
    {
        public WalletTypeService(IUnitOfWork<BEAUTIFUL_ARTSContext> unitOfWork, 
            ILogger<WalletTypeService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) 
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<WalletType> CreateWallet(WalletTypeRequest req)
        {
            WalletType walletType = new WalletType()
            {
                Id = Guid.NewGuid(),
                DeFlag = false,
                InsDate = DateTime.Now,
                UpsDate = DateTime.Now,
                Name = req.name
            };
            await _unitOfWork.GetRepository<WalletType>().InsertAsync(walletType);
            await _unitOfWork.CommitAsync();
            return walletType;
        }
        public async Task<bool> DeleteWallet(Guid Id)
        {
            WalletType walletType = await _unitOfWork.GetRepository<WalletType>().SingleOrDefaultAsync(
                predicate: x => x.Id.Equals(Id) &&(bool) !x.DeFlag
                );
            if(walletType == null)
            {
                return false;
            }
            walletType.DeFlag = true;
            _unitOfWork.GetRepository<WalletType>().UpdateAsync(walletType);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
