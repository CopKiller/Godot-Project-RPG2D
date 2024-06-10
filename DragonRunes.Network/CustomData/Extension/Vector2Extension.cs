using DragonRunes.Network.CustomData;
using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonRunes.Network.CustomData.Extension
{
    public static class Vector2Extension
    {
        public static void SerializeVector2(NetDataWriter writer, Vector2 vector)
        {
            writer.Put(vector.X);
            writer.Put(vector.Y);
        }
        public static Vector2 DeserializeVector2(NetDataReader reader)
        {
            Vector2 v = new Vector2();

            v.X = reader.GetFloat();
            v.Y = reader.GetFloat();

            return v;
        }
        public static Vector2 GetVector2(this NetDataReader reader)
        {
            return DeserializeVector2(reader);
        }
        public static void Put(this NetDataWriter writer, Vector2 vector)
        {
            SerializeVector2(writer, vector);
        }
    }
}
