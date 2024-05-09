
using GdProject.Client;
using GdProject.Infrastructure;
using GdProject.Model;
using Godot;

namespace Network.Packet
{
    internal class SPlayerData : IRecv
    {
        public PlayerDataModel PlayerDataModel { get; set; }
        public PlayerPhysicModel PlayerPhysicModel { get; set; }

        public void ReadPacket(int peerId)
        {
            var MyPlayer = NodeManager.GetNode<PlayerController>("Player");

            var pController = new PlayerController();

            pController.playerDataModel = PlayerDataModel;
            pController.playerPhysicModel = PlayerPhysicModel;

            MyPlayer.CallDeferred(nameof(MyPlayer.DuplicatePlayer), pController);
        }
    }
}
