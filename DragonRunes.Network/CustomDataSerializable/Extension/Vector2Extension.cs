using DragonRunes.Models.CustomData;
using DragonRunes.Models.Enum;
using LiteNetLib.Utils;
using System.Runtime.CompilerServices;

namespace DragonRunes.Network.CustomDataSerializable.Extension
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

            v.X = reader.GetInt();
            v.Y = reader.GetInt();

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

        // Conversão estática para tipos que herdam de Vector2
        public static void ReplicateData(this Vector2 receiverData, Vector2 newData)
        {
            receiverData.X = newData.X;
            receiverData.Y = newData.Y;
        }

        public static int ToInt(this float vector) 
        {
            return Convert.ToInt32(vector);
        }

        public static void Snapped<T>(this T position, T step) where T : Vector2
        {
            position.X = Snapped(position.X, step.X);
            position.Y = Snapped(position.Y, step.Y);
        }

        private static float Snapped(float s, float step)
        {
            if (step != 0f)
            {
                return MathF.Floor(s / step + 0.5f) * step;
            }

            return s;
        }
    }
}
