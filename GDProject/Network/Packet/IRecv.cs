﻿

namespace GdProject.Network.Packet
{
    internal interface IRecv
    {
        public void ReadPacket(int peerId);
    }
}