using DragonRunes.Models;
using DragonRunes.Models.CustomData;
using DragonRunes.Shared.CustomDataSerializable.Extension;
using LiteNetLib.Utils;

namespace DragonRunes.Shared.CustomDataSerializable
{
    public class PlayerDataModel : PlayerModel, INetSerializable
    {
        public int Index { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            Index = reader.GetInt();
            Name = reader.GetString();
            Position = reader.GetVector2<Position>();
            Direction = reader.GetVector2<Direction>();
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
