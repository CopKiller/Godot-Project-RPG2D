
using DragonRunes.Client.Scripts;
using DragonRunes.Network.Packet.Server;
using LiteNetLib;
using Godot;

namespace DragonRunes.Scripts.Network
{
    public partial class ClientPacketProcessor
    {
        public void PlayerData(SPlayerData obj, NetPeer netPeer)
        {
            var myPlayerData = NodeManager.GetNode<ClientManager>(nameof(ClientManager))._player;

            if (myPlayerData.GameState == GameState.InGame)
            {
                var myPlayerNode = NodeManager.GetNode<PlayerController>("Player");

                foreach (var player in obj.PlayerDataModels)
                {
                    var playerController = new PlayerController();
                    playerController.playerDataModel = player;

                    if (player.Index == myPlayerData.PlayerData.Index)
                    {
                        myPlayerNode.CallDeferred(nameof(myPlayerNode.AddLocalPlayer), playerController);
                        continue;
                    }
                    myPlayerNode.CallDeferred(nameof(myPlayerNode.DuplicatePlayer), playerController);
                }
            }
            else if (myPlayerData.GameState == GameState.InLogin)
            {
                var root = NodeManager.GetNode<Node2D>("Root");
                SceneManager.LoadScene(root, nameof(Game));
                myPlayerData.GameState = GameState.InGame;
                PlayerData(obj, netPeer);
            }
        }
    }
}