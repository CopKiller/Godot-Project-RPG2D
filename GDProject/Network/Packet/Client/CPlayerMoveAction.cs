
using GdProject.Network;
using SharedLibrary.DataType;
using LiteNetLib;
using GdProject.Model;

namespace Network.Packet
{
    public class CPlayerMoveAction : IRecv, ISend
    {
        public int Index { get; set; }

        public PlayerMoveModel PlayerMoveModel { get; set; }

        public void ReadPacket(int peerId)
        {
            var playerNode = NodeManager.GetNode<PlayerController>(Index.ToString());

            if (playerNode == null) return;

            // Sempre que precisar passar valores de uma classe para ela mesma em outro local,
            // é melhor passar seus atributos separadamente para não ter problemas de referência

            playerNode.ReceivePlayerMove(PlayerMoveModel);
        }

        public void WritePacket(PacketProcessor netPacketProcessor)
        {
            netPacketProcessor.SendDataToServer(this, DeliveryMethod.ReliableSequenced);
        }
    }

}
