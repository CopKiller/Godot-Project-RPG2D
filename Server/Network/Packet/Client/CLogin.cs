
using Server.Infrastructure;
using Server.Logger;
using Server.Network;
using SharedLibrary.DataType;
using SharedLibrary.Extensions;

namespace Network.Packet
{
    public class CLogin : IRecv
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public void ReadPacket(DictionaryWrapper<int, ServerClient> players,
            PacketProcessor netPacketProcessor, int peerId)
        {
            var player = players.GetItem(peerId);

            // Check if player is in menu
            if (player.GameState != GameState.InMenu) { return; }

            // Get repo
            var db = InitServer._databaseManager._databaseManager.AccountRepo;
            if (db == null) { return; }

            // Validate login
            var account = db.AuthenticateAsync(Login, Password);
            if (account.Result == null) 
            { 
                new SAlertMsg() { Msg = "Invalid login or password" }.WritePacket(netPacketProcessor, player._peer);
                return;
            }

            player._playerData.accountId = account.Result.Id;

            // Check if account is already logged in
            if (CheckMultipleAccounts(player, players))
            {
                new SAlertMsg() { Msg = "Account is already logged in" }.WritePacket(netPacketProcessor, player._peer);
                return;
            }

            ExternalLogger.Print("account logged in: " + account.Result.Login + " index: " + peerId);

            if (account.Result.Player.Name == string.Empty)
            {
                // Create character
                new SNewChar().WritePacket(netPacketProcessor, player._peer);
                player.GameState = GameState.InCharacterCreation;
                return;
            }

            // Create player data
            var playerVar = account.Result.Player;

            player._playerData.ConvertPlayerData(playerVar);

            player._playerPhysic.ConvertPosition(playerVar.Position);
            player._playerPhysic.ConvertDirection(playerVar.Direction);

            JoinGameData(players, netPacketProcessor, peerId);
        }

        private bool CheckMultipleAccounts(ServerClient client, DictionaryWrapper<int, ServerClient> players)
        {
            var _players = players.GetItems().Count(x => x.Value._playerData.accountId == client._playerData.accountId);

            return _players > 1;

        }

        public void JoinGameData(DictionaryWrapper<int, ServerClient> players,
            PacketProcessor netPacketProcessor, int peerId)
        {
            var client = players.GetItem(peerId);
            // Add player to the Dictionary of all players
            client.GameState = GameState.InGame;

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
            sPeersAll.PlayerDataModels = [.. _players.Select(x => x.Value).Where(y => y.GameState == GameState.InGame).Select(z => z._playerData)];
            sPeersAll.PlayerPhysicModels = [.. _players.Select(x => x.Value).Where(y => y.GameState == GameState.InGame).Select(z => z._playerPhysic)];
            sPeersAll.WritePacket(netPacketProcessor, players.GetItem(peerId)._peer);
        }

        private void SendPlayerToAllPlayers(DictionaryWrapper<int, ServerClient> players,
                       PacketProcessor netPacketProcessor, int peerId)
        {
            var _players = players.GetItems().Values.Where(x => x.GameState == GameState.InGame && x._peer.Id != peerId);


            var myPlayer = players.GetItem(peerId);

            foreach (var player in _players)
            {
                new SPlayerData()
                {
                    PlayerDataModel = myPlayer._playerData,
                    PlayerPhysicModel = myPlayer._playerPhysic
                }.WritePacket(netPacketProcessor, player._peer);
            }
        }
    }
}
