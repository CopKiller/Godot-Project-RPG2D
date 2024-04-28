using Server.Infrastructure;
using Server.Network;
using SharedLibrary.Extensions;

namespace Network.Packet
{
    internal class CNewAccount : IRecv
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public void ReadPacket(DictionaryWrapper<int, ServerClient> players,
            PacketProcessor netPacketProcessor, int peerId)
        {
            var player = players.GetItem(peerId);
            if (player.GameState != GameState.InMenu) { return; }

            var db = InitServer._databaseManager._databaseManager.AccountRepo;

            if (db == null) { return; }

            db.RegisterAccountAsync(Login, Password, Email);
        }
    }
}
