
using GdProject.Network;
using SharedLibrary.DataType;
using LiteNetLib;

namespace Network.Packet
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
            var playerNode = NodeManager.GetNode<Player>(PlayerId.ToString());

            if (playerNode == null) return;

            // Sempre que precisar passar valores de uma classe para ela mesma em outro local,
            // é melhor passar seus atributos separadamente para não ter problemas de referência

            playerNode.PlayerAction.ActionType = ActionType;
            playerNode.PlayerAction.Direction = Direction;
            playerNode.PlayerAction.Speed = Speed;
            playerNode.PlayerAction.Running = Running;
            playerNode.CallDeferred(nameof(playerNode.UpdatePlayerAction), new Godot.Vector2(Position.X, Position.Y));
        }

        public void WritePacket(PacketProcessor netPacketProcessor)
        {
            netPacketProcessor.SendDataToServer(this, DeliveryMethod.ReliableSequenced);
        }
    }

    public enum PlayerActionType : byte
    {
        Move,
        Stop
    }

}
