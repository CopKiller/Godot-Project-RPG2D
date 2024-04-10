
using SharedLibrary.DataType;
using LiteNetLib;

namespace GdProject.Network.Packet.Client
{
    public class CPlayerAction : IRecv, ISend
    {
        public int PlayerId { get; set; }
        public PlayerActionType ActionType { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public bool Running { get; set; }

        public void ReadPacket(int peerId)
        {
            
        }

        public void WritePacket(PacketProcessor netPacketProcessor)
        {
            
        }
    }

    public enum PlayerActionType: byte
    {
        Move,
        Stop
    }

}
