using Server.Infrastructure;
using Server.Logger;
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

            var result = db.RegisterAccountAsync(Login, Password, Email);

            new SAlertMsg() { Msg = result.Result.Message }.WritePacket(netPacketProcessor, player._peer);

            if (result.Result.Success)
            {
                ExternalLogger.Print("account created: " + Login + " index: " + peerId);

                // Create character
                player._playerData.accountId = result.Result.EntityType.Id;
                new SNewChar().WritePacket(netPacketProcessor, player._peer);
                player.GameState = GameState.InCharacterCreation;
            }
        }
    }
}
