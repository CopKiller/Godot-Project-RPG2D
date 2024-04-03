using Godot;
using LiteNetLib.Utils;
using System;

namespace GdProject.Shared.Scripts.Entities.Class
{
    public class ClassModel : INetSerializable
    {
        public ClassType Class { get; set; }
        public AnimatedSprite2D AnimatedSprite { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            Class = (ClassType)reader.GetByte();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put((byte)Class);
        }
    }
    //{
    //    public ClassType Class { get; set; }
    //    public AnimatedSprite2D AnimatedSprite { get; set; }
    //}
}
