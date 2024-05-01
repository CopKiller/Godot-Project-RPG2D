using EntityFramework.Entities.Account;
using EntityFramework.Entities.Interface;
using EntityFramework.Repositories.Account;
using EntityFramework.Repositories.Interface;
using EntityFramework.Repositories.ValidadeData;
using Microsoft.Extensions.DependencyInjection;
using Server.Logger;
using System.Security.Principal;

namespace Server.Database.Repository.Account
{
    internal class AccountRepo
    {
        private readonly IServiceProvider _serviceProvider;

        public IRepository<T> GetRepository<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<IRepository<T>>();
        }

        public AccountRepo(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IAccountEntity> AuthenticateAsync(string username, string password)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var accountRepository = GetRepository<AccountEntity>();
                var repository = (AccountRepository)accountRepository;

                var account = await repository.AuthenticateAccountAsync(username, password);

                ExternalLogger.Print(account.Message);

                if (account.EntityType == null)
                {
                    return null;
                }

                if (account.Success)
                {
                    return account.EntityType;
                }

                return null;
            }
        }

        public async Task<OperationResult> RegisterAccountAsync(string username, string password, string email)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var accountRepository = GetRepository<AccountEntity>();
                var repository = (AccountRepository)accountRepository;

                var playerAccount = new AccountEntity();
                playerAccount.Login = username;
                playerAccount.Password = password;
                playerAccount.Email = email;

                var account = await repository.AddPlayerAccountAsync(playerAccount);

                return account;
            }
        }
    }
}
