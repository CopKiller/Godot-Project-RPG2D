using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonRunes.Network.CustomData
{
    public struct Vector2 : INetSerializable
    {
        public float X { get; set; }
        public float Y { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            X = reader.GetFloat();
            Y = reader.GetFloat();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put(X);
            writer.Put(Y);
        }
    }
}
