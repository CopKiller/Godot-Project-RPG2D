using LiteNetLib.Utils;

namespace Server.Model
{
    public class PlayerDataModel : INetSerializable
    {
        public int playerId { get; set; } // Database of player id non serializable
        public int accountId { get; set; } // Database of player non serializable
        public int Index { get; set; }
        public string PlayerName { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            Index = reader.GetInt();
            PlayerName = reader.GetString();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Index);
            writer.Put(PlayerName);
        }
    }

}
