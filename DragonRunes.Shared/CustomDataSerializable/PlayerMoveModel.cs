
using DragonRunes.Models.CustomData;
using DragonRunes.Shared.CustomDataSerializable.Extension;
using LiteNetLib.Utils;

namespace DragonRunes.Shared.CustomDataSerializable
{
    public class PlayerMoveModel : INetSerializable
    {
        public int Index { get; set; }
        public bool IsMoving { get; set; }
        public bool IsRunning { get; set; }
        public Position Position { get; set; }
        public Direction Direction { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            Index = reader.GetInt();
            IsMoving = reader.GetBool();
            IsRunning = reader.GetBool();
            Position = reader.GetVector2<Position>();
            Direction = reader.GetVector2<Direction>();
        }
        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Index);
            writer.Put(IsMoving);
            writer.Put(IsRunning);
            writer.Put(Position);
            writer.Put(Direction);
        }
    }
}
