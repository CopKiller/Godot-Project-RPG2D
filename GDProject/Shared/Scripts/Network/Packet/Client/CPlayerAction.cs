using GdProject.Shared.Scripts.Network.Extensions;
using Godot;
using LiteNetLib.Utils;

namespace GdProject.Shared.Scripts.Network.Packet.Client
{
    public class CPlayerAction : INetSerializable
    {
        public int PlayerId { get; set; }
        public PlayerActionType ActionType { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public bool Running { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            PlayerId = reader.GetInt();
            ActionType = (PlayerActionType)reader.GetByte();
            Position = reader.GetVector2();
            Direction = reader.GetVector2();
            Speed = reader.GetFloat();
            Running = reader.GetBool();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(PlayerId);
            writer.Put((byte)ActionType);
            writer.Put(Position);
            writer.Put(Direction);
            writer.Put(Speed);
            writer.Put(Running);
        }
    }

    public enum PlayerActionType: byte
    {
        Move,
        Stop
    }

}
