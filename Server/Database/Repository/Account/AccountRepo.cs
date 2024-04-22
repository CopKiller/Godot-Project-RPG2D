using EntityFramework.Entities.Account;
using EntityFramework.Entities.Interface;
using EntityFramework.Repositories.Account;
using EntityFramework.Repositories.Interface;
using Microsoft.Extensions.DependencyInjection;
using Server.Logger;

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

        public async void RegisterAccountAsync(string username, string password)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var accountRepository = GetRepository<AccountEntity>();
                var repository = (AccountRepository)accountRepository;

                var playerAccount = new AccountEntity();
                playerAccount.Login = username;
                playerAccount.Password = password;

                var account = await repository.AddPlayerAccountAsync(playerAccount);

                ExternalLogger.Print(account.Message);
            }
        }
    }
}
