using LiteNetLib;
using LiteNetLib.Utils;
using Server.Infrastructure;
using SharedLibrary.Extensions;

namespace Server.Network.Packet.Client
{
    internal class CNewChar : IRecv
    {
        public string Name { get; set; }

        public void ReadPacket(DictionaryWrapper<int, ServerClient> players,
            PacketProcessor netPacketProcessor, int peerId)
        {
            var player = players.GetItem(peerId);
            if (player._playerData.GameState != GameState.InCharacterCreation) { return; }

            var db = InitServer._databaseManager._databaseManager.PlayerRepo;

            if (db == null) { return; }

            var result = db.RegisterPlayerAsync(Name, player._playerData.accountId).Result;


            player._playerData.PlayerName = Name;

            var joinGameData = new CLogin();
            joinGameData.JoinGameData(players, netPacketProcessor, peerId);
        }

    }
}
