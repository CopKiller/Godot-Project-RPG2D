using DragonRunes.Database.Repository;
using DragonRunes.Models;
using DragonRunes.Server.Infrastructure;

namespace DragonRunes.Server.Repository
{
    public interface IAccountRepository
    {
        int CountAccounts();

        Task<IAccountModel> CheckAccountAsync(string username, string password);
        Task<bool> AddAccountAsync(AccountModel accountModel);
        Task<bool> DeleteAccountAsync(string username);
    }
}
