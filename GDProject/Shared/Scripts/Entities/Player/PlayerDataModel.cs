using GdProject.Shared.Scripts.Network.Extensions;
using Godot;
using LiteNetLib.Utils;

namespace GdProject.Shared.Scripts.Entities.Player
{
    public class PlayerDataModel : INetSerializable
    {
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
