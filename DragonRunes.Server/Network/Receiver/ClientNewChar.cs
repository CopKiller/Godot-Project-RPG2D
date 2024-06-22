using DragonRunes.Models;
using DragonRunes.Network;
using DragonRunes.Network.CustomDataSerializable;
using DragonRunes.Network.Packet.Client;
using DragonRunes.Server.Infrastructure;
using LiteNetLib;

namespace DragonRunes.Server.Network
{
    public partial class ServerPacketProcessor
    {
        public async void ClientNewChar(CNewChar obj, NetPeer netPeer)
        {
            var player = _players.GetItem(netPeer.Id);

            if (player.GameState != GameState.InLogin)
            {
                ServerAlertMsg(netPeer, "You are not in the menu screen!");
                player.Disconnect();
                _players.RemoveItem(netPeer.Id);
                return;
            }

            if (!obj.Name.IsValidName())
            {
                ServerAlertMsg(netPeer, "Invalid character name!");
            }

            if (obj.Gender < 0 || (byte)obj.Gender > Enum.GetValues(typeof(Gender)).Length) 
            {
                ServerAlertMsg(netPeer, "Invalid Gender!");
            }

            var newPlayer = new PlayerModel
            {
                Name = obj.Name,
                Gender = obj.Gender,
                Class = Class.Mage,
            };

            var result = await InitServer._databaseManager.PlayerRepository
                .RegisterPlayerAsync(newPlayer);

            if (!result) { ServerAlertMsg(netPeer, "Character name already exists!"); return; }

            player._playerData = new PlayerDataModel(newPlayer);
            player._playerData.Index = netPeer.Id;
            player.GameState = GameState.InGame;

            ServerInGame(netPeer);

            //ServerAllPlayerData(netPeer);

            ServerPlayerToAllBut(netPeer);

        }
    }
}
