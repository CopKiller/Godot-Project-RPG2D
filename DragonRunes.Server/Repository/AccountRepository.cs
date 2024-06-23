using DragonRunes.Cryptography;
using DragonRunes.Database;
using DragonRunes.Database.Repository;
using DragonRunes.Models;
using DragonRunes.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DragonRunes.Server.Repository
{
    public class AccountRepository : AccountModelRepository
    {
        public AccountRepository(DatabaseContext db) : base(db) { }

        // Métodos de implementação e tratamento, específicos do servidor

        public int CountAccounts()
        {
            return _db.Accounts.Count();
        }

        public async Task<AccountModel> CheckAccountAsync(string username, string password)
        {
            var account = await base.GetAccountAsync(username);

            if (account == null) { return null; }

            if (!SHA256.VerifyPassword(password, account.Password, account.Salt)) { return null; }

            return account;
        }

        public override async Task<bool> AddAccountAsync(AccountModel accountModel)
        {
            // Cryptography
            SHA256.CreatePasswordHash(accountModel.Password, out string passwordHash, out string salt);
            accountModel.User = accountModel.User.ToUpper();
            accountModel.Password = passwordHash;
            accountModel.Salt = salt;

            var result = await base.AddAccountAsync(accountModel);
            return result;
        }
    }
}
