using GdProject.Network.Extensions;
using LiteNetLib.Utils;
using SharedLibrary.DataType;

namespace GdProject.Model
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
    }
}
