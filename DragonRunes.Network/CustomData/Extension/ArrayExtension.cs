using DragonRunes.Network.CustomData;
using LiteNetLib.Utils;

namespace DragonRunes.Network.CustomData.Extension
{
    public static class ArrayExtension
    {
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
