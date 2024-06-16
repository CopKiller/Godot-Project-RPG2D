using DragonRunes.Models;
using DragonRunes.Network.Packet.Client;
using DragonRunes.Server.Infrastructure;
using DragonRunes.Shared;
using LiteNetLib;

namespace DragonRunes.Server.Network
{
    public partial class ServerPacketProcessor
    {
        public void ClientRegister(CRegister obj, NetPeer netPeer)
        {
            var player = _players.GetItem(netPeer.Id);

            if (player.GameState != GameState.InLogin)
            {
                ServerAlertMsg(netPeer, "You are not in the menu screen!");
                player.Disconnect();
                return;
            }

            if (obj.Login.IsValidName() && 
                obj.Password.IsValidPassword() && 
                obj.Email.IsValidEmail() && 
                obj.BirthDate.IsValidBirthDate())
            {

                var account = new AccountModel
                {
                    User = obj.Login,
                    Password = obj.Password,
                    Mail = obj.Email,
                    BirthDate = Convert.ToDateTime(obj.BirthDate)
                };

                InitServer._databaseManager.AccountRepository.AddAccountAsync(player, account);
            }
            else
            {
                ServerAlertMsg(netPeer, "Invalid username or password!");
            }
            
        }
    }
}
