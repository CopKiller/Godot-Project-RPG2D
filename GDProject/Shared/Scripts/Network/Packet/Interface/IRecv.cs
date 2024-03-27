using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GdProject.Shared.Scripts.Network.Packet.Interface
{
    internal interface IRecv
    {
        void Recv(byte[] data);
    }
}
