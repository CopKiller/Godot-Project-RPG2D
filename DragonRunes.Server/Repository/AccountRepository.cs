using DragonRunes.Cryptography;
using DragonRunes.Database;
using DragonRunes.Database.Repository;
using DragonRunes.Models;
using DragonRunes.Server.Infrastructure;
using DragonRunes.Server.Network;
using LiteNetLib;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonRunes.Server.Repository
{
    public class AccountRepository : AccountModelRepository, IAccountRepository
    {
        public AccountRepository(DatabaseContext db) : base(db) { }

        // Métodos de implementação e tratamento, específicos do servidor

        public int CountAccounts()
        {
            return _db.Accounts.Count();
        }

        public async Task<AccountModel> CheckAccount(ServerClient playerClient, string username, string password)
        {
            var account = await base.GetAccountAsync(username);

            if (account == null || account.Password != password)
            {
                playerClient._serverPacketProcessor.ServerAlertMsg(playerClient._peer, "Username or Password incorrect!");
                return null;
            }

            if (!ValidateAccountByCrypto(account, password))
            {
                playerClient._serverPacketProcessor.ServerAlertMsg(playerClient._peer, "Username or Password incorrect!");
                return null;
            }

            return account;
        }



        private bool ValidateAccountByCrypto(AccountModel account, string inputPassword)
        {
            return SHA256.VerifyPassword(inputPassword, account.Password, account.Salt);
        }

        public async Task AddAccountAsync(ServerClient playerClient, AccountModel accountModel)
        {

            if (await HasAccountName(accountModel.User))
            {
                playerClient._serverPacketProcessor.ServerAlertMsg(playerClient._peer, "Account already exists!");
                return;
            }

            // Cryptography
            SHA256.CreatePasswordHash(accountModel.Password, out string passwordHash, out string salt);
            accountModel.Password = passwordHash;
            accountModel.Salt = salt;

            await base.AddAccountAsync(accountModel);
            playerClient._serverPacketProcessor.ServerAlertMsg(playerClient._peer, "Account created successfully!");
        }
        private async Task<bool> HasAccountName(string username)
        {
            return await _db.Accounts.AnyAsync(a => a.User == username);
        }
    }
}
