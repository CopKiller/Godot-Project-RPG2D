using DragonRunes.Logger;
using DragonRunes.Network.Packet;
using DragonRunes.Network.Packet.Server;
using LiteNetLib;
using System;
namespace DragonRunes.Scripts.Network
{
    public partial class ClientPacketProcessor
    {
        public void ServerLeft(SLeft obj, NetPeer netPeer)
        {

            NodeManager.GetNode<PlayerController>(obj.Index.ToString()).QueueFree();
        }
    }
}
