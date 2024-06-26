using DragonRunes.Client.Scripts.ControlsBase;
using DragonRunes.Client.Scripts.PlayerScript;
using DragonRunes.Logger;
using DragonRunes.Network.CustomDataSerializable;
using Godot;
using System.Collections;
using System.Collections.Generic;

namespace DragonRunes.Client.Scripts
{
    public partial class Players : Node
    {
        public LocalPlayerController localPlayerController { get; set; }
        public List<RemotePlayerController> remotePlayerController { get; set; }

        public override void _Ready()
        {
            AddPlayer(localPlayerController);

            foreach (var player in remotePlayerController)
            {
                AddPlayer(player);
            }
        }

        private void AddPlayer<T>(T player) where T : Node
        {
            NodeManager.AddNode(player as T);
            AddChild(player);
        }

        private void RemovePlayer(int Index)
        {
            //var player = PlayerControllers.Find(a => a.playerDataModel.Index == Index);
            //if (player != null)
            //{
            //    NodeManager.RemoveNode(player);
            //    RemoveChild(player);
            //    player.QueueFree();
            //}
        }
    }
}
