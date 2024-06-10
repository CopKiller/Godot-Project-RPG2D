
using EntityFramework.Entities.ValueObjects.Player;
using LiteNetLib.Utils;
using Server.Network.Extensions;
using SharedLibrary.DataType;

namespace DragonRunes.Server.Model
{
    public class PlayerPhysicModel : INetSerializable
    {
        public int Index { get; set; }
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Direction { get; set; } = Vector2.Zero;
        public float Speed { get; set; } = 100;
        public bool isRunning { get; set; } = false;

        public void Deserialize(NetDataReader reader)
        {
            Index = reader.GetInt();
            Position = reader.GetVector2();
            Direction = reader.GetVector2();
            Speed = reader.GetFloat();
            isRunning = reader.GetBool();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Index);
            writer.Put(Position);
            writer.Put(Direction);
            writer.Put(Speed);
            writer.Put(isRunning);
        }

        public void ConvertPosition(Position position)
        {
            Position = new Vector2(position.X, position.Y);
        }

        public void ConvertDirection(Direction direction)
        {
            Direction = new Vector2(direction.X, direction.Y);
        }
    }
}
