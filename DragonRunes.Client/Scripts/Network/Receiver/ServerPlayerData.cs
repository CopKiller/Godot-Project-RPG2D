
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
        public void ServerPlayerData(SPlayerData obj, NetPeer netPeer)
        {
            LoadPlayers(obj.PlayerDataModels, netPeer);
        }
    }
}