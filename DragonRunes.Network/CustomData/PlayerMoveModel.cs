using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonRunes.Models.CustomData;
using DragonRunes.Network.CustomData.Extension;
using LiteNetLib.Utils;

namespace DragonRunes.Network.CustomData
{
    public class PlayerMoveModel : INetSerializable
    {
        public int Index { get; set; }
        public bool IsMoving { get; set; }
        public bool IsRunning { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            Index = reader.GetInt();
            IsMoving = reader.GetBool();
            IsRunning = reader.GetBool();
            Position = reader.GetVector2();
            Direction = reader.GetVector2();
        }
        public void Serialize(NetDataWriter writer)
        {
            writer.Put(Index);
            writer.Put(IsMoving);
            writer.Put(IsRunning);
            writer.Put(Position);
            writer.Put(Direction);
        }
    }
}
