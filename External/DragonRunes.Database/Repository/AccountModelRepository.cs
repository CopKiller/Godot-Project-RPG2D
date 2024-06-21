using DragonRunes.Database;
using DragonRunes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DragonRunes.Database.Repository
{
    public abstract class AccountModelRepository : IAccountModelRepository
    {
        protected readonly DatabaseContext _db;

        public AccountModelRepository(DatabaseContext db) {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async virtual Task<bool> AddAccountAsync(AccountModel account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (await HasAccountNameAsync(account.User))
            {
                return false;
            }

            account.User = account.User.ToUpper();

            await _db.Accounts.AddAsync(account);
            return await _db.SaveChangesAsync() > 0 ? true : false;
        }

        public async virtual Task<bool> DeleteAccountAsync(string user)
        {
            user = user.ToUpper();
            var account = await _db.Accounts
                .Include(x => x.Player)
                .Include(y => y.Player.Position)
                .Include(z => z.Player.Direction)
                .FirstOrDefaultAsync(a => a.User == user);

            if (account == null)
            {
                return false;
            }

            _db.Accounts.Remove(account);
            return await _db.SaveChangesAsync() > 0 ? true : false;
        }

        public async virtual Task<AccountModel> GetAccountAsync(string user)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("User cannot be null or empty.", nameof(user));
            }
            user = user.ToUpper();
            var account = await _db.Accounts
                .Include(x => x.Player)
                .Include(y => y.Player.Position)
                .Include(z => z.Player.Direction)
                .FirstOrDefaultAsync(a => a.User == user);

            return account;
        }

        public async virtual Task<IList<AccountModel>> GetAccountsAsync()
        {
            return await _db.Accounts
                .Include(x => x.Player)
                .Include(y => y.Player.Position)
                .Include(z => z.Player.Direction)
                .ToListAsync();
        }

        public async virtual Task<bool> UpdateAccountAsync(AccountModel account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            _db.Accounts.Update(account);
            return await _db.SaveChangesAsync() > 0 ? true : false;
        }

        private async Task<bool> HasAccountNameAsync(string username)
        {
            username = username.ToUpper();
            return await _db.Accounts.AnyAsync(a => a.User == username);
        }
    }
}
