using DragonRunes.Network.CustomData.Extension;
using LiteNetLib.Utils;

namespace DragonRunes.Network.CustomData
{
    public class PlayerDataModel : INetSerializable
    {
        public int Index { get; set; }
        public string Name { get; set; } = string.Empty;
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public Class Class { get; set; }
        public Gender Gender { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            Index = reader.GetInt();
            Name = reader.GetString();
            Position = reader.GetVector2();
            Direction = reader.GetVector2();
            Class = (Class)reader.GetByte();
            Gender = (Gender)reader.GetByte();
        }
        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Index);
            writer.Put(Name);
            writer.Put(Position);
            writer.Put(Direction);
            writer.Put((byte)Class);
            writer.Put((byte)Gender);
        }
    }
}
