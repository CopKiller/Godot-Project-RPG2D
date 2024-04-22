
using GdProject.Client;
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


        public void ReadPacket(int peerId)
        {
            if (PlayerDataModels.Count == 0) { return; }

            ExternalLogger.Print("SPeersAll");
            ExternalLogger.Print($"PlayerDataModels: {PlayerDataModels.Count}");
            ExternalLogger.Print($"{InitClient.LocalPlayer.RemotePeer.Id} - local {peerId}");

            Player MyPlayer = NodeManager.GetNode<Player>("Player");

            foreach (var playerData in PlayerDataModels)
            {
                ExternalLogger.Print($"PlayerDataModels: {playerData.PlayerName}");
                if (playerData.Index == InitClient.LocalPlayer.RemotePeer.RemoteId)
                {
                    var pData = new Player();
                    pData.PlayerData = playerData;

                    MyPlayer.CallDeferred(nameof(MyPlayer.AddLocalPlayer), pData);

                    ExternalLogger.Print($"PlayerDataModels: {playerData.PlayerName} - Local");
                }
                else
                {
                    var pData = new Player();
                    pData.PlayerData = playerData;

                    MyPlayer.CallDeferred("DuplicatePlayer", pData);
                }
            }

            var clientNode = NodeManager.GetNode<ClientNode>("Client");
            clientNode.CallDeferred(method: nameof(clientNode.InitGame));
        }

    }
}
