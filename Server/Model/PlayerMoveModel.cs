
using LiteNetLib.Utils;
using Server.Network.Extensions;
using SharedLibrary.DataType;

namespace Server.Model
{
    public class PlayerMoveModel : INetSerializable
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Direction { get; set; } = Vector2.Zero;

        public bool isRunning = false;

        public float Speed { get; set; } = 100;

        public void Deserialize(NetDataReader reader)
        {
            Position = reader.GetVector2();
            Direction = reader.GetVector2();
            isRunning = reader.GetBool();
            Speed = reader.GetFloat();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Position);
            writer.Put(Direction);
            writer.Put(isRunning);
            writer.Put(Speed);
        }

    }
}
