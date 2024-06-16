using DragonRunes.Database.Repository;
using DragonRunes.Models;
using DragonRunes.Server.Infrastructure;

namespace DragonRunes.Server.Repository
{
    public interface IAccountRepository : IAccountModelRepository
    {
        int CountAccounts();

        Task<AccountModel> CheckAccount(ServerClient playerClient, string username, string password);

        Task AddAccountAsync(ServerClient playerClient, AccountModel accountModel);
    }
}
