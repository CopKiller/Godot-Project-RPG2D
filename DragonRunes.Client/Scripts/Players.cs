using DragonRunes.Logger;
using DragonRunes.Network.CustomDataSerializable;
using Godot;
using System.Collections;
using System.Collections.Generic;

namespace DragonRunes.Client.Scripts
{
    public partial class Players : Node
    {
        public List<PlayerController> PlayerControllers { get; set; }

        public override void _Ready()
        {
            AddPlayers();
        }
        public void AddPlayers()
        {
            foreach (var player in PlayerControllers)
            {
                AddPlayer(player);
            }
        }

        private void AddPlayer(PlayerController player)
        {
            NodeManager.AddNode(player);
            AddChild(player);
            player.InitializePlayerModel();
        }

        private void RemovePlayer(int Index)
        {
            var player = PlayerControllers.Find(a => a.playerDataModel.Index == Index);
            if (player != null)
            {
                NodeManager.RemoveNode(player);
                RemoveChild(player);
                player.QueueFree();
            }
        }
    }
}
