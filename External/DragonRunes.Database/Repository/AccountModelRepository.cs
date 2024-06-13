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

        public async virtual Task AddAccountAsync(AccountModel account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            await _db.Accounts.AddAsync(account);
            await _db.SaveChangesAsync();
        }

        public async virtual Task DeleteAccountAsync(string user)
        {
            var account = await GetAccountAsync(user);
            _db.Accounts.Remove(account);
            await _db.SaveChangesAsync();
        }

        public async virtual Task<AccountModel> GetAccountAsync(string user)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("User cannot be null or empty.", nameof(user));
            }

            return await _db.Accounts.Include(x => x.Player).FirstOrDefaultAsync(a => a.User == user);
        }

        public async virtual Task<IList<AccountModel>> GetAccountsAsync()
        {
            return await _db.Accounts.Include(x => x.Player).ToListAsync();
        }

        public async virtual Task UpdateAccountAsync(AccountModel account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            _db.Accounts.Update(account);
            await _db.SaveChangesAsync();
        }
    }
}
