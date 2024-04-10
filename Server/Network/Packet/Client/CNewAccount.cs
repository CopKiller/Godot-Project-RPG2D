
using LiteNetLib;
using LiteNetLib.Utils;
using Server.Infrastructure;
using SharedLibrary.Extensions;

namespace Server.Network.Packet.Client
{
    internal class CNewAccount : IRecv
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public void ReadPacket(DictionaryWrapper<int, ServerClient> players,
            PacketProcessor netPacketProcessor, int peerId)
        {
            var player = players.GetItem(peerId);
            if (player._playerData.GameState != GameState.InMenu) { return; }

            var db = InitServer._databaseManager._databaseManager.AccountRepo;

            if (db == null) { return; }

            db.RegisterAccountAsync(Login, Password);
        }
    }
}
