using DragonRunes.Client.Scripts;
using DragonRunes.Client.Scripts.SceneScript.MainMenu.Windows;
using DragonRunes.Logger;
using DragonRunes.Network.CustomDataSerializable;
using DragonRunes.Network.Packet;
using DragonRunes.Network.Packet.Server;
using Godot;
using LiteNetLib;
using System.Collections.Generic;
using System.Linq;

namespace DragonRunes.Scripts.Network
{
    public partial class ClientPacketProcessor
    {
        public void ServerInGame(SInGame obj, NetPeer netPeer)
        {
            // Load Players & Save in NodeManager
            LoadPlayers(obj.PlayerDataModels, netPeer);

            // Load Game Scene
            LoadGameScene();
        }

        private void LoadPlayers(List<PlayerDataModel> PlayerDataModels, NetPeer netPeer)
        {
            var playersNode = NodeManager.GetNode<Players>(nameof(Players));

            if (playersNode == null)
            {
                playersNode = InstantiatePlayersNode();
                playersNode.PlayerControllers = InstantiatePlayersController(PlayerDataModels, netPeer);
                NodeManager.AddNode(playersNode);
            } else
            {
                playersNode.PlayerControllers = InstantiatePlayersController(PlayerDataModels, netPeer);
                playersNode.CallDeferred(nameof(playersNode.AddPlayers));
                Logg.Logger.Log("Players added to Players Node");
            }
        }

        private List<PlayerController> InstantiatePlayersController(List<PlayerDataModel> PlayerDataModels, NetPeer netPeer)
        {
            var sceneManager = NodeManager.GetNode<SceneManager>(nameof(SceneManager));

            var playerScene = sceneManager.GetScene("Player");

            return PlayerDataModels.Select(playerDataModel =>
            {
                var playerController = playerScene.Instantiate() as PlayerController;
                if (playerController != null)
                {
                    playerController.Name = playerDataModel.Index.ToString();
                    playerController.playerDataModel = playerDataModel;
                    playerController.IsLocalPlayer = playerDataModel.Index == netPeer.RemoteId;
                }
                return playerController;
            }).ToList();
        }

        private Players InstantiatePlayersNode()
        {
            var sceneManager = NodeManager.GetNode<SceneManager>(nameof(SceneManager));
            var playersInstance = sceneManager.GetScene("Players").Instantiate() as Players;
            if (playersInstance != null)
            {
                playersInstance.Name = nameof(Players);
            }
            return playersInstance;
        }

        private void LoadGameScene()
        {
            var sceneManager = NodeManager.GetNode<SceneManager>(nameof(SceneManager));
            sceneManager.CallDeferred(nameof(sceneManager.LoadScene), nameof(Game));
        }
    }
}
