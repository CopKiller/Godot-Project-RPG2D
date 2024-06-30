using DragonRunes.Models;
using DragonRunes.Models.CustomData;
using DragonRunes.Models.Enum;
using DragonRunes.Network.CustomDataSerializable.Extension;
using LiteNetLib.Utils;

namespace DragonRunes.Network.CustomDataSerializable
{
    public class PlayerDataModel: PlayerModel, INetSerializable
    {
        public int accountId { get; set; }
        public int Index { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            Index = reader.GetInt();
            Name = reader.GetString();
            Position = reader.GetVector2<Position>();
            Direction = (Direction)reader.GetByte();
            Class = (Class)reader.GetByte();
            Gender = (Gender)reader.GetByte();
        }
        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Index);
            writer.Put(Name);
            writer.Put(Position);
            writer.Put((byte)Direction);
            writer.Put((byte)Class);
            writer.Put((byte)Gender);
        }

        public PlayerDataModel(PlayerModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Position = model.Position;
            Direction = model.Direction;
            Class = model.Class;
            Gender = model.Gender;
        }

        public PlayerDataModel()
        {

        }
    }
}
