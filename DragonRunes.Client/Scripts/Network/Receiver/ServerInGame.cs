using DragonRunes.Client.Scripts;
using DragonRunes.Client.Scripts.PlayerScript;
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
            var sceneManager = NodeManager.GetNode<SceneManager>(nameof(SceneManager));
            var playersNode = NodeManager.GetNode<Players>(nameof(Players));
            var playerScene = sceneManager.GetScene("Player");
            var remotePlayerScene = sceneManager.GetScene("RemotePlayer");

            GD.Print("Players: " + PlayerDataModels.Count);

            foreach (var playerDataModel in PlayerDataModels)
            {
                GD.Print("Player: " + playerDataModel.Index);
                GD.Print("Player: " + playerDataModel.Name);
            }

            GD.Print("LocalPlayerRemoteId: " + netPeer.RemoteId);

            if (playersNode == null)
            {
                playersNode = InstantiatePlayersNode();
            }

            NodeManager.AddNode(playersNode);

            var localPlayerData = PlayerDataModels.FirstOrDefault(playerDataModel => playerDataModel.Index == netPeer.RemoteId);
            var othersPlayerData = PlayerDataModels.Where(playerDataModel => playerDataModel.Index != netPeer.RemoteId).ToList();

            if (localPlayerData != null)
            {
                var localPlayerController = playerScene.InstantiateOrNull<LocalPlayerController>();
                localPlayerController.Name = "LocalPlayer";
                localPlayerController.playerDataModel = localPlayerData;
                var packetProcessor = NodeManager.GetNode<ClientManager>(nameof(ClientManager))._networkService._clientPacketProcessor;
                localPlayerController.packetProcessor = packetProcessor;
                localPlayerController.serverPeer = netPeer;
                playersNode.localPlayerController = localPlayerController;
                //playersNode.CallDeferred(nameof(playersNode.AddPlayer), localPlayerController);
            }

            var list = new List<RemotePlayerController>();
            foreach (var playerDataModel in othersPlayerData)
            {
                var remotePlayerController = remotePlayerScene.InstantiateOrNull<RemotePlayerController>();
                remotePlayerController.Name = playerDataModel.Index.ToString();
                remotePlayerController.playerDataModel = playerDataModel;
                //playersNode.CallDeferred(nameof(playersNode.AddPlayer), remotePlayerController);
                list.Add(remotePlayerController);
            }
            playersNode.remotePlayerController = list;
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
