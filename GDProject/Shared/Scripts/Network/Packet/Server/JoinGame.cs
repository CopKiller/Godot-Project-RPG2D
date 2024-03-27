using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GdProject.Shared.Scripts.Network.Packet
{
    public class JoinGame: INetSerializable
    {
        public string PlayerName { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            PlayerName = reader.GetString();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(PlayerName);
        }
    }
}
