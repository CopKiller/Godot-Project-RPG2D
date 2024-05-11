
using GdProject.Client;
using GdProject.Client.Scripts.Entities.Player;
using GdProject.Infrastructure;
using GdProject.Logger;
using GdProject.Model;
using Godot;
using System;
using System.Collections.Generic;

namespace Network.Packet
{
    internal class SPeersAll : IRecv
    {
        public List<PlayerDataModel> PlayerDataModels { get; set; }
        public List<PlayerPhysicModel> PlayerPhysicModels { get; set; }

        public void ReadPacket(int peerId)
        {
            if (PlayerDataModels.Count == 0) { return; }
            if (PlayerPhysicModels.Count == 0) { return; }

            ExternalLogger.Print($"SPeersAll Received");

            var dict = new Dictionary<PlayerDataModel, PlayerPhysicModel>();

            for (int i = 0; i < PlayerDataModels.Count; i++)
            {
                dict.Add(PlayerDataModels[i], PlayerPhysicModels[i]);
            }

            PlayerController MyPlayer = NodeManager.GetNode<PlayerController>("Player");

            foreach (KeyValuePair<PlayerDataModel, PlayerPhysicModel> par in dict)
            {
                var pController = new PlayerController();

                pController.playerDataModel = par.Key;

                pController.playerPhysicModel = par.Value;

                if (par.Key.Index == ClientManager.LocalPlayer.RemotePeer.RemoteId)
                {
                    MyPlayer.CallDeferred(nameof(MyPlayer.AddLocalPlayer), pController);
                }
                else
                {
                    MyPlayer.CallDeferred(nameof(MyPlayer.DuplicatePlayer), pController);
                }
            }

            var clientNode = NodeManager.GetNode<ClientNode>("Client");
            clientNode.CallDeferred(method: nameof(clientNode.InitGame));
        }

    }
}
