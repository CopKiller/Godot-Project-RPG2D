using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GdProject.Shared.Scripts.Network.Packet.Client
{
    public class CNewChar : INetSerializable
    {
        public string Name { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            Name = reader.GetString();
        }
        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Name);
        }

    }
}
