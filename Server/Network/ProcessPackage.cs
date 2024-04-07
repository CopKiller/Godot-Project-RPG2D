using LiteNetLib.Utils;
using Server.Infrastructure;
using SharedLibrary.Extensions;
using SharedLibrary.Network.Packet.Client;
using LiteNetLib;
using Server.Logger;
using SharedLibrary.Network.Packet.Server;
using SharedLibrary.Models;
using SharedLibrary.DataType;

namespace Server.Network
{
    internal class ProcessPackage
    {
        internal readonly DictionaryWrapper<int, ServerClient> _players;
        internal readonly NetPacketProcessor _processor;

        public ProcessPackage(DictionaryWrapper<int, ServerClient> players, NetPacketProcessor netPacketProcessor)
        {
            _players = players;
            _processor = netPacketProcessor;
        }

        #region ReceiveData
        public void ProcessPlayerAction(CPlayerAction playerAction, NetPeer netPeer)
        {
            var player = _players.GetItem(netPeer.Id);
            if (player._playerData.GameState != GameState.InGame) { return; }

            if (_players.ContainsKey(netPeer.Id))
            {
                switch (playerAction.ActionType)
                {
                    case PlayerActionType.Move:
                        _players.GetItem(netPeer.Id)._playerData.Position = playerAction.Position;
                        playerAction.PlayerId = netPeer.Id;

                        SentMessageToAllBut(playerAction, DeliveryMethod.ReliableSequenced, netPeer.Id);
                        break;
                    case PlayerActionType.Stop:
                        _players.GetItem(netPeer.Id)._playerData.Position = playerAction.Position;
                        playerAction.PlayerId = netPeer.Id;

                        SentMessageToAllBut(playerAction, DeliveryMethod.ReliableSequenced, netPeer.Id);
                        break;
                    default:
                        break;
                }
            }
        }

        private bool CheckMultipleAccounts(int accountId)
        {
            var players = _players.GetItems().Count(x => x.Value._playerData.accountId == accountId);

            return players > 0;

        }

        public void ProcessPlayerLogin(CLogin playerLogin, NetPeer netPeer)
        {
            var player = _players.GetItem(netPeer.Id);

            if (player._playerData.GameState != GameState.InMenu) { return; }

            var db = InitServer._databaseManager._databaseManager.AccountRepo;

            if (db == null) { return; }

            var account = db.AuthenticateAsync(playerLogin.Login, playerLogin.Password);

            if (account.Result == null) { return; }

            if (CheckMultipleAccounts(account.Result.Id))
            { return; }

            ExternalLogger.Print("account logged in: " + account.Result.Login);

            player._playerData.accountId = account.Result.Id;

            if (account.Result.Player.Name == string.Empty)
            {
                // Create character
                var newChar = new SNewChar();
                _processor.Send(netPeer, newChar, DeliveryMethod.ReliableUnordered);
                player._playerData.GameState = GameState.InCharacterCreation;
                return;
            }

            //GDLog.Print("Player logged in: " + account.Result.Player.Name);

            // Create player data
            var playerVar = account.Result.Player;

            player._playerData.Position = new Vector2(playerVar.Position.X, playerVar.Position.Y);
            player._playerData.PlayerName = playerVar.Name;
            player._playerData.playerId = playerVar.Id;

            JoinGameData(player._playerData, netPeer);
        }

        public void ProcessPlayerRegister(CNewAccount playerLogin, NetPeer netPeer)
        {
            var player = _players.GetItem(netPeer.Id);
            if (player._playerData.GameState != GameState.InMenu) { return; }

            var db = InitServer._databaseManager._databaseManager.AccountRepo;

            if (db == null) { return; }

            db.RegisterAccountAsync(playerLogin.Login, playerLogin.Password);

        }

        public void ProcessPlayerCreateChar(CNewChar playerCharacter, NetPeer netPeer)
        {
            var player = _players.GetItem(netPeer.Id);
            if (player._playerData.GameState != GameState.InCharacterCreation) { return; }

            var db = InitServer._databaseManager._databaseManager.PlayerRepo;

            if (db == null) { return; }

            var result = db.RegisterPlayerAsync(playerCharacter.Name, player._playerData.accountId).Result;

            player._playerData.PlayerName = playerCharacter.Name;
            JoinGameData(player._playerData, netPeer);

        }
        #endregion

        #region SendData

        private void JoinGameData(PlayerDataModel playerDataModel, NetPeer peer)
        {
            // Add player to the Dictionary of all players
            playerDataModel.GameState = GameState.InGame;

            // Send all players to the new player -> TO NEW PLAYER
            SendAllPlayersTo(peer);

            // Send new player to all players -> TO ALL PLAYERS
            SendPlayerDataTo(peer, playerDataModel);
        }

        private void SendPlayerDataTo(NetPeer peer, PlayerDataModel playerDataModel)
        {
            var playerData = new SPlayerData();
            playerData.PlayerDataModel = playerDataModel;

            foreach (var player in _players.GetItems())
            {
                if (player.Value._peer.Id != peer.Id)
                {
                    if (player.Value._playerData.GameState == GameState.InGame)
                    {
                        _processor.Send(player.Value._peer, playerData, DeliveryMethod.ReliableUnordered);
                    }
                }
            }
        }

        private void SendAllPlayersTo(NetPeer peer)
        {
            var playersList = _players.GetItems();

            var allPers = new SPeersAll();
            allPers.PlayerDataModels = playersList.Values.Aggregate(new List<PlayerDataModel>(), (acc, x) =>
            {
                if (x._playerData.GameState == GameState.InGame)
                    acc.Add(x._playerData);
                return acc;
            });
            _processor.Send(peer, allPers, DeliveryMethod.ReliableOrdered);
        }



        public void SentMessageToAllBut<T>(T packet, DeliveryMethod deliveryMethod, int excludePeerId) where T : class, new()
        {
            var players = _players.GetItems().Where(player => player.Value._peer.Id != excludePeerId &&
                        player.Value._playerData.GameState == GameState.InGame);

            foreach (var player in players)
            {
                _processor.Send(player.Value._peer, packet, deliveryMethod);
            }
        }

        #endregion
    }
}
