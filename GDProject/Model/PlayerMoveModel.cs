
using GdProject.Network.Extensions;
using LiteNetLib.Utils;
using SharedLibrary.DataType;

namespace GdProject.Model
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
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Position);
            writer.Put(Direction);
            writer.Put(isRunning);
        }

    }
}
