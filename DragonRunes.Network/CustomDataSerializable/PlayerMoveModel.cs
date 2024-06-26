
using DragonRunes.Models.CustomData;
using DragonRunes.Network.CustomDataSerializable.Extension;
using LiteNetLib.Utils;

namespace DragonRunes.Network.CustomDataSerializable
{
    public class PlayerMoveModel : INetSerializable
    {
        public int Index { get; set; }
        public bool IsRunning { get; set; }
        public Position Position { get; set; }
        public Direction Direction { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            Index = reader.GetInt();
            IsRunning = reader.GetBool();
            Position = reader.GetVector2<Position>();
            Direction = reader.GetVector2<Direction>();
        }
        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Index);
            writer.Put(IsRunning);
            writer.Put(Position);
            writer.Put(Direction);
        }
    }
}
