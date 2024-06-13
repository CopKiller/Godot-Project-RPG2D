using DragonRunes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonRunes.Database.Repository
{
    public interface IAccountModelRepository
    {
        Task<IList<AccountModel>> GetAccountsAsync();
        Task<AccountModel> GetAccountAsync(string login);
        Task AddAccountAsync(AccountModel account);
        Task UpdateAccountAsync(AccountModel account);
        Task DeleteAccountAsync(string login);
    }
}
