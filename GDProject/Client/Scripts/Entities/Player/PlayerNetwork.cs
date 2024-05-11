
using GdProject.Infrastructure;
using GdProject.Model;
using GdProject.Network;
using Network.Packet;

namespace GdProject.Client.Scripts.Entities.Player
{
    public partial class PlayerNetwork : PlayerData
    {

        private PacketProcessor packetProcessor => ClientManager.LocalPlayer.PacketProcessor;

        private CPlayerMoveAction CPlayerMoveAction;

        public void InitializePlayerNetwork()
        {
            CPlayerMoveAction = new CPlayerMoveAction();
            CPlayerMoveAction.PlayerMoveModel = new PlayerMoveModel();

            this.OnPlayerMove += SendPlayerMove;
        }

        public void SendPlayerMove()
        {
            CPlayerMoveAction.PlayerMoveModel.Position = new SharedLibrary.DataType.Vector2(Position.X, Position.Y);
            CPlayerMoveAction.PlayerMoveModel.Direction = new SharedLibrary.DataType.Vector2(LastDirection.X, LastDirection.Y);
            CPlayerMoveAction.PlayerMoveModel.isRunning = isRunning;
            CPlayerMoveAction.PlayerMoveModel.Speed = Speed;
            CPlayerMoveAction.WritePacket(packetProcessor);
        }

        public void ReceivePlayerMove(PlayerMoveModel playerMoveModel)
        {
            CPlayerMoveAction.PlayerMoveModel = playerMoveModel;

            CallDeferred(nameof(UpdatePlayerMove));
        }
        public void UpdatePlayerMove()
        {
            Position = new Godot.Vector2(CPlayerMoveAction.PlayerMoveModel.Position.X, CPlayerMoveAction.PlayerMoveModel.Position.Y);
            Direction = new Godot.Vector2(CPlayerMoveAction.PlayerMoveModel.Direction.X, CPlayerMoveAction.PlayerMoveModel.Direction.Y);
            isRunning = CPlayerMoveAction.PlayerMoveModel.isRunning;
            Speed = CPlayerMoveAction.PlayerMoveModel.Speed;
        }
    }
}
