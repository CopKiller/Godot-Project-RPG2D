using DragonRunes.Models.CustomData;
using LiteNetLib.Utils;
using System.Runtime.CompilerServices;

namespace DragonRunes.Shared.CustomDataSerializable.Extension
{
    public static class Vector2Extension
    {
        public static void SerializeVector2<T>(NetDataWriter writer, T vector) where T : Vector2, new()
        {
            writer.Put(vector.X);
            writer.Put(vector.Y);
        }
        public static T DeserializeVector2<T>(NetDataReader reader) where T : Vector2, new()
        {
            var v = new T();

            v.X = reader.GetFloat();
            v.Y = reader.GetFloat();

            return v;
        }
        public static T GetVector2<T>(this NetDataReader reader) where T : Vector2, new()
        {
            return DeserializeVector2<T>(reader);
        }
        public static void Put<T>(this NetDataWriter writer, T vector) where T : Vector2, new()
        {
            SerializeVector2(writer, vector);
        }
    }
}
