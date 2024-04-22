using LiteNetLib.Utils;
using Server.Network.Extensions;
using SharedLibrary.DataType;

namespace Server.Model
{
    public class PlayerDataModel : INetSerializable
    {
        public int playerId { get; set; } // Database of player
        public int accountId { get; set; } // Database of player
        public int Index { get; set; }
        public string PlayerName { get; set; }
        public Vector2 Position { get; set; } = Vector2.Zero;

        public void Deserialize(NetDataReader reader)
        {
            Index = reader.GetInt();
            PlayerName = reader.GetString();
            Position = reader.GetVector2();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Index);
            writer.Put(PlayerName);
            writer.Put(Position);
        }
    }

}
