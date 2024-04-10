using GdProject.Network.Extensions;
using LiteNetLib.Utils;
using SharedLibrary.DataType;

namespace GdProject.Model
{
    public class PlayerDataModel : INetSerializable
    {
        public int playerId { get; set; } // Database of player
        public int accountId { get; set; } // Database of player
        public GameState GameState { get; set; } = GameState.InMenu;
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
