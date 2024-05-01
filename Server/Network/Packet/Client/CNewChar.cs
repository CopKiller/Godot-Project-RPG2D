using Server.Infrastructure;
using Server.Network;
using SharedLibrary.Extensions;

namespace Network.Packet
{
    internal class CNewChar : IRecv
    {
        public string Name { get; set; }

        public void ReadPacket(DictionaryWrapper<int, ServerClient> players,
            PacketProcessor netPacketProcessor, int peerId)
        {
            var player = players.GetItem(peerId);
            if (player.GameState != GameState.InCharacterCreation) { return; }

            var db = InitServer._databaseManager._databaseManager.PlayerRepo;

            if (db == null) { return; }

            var result = db.RegisterPlayerAsync(Name, player._playerData.accountId).Result;

            if (result.Success == false)
            {
                new SAlertMsg() { Msg = result.Message }.WritePacket(netPacketProcessor, player._peer);
                return;
            }
            else
            {
                player._playerData.PlayerName = Name;
                var joinGameData = new CLogin();
                joinGameData.JoinGameData(players, netPacketProcessor, peerId);
            }
        }

    }
}
