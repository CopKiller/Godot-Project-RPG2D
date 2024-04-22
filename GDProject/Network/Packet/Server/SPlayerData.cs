
using GdProject.Client;
using GdProject.Infrastructure;
using GdProject.Model;
using Godot;

namespace Network.Packet
{
    internal class SPlayerData : IRecv
    {
        public PlayerDataModel PlayerDataModel { get; set; }

        public void ReadPacket(int peerId)
        {
            var MyPlayer = NodeManager.GetNode<Player>("Player");

            var pData = new Player();
            pData.PlayerData = PlayerDataModel;

            MyPlayer.CallDeferred("DuplicatePlayer", pData);
        }
    }
}
