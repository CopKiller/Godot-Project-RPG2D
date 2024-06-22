using DragonRunes.Client.Scripts;
using DragonRunes.Client.Scripts.SceneScript.MainMenu.Windows;
using DragonRunes.Network.CustomDataSerializable;
using DragonRunes.Network.Packet;
using DragonRunes.Network.Packet.Server;
using Godot;
using LiteNetLib;
using System.Collections.Generic;

namespace DragonRunes.Scripts.Network
{
    public partial class ClientPacketProcessor
    {
        public void ServerInGame(SInGame obj, NetPeer netPeer)
        {
            var sceneManager = NodeManager.GetNode<SceneManager>(nameof(SceneManager));

            var playersInstance = sceneManager.GetScene("Players").Instantiate() as Players;
            playersInstance.Name = nameof(Players);

            var playerController = sceneManager.GetScene("Player");

            var listPlayers = new List<PlayerController>();

            foreach(var player in obj.PlayerDataModels)
            {
                var playerInstance = playerController.Instantiate() as PlayerController;
                playerInstance.Name = player.Name;
                playerInstance.playerDataModel = player;

                if (playerInstance.playerDataModel.Index == netPeer.RemoteId)
                {
                    playerInstance.IsLocalPlayer = true;
                }

                listPlayers.Add(playerInstance);
            }

            playersInstance.PlayerControllers = listPlayers;

            NodeManager.AddNode(playersInstance);

            sceneManager.CallDeferred(nameof(sceneManager.LoadScene), nameof(Game));
        }
    }
}
