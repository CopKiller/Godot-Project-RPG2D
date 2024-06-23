using DragonRunes.Models;
using DragonRunes.Network.Packet.Client;
using DragonRunes.Server.Infrastructure;
using DragonRunes.Network;
using LiteNetLib;
using DragonRunes.Network.CustomDataSerializable;

namespace DragonRunes.Server.Network
{
    public partial class ServerPacketProcessor
    {
        public async void ClientRegister(CRegister obj, NetPeer netPeer)
        {
            var player = _players.GetItem(netPeer.Id);

            if (player.GameState != GameState.InLogin)
            {
                ServerAlertMsg(netPeer, "You are not in the menu screen!");
                player.Disconnect();
                _players.RemoveItem(netPeer.Id);
                return;
            }

            if (!(obj.Login.IsValidName() &&
                obj.Password.IsValidPassword() &&
                obj.Email.IsValidEmail() &&
                obj.BirthDate.IsValidBirthDate()))
            {
                ServerAlertMsg(netPeer, "Rules of data is ivalid!");
                return;
            }

            var account = new AccountModel
            {
                User = obj.Login,
                Password = obj.Password,
                Email = obj.Email,
                BirthDate = Convert.ToDateTime(obj.BirthDate)
            };

            var result = await InitServer._databaseManager.AccountRepository.AddAccountAsync(account);

            if (!result) { ServerAlertMsg(netPeer, "Username already exists!"); return; }

            ServerAlertMsg(netPeer, "Account created successfully!");

            ClientLogin(new CLogin { Login = obj.Login, Password = obj.Password }, netPeer);
        }
    }
}
