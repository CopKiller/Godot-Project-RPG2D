using DragonRunes.Network.Packet.Client;
using DragonRunes.Server.Infrastructure;
using DragonRunes.Shared.CustomDataSerializable;
using LiteNetLib;

namespace DragonRunes.Server.Network
{
    public partial class ServerPacketProcessor
    {
        public void Login(CLogin obj, NetPeer netPeer)
        {
            var player = _players.GetItem(netPeer.Id);

            if (player.GameState != GameState.InLogin)
            {
                ServerAlertMsg(netPeer, "You are not in the menu screen!");
                player.Disconnect();
                return;
            }

            var account = InitServer._databaseManager.AccountRepository.CheckAccount(player, obj.Login, obj.Password);

            if (account.Result == null) { return; }

            var playerData = (PlayerDataModel)account.Result.Player;
            playerData.Index = netPeer.Id;

            player._playerData = playerData;

            if (playerData.Name == string.Empty)
            {
                ServerAlertMsg(netPeer, "You need to create a character first!");
                ServerNewChar(netPeer);
                return;
            }

            ServerAllPlayerData(netPeer);

            ServerPlayerToAllBut(netPeer, playerData);

        }
    }
}
