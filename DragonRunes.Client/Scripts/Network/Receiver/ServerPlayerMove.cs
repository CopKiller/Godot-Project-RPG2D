using DragonRunes.Client.Scripts.PlayerScript;
using DragonRunes.Network.Packet;
using DragonRunes.Network.Packet.Server;
using LiteNetLib;
using System;

namespace DragonRunes.Scripts.Network
{
    public partial class ClientPacketProcessor
    {
        public void ServerPlayerMove(SPlayerMove obj, NetPeer netPeer)
        {

            var playerNode = NodeManager.GetNode<RemotePlayerController>(obj.PlayerMoveModel.Index.ToString());

            if (playerNode == null) return;

            //// Sempre que precisar passar valores de uma classe para ela mesma em outro local,
            //// é melhor passar seus atributos separadamente para não ter problemas de referência

            //ExternalLogger.Print("PlayerMoveModel: Recebido" + Index.ToString() + " " + playerNode.GetType().ToString());

            playerNode.ReceivePlayerMove(obj.PlayerMoveModel);
        }
    }
}
