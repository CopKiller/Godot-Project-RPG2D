using EntityFramework.Entities.Account;
using EntityFramework.Entities.Interface;
using EntityFramework.Entities.Player;
using EntityFramework.Repositories.ValidadeData;
using EntityFramework.Repositories.ValidateData;
using Microsoft.EntityFrameworkCore;


namespace EntityFramework.Repositories.Account
{
    public class AccountRepository : RepositoryBase<AccountEntity>
    {
        private readonly MeuDbContext _dbContext;

        public override object GetPrimaryKey(AccountEntity entity) => entity.Id;

        public AccountRepository(MeuDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> AddPlayerAccountAsync(AccountEntity account)
        {
            var login = InputValidator.IsValidLogin(account.Login);
            var password = InputValidator.IsValidPassword(account.Password);

            if (login.Success && password.Success)
            {

                var verifyAccountExists = await CheckPlayerAccountAsync(account.Login);

                if (verifyAccountExists.Success == false)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = $"[DATABASE] Account {account.Login} already exists!"
                    };
                }

                //SHA256 Criptography of password -> Set on Database
                Hash.CreatePasswordHash(account.Password, out string passwordHash, out string salt);
                account.Password = passwordHash;
                account.Salt = salt;

                await _dbContext.AccountEntities.AddAsync(account);

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    return new OperationResult
                    {
                        Success = true,
                        Message = $"[DATABASE] Account {account.Login} has been addeded!",
                    };
                }
                else
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = $"[DATABASE] Account {account.Login} has not been addeded!",
                    };
                }
            }
            else // Tratamentos de erros
            {
                if (!login.Success)
                {
                    return login;
                }
                else if (!password.Success)
                {
                    return password;
                }
                else
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = $"[DATABASE] Account {account.Login} has not been addeded!",
                    };
                }
            }
        }


        //Normalmente utilizado ao criar uma conta, verificando se existe alguma conta com o mesmo login
        //Esta sendo utilizado como login para testes!
        public async Task<OperationResult> CheckPlayerAccountAsync(string login)
        {
            var operationResult = new OperationResult();
            var conta = await _dbContext.AccountEntities
                .FirstOrDefaultAsync(e => e.Login == login);

            if (conta == null)
            {
                operationResult.Success = true;
                return operationResult;
            }
            else
            {
                operationResult.Success = false;
                operationResult.Message = $"[DATABASE] Account {login} already exists!";
                return operationResult;
            }
        }

        public async Task<OperationResult> AuthenticateAccountAsync(string login, string password)
        {
            var operationResult = new OperationResult();

            var account = await _dbContext.AccountEntities.Include(a => a.Player)
                .FirstOrDefaultAsync(e => e.Login == login);

            if (account == null)
            {
                operationResult.Success = false;
                operationResult.Message = $"[DATABASE] Account {login} not found!";
                return operationResult;
            }

            if (Hash.VerifyPassword(password, account.Password, account.Salt))
            {
                operationResult.Success = true;
                operationResult.Message = $"[DATABASE] Account {login} has been authenticated!";
            }
            else
            {
                operationResult.Success = false;
                operationResult.Message = $"[DATABASE] Account {login} has not been authenticated!";
            }

            operationResult.EntityType = account;

            return operationResult;

        }

        public async Task<int> AtualizarContaAsync()
        {
            try
            {
                var result = await _dbContext.SaveChangesAsync();

                return result; // Retorna o número de entidades atualizadas
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar conta: {ex.Message}");
                return 0;
            }
        }

        public async Task<int> ExcluirContaPorLoginAsync(string login)
        {
            var accounts = await _dbContext.AccountEntities
                .Where(e => e.Login == login)
                .ToListAsync();

            if (accounts.Any())
            {
                _dbContext.AccountEntities.RemoveRange(accounts);
                await _dbContext.SaveChangesAsync();
                return accounts.Count;
            }

            return 0;
        }
    }
}
