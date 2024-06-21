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
        Task<bool> AddAccountAsync(AccountModel account);
        Task<bool> UpdateAccountAsync(AccountModel account);
        Task<bool> DeleteAccountAsync(string login);
    }
}
