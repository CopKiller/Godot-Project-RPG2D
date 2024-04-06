using LiteNetLib.Utils;
using SharedLibrary.DataType;

namespace SharedLibrary.Network.Extensions
{
    public static class NetExtensions
    {

        public static void SerializeVector2(NetDataWriter writer, Vector2 vector)
        {
            writer.Put(vector.X);
            writer.Put(vector.Y);
        }

        public static Vector2 DeserializeVector2(NetDataReader reader)
        {
            Vector2 v;
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

        /// <summary>
        /// Gets the array.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>An array of TS.</returns>
        public static T[] GetArray<T>(this NetDataReader reader) where T : INetSerializable, new()
        {
            var len = reader.GetUShort();
            var array = new T[len];
            for (int i = 0; i < len; i++)
            {
                array[i] = reader.Get<T>();
            }
            return array;
        }

        /// <summary>
        /// Puts the array.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="array">The array.</param>
        public static void PutArray<T>(this NetDataWriter writer, T[] array) where T : INetSerializable
        {
            writer.Put((ushort)array.Length);
            foreach (var obj in array)
            {
                writer.Put(obj);
            }
        }
    }
}
