using DragonRunes.Models;
using DragonRunes.Network;
using DragonRunes.Network.CustomDataSerializable;
using DragonRunes.Network.Packet.Client;
using DragonRunes.Server.Infrastructure;
using LiteNetLib;
using System.Xml.Linq;

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

            var accountId = player.accountId;

            var myPlayerData = await InitServer._databaseManager.PlayerRepository.GetPlayerByAccountIdAsync(accountId);
            if (myPlayerData != null) { ServerAlertMsg(netPeer, "You already have a character!"); return; }

            var checkPlayerName = await InitServer._databaseManager.PlayerRepository.HasPlayerNameAsync(obj.Name);
            if (checkPlayerName) { ServerAlertMsg(netPeer, "Character name already exists!"); return; }

            var newPlayer = new PlayerModel
            {
                Name = obj.Name,
                Gender = obj.Gender,
                Class = Class.Mage
            };

            var result = await InitServer._databaseManager.PlayerRepository
                .AddPlayerAsync(newPlayer);
            if ( result == null ) { ServerAlertMsg(netPeer, "Error creating character!"); return; }

            player._playerData = new PlayerDataModel(result);
            player.GameState = GameState.InGame;

            ServerInGame(netPeer);

            ServerPlayerToAllBut(netPeer);

        }
    }
}
