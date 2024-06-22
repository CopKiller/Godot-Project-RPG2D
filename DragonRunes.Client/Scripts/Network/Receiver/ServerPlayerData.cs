
using DragonRunes.Client.Scripts;
using DragonRunes.Network.Packet.Server;
using LiteNetLib;
using Godot;
using DragonRunes.Logger;
using System.Collections.Generic;

namespace DragonRunes.Scripts.Network
{
    public partial class ClientPacketProcessor
    {
        //public Queue<SPlayerData> _packetQueue = new Queue<SPlayerData>();

        public void ServerPlayerData(SPlayerData obj, NetPeer netPeer)
        {
            //var clientManager = NodeManager.GetNode<ClientManager>(nameof(ClientManager));

            ////if (clientManager._player.GameState != GameState.InGame)
            ////{
            ////    _packetQueue.Enqueue(obj);
            ////    return;
            ////}

            //var sceneManager = NodeManager.GetNode<SceneManager>(nameof(SceneManager));

            //var playersNode = NodeManager.GetNode<Players>(nameof(Players));

            //var playerScene = sceneManager.GetScene("Player");

            //foreach (var player in obj.PlayerDataModels)
            //{
            //    var playerInstance = playerScene.Instantiate() as PlayerController;

            //    playerInstance.Name = player.Name;

            //    playerInstance.playerDataModel = player;

            //    if (playerInstance.playerDataModel.Index == netPeer.RemoteId)
            //    {
            //        playerInstance.IsLocalPlayer = true;
            //    }

            //    playersNode.CallDeferred(nameof(playersNode.AddPlayer), playerInstance);
            //}
        }
    }
}