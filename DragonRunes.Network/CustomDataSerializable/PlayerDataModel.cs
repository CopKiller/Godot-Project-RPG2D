using DragonRunes.Models;
using DragonRunes.Models.CustomData;
using DragonRunes.Network.CustomDataSerializable.Extension;
using LiteNetLib.Utils;

namespace DragonRunes.Network.CustomDataSerializable
{
    public class PlayerDataModel: PlayerModel,  INetSerializable
    {
        public int Index { get; set; }

        private readonly PlayerModel model;

        public void Deserialize(NetDataReader reader)
        {
            Index = reader.GetInt();
            Name = reader.GetString();
            Position = reader.GetVector2<Position>();
            Direction = reader.GetVector2<Direction>();
            model.Class = (Class)reader.GetByte();
            Gender = (Gender)reader.GetByte();
        }
        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Index);
            writer.Put(model.Name);
            writer.Put(model.Position);
            writer.Put(model.Direction);
            writer.Put((byte)model.Class);
            writer.Put((byte)model.Gender);
        }

        public PlayerDataModel(PlayerModel model)
        {
            this.model = model;
        }

        public PlayerDataModel()
        {

        }
    }
}
