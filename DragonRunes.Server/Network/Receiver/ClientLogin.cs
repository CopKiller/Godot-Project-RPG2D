using DragonRunes.Models;
using DragonRunes.Network.Packet.Client;
using DragonRunes.Server.Infrastructure;
using DragonRunes.Network.CustomDataSerializable;
using LiteNetLib;

namespace DragonRunes.Server.Network
{
    public partial class ServerPacketProcessor
    {
        public async void ClientLogin(CLogin obj, NetPeer netPeer)
        {
            var player = _players.GetItem(netPeer.Id);

            if (player == null)
            {
                ServerAlertMsg(netPeer, "You are not connected to the server!");
                netPeer.Disconnect();
                return;
            }

            if (player.GameState != GameState.InLogin)
            {
                ServerAlertMsg(netPeer, "You are not in the menu screen!");
                player.Disconnect();
                _players.RemoveItem(netPeer.Id);
                return;
            }

            var account = await InitServer._databaseManager.AccountRepository.CheckAccountAsync(obj.Login, obj.Password);

            if (account == null)
            {
                ServerAlertMsg(netPeer, "Invalid username or password!");
                return;
            }

            var playerData = account.Player;

            var playerDataModel = new PlayerDataModel(playerData);
            playerDataModel.Index = netPeer.Id;
            player._playerData = playerDataModel;

            if (playerData.Name == string.Empty)
            {
                ServerNewChar(netPeer);
                ServerAlertMsg(netPeer, "You need to create a character first!");
                return;
            }

            ServerAllPlayerData(netPeer);

            ServerPlayerToAllBut(netPeer, playerDataModel);

        }
    }
}
