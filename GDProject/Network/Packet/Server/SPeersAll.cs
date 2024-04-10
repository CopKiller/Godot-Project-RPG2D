

using LiteNetLib.Utils;
using LiteNetLib;
using GdProject.Model;
using System.Collections.Generic;

namespace GdProject.Network.Packet.Server
{
    internal class SPeersAll : IRecv
    {
        public List<PlayerDataModel> PlayerDataModels { get; set; }

        
        public void ReadPacket(int peerId)
        {
            
        }

    }
}
