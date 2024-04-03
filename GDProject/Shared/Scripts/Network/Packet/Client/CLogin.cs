using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GdProject.Shared.Scripts.Network.Packet.Client
{
    public class CLogin : INetSerializable
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            Username = reader.GetString();
            Password = reader.GetString();
        }
        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Username);
            writer.Put(Password);
        }

    }
}
