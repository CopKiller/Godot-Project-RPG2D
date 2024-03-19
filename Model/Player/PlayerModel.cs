using Godot;
using GodotProject.scripts.Class;
using System;

namespace GodotProject.Model.Player
{
    public partial class PlayerModel : CharacterBody2D
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public ClassModel Class { get; set; }
        public int Speed { get; set; } = 60;
        public Vector2 LastDirection { get; set; } = Vector2.Zero;

        public void LoadPlayer(int id, string playerName, ClassType classType)
        {
            Id = id;
            PlayerName = playerName;
            LoadClass(classType);
        }
        private void LoadClass(ClassType classType)
        {
            Class = new ClassModel();
            Class.Class = classType;

            GD.Print(Enum.GetName(typeof(ClassType), classType));
            Class.AnimatedSprite = GetNode<AnimatedSprite2D>(Enum.GetName(typeof(ClassType), classType));
        }
    }
}
