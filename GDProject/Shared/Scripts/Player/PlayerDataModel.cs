using GdProject.Shared.Scripts.Player;
using Godot;
using System;

namespace Shared.Scripts.Player
{
    public partial class PlayerDataModel : GodotObject
    {
        public int Index { get; set; }
        public long PlayerId { get; set; }
        public string PlayerName { get; set; }
        public ClassType Class { get; set; }
        public int Speed { get; set; } = 60;
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 LastDirection { get; set; } = Vector2.Zero;
        public PlayerActionsModel Actions { get; set; }
    }
}
