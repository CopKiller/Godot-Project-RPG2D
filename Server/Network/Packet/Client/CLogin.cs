using LiteNetLib;
using LiteNetLib.Utils;
using Server.Infrastructure;
using Server.Logger;
using Server.Network.Packet.Server;
using SharedLibrary.DataType;
using SharedLibrary.Extensions;

namespace Server.Network.Packet.Client
{
    internal class CLogin : IRecv
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public void ReadPacket(DictionaryWrapper<int, ServerClient> players,
            PacketProcessor netPacketProcessor, int peerId)
        {
            var player = players.GetItem(peerId);

            // Check if player is in menu
            if (player._playerData.GameState != GameState.InMenu) { return; }

            // Get repo
            var db = InitServer._databaseManager._databaseManager.AccountRepo;
            if (db == null) { return; }

            // Validate login
            var account = db.AuthenticateAsync(Login, Password);
            if (account.Result == null) { return; }

            // Check if account is already logged in
            if (CheckMultipleAccounts(player, players)) { return; }

            ExternalLogger.Print("account logged in: " + account.Result.Login);

            player._playerData.accountId = account.Result.Id;

            if (account.Result.Player.Name == string.Empty)
            {
                // Create character
                new SNewChar().WritePacket(netPacketProcessor, player._peer);
                player._playerData.GameState = GameState.InCharacterCreation;
                return;
            }

            // Create player data
            var playerVar = account.Result.Player;
            player._playerData.Position = new Vector2(playerVar.Position.X, playerVar.Position.Y);
            player._playerData.PlayerName = playerVar.Name;
            player._playerData.playerId = playerVar.Id;

            JoinGameData(players, netPacketProcessor, peerId);
        }

        private bool CheckMultipleAccounts(ServerClient client, DictionaryWrapper<int, ServerClient> players)
        {
            var _players = players.GetItems().Count(x => x.Value._playerData.accountId == client._playerData.accountId);

            return _players > 0;

        }

        public void JoinGameData(DictionaryWrapper<int, ServerClient> players,
            PacketProcessor netPacketProcessor, int peerId)
        {
            var client = players.GetItem(peerId);
            // Add player to the Dictionary of all players
            client._playerData.GameState = GameState.InGame;

            // Send all players to the new player -> TO NEW PLAYER
            SendAllPlayersTo(players, netPacketProcessor, peerId);

            // Send new player to all players -> TO ALL PLAYERS
            SendPlayerToAllPlayers(players, netPacketProcessor, peerId);
        }

        private void SendAllPlayersTo(DictionaryWrapper<int, ServerClient> players,
                       PacketProcessor netPacketProcessor, int peerId)
        {
            var _players = players.GetItems();

            var sPeersAll = new SPeersAll();
            sPeersAll.PlayerDataModels = [.. _players.Select(x => x.Value._playerData).Where(y => y.GameState == GameState.InGame)];
            sPeersAll.WritePacket(netPacketProcessor, players.GetItem(peerId)._peer);
        }

        private void SendPlayerToAllPlayers(DictionaryWrapper<int, ServerClient> players,
                       PacketProcessor netPacketProcessor, int peerId)
        {
            var _players = players.GetItems().Values.Where(x => x._playerData.GameState == GameState.InGame && x._peer.Id != peerId);

            var myPlayer = players.GetItem(peerId);

            foreach (var player in _players)
            {
                new SPlayerData()
                {
                    PlayerDataModel = myPlayer._playerData
                }.WritePacket(netPacketProcessor, player._peer);
            }
        }
    }
}
