
using GdProject.Model;
using LiteNetLib;

namespace GdProject.Network.Packet.Server
{
    internal class SPlayerData : IRecv
    {
        public PlayerDataModel PlayerDataModel { get; set; }

        public void ReadPacket(int peerId)
        {
            
        }
        
    }
}
