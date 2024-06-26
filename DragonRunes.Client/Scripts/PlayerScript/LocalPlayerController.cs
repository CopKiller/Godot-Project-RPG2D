using DragonRunes.Network.CustomDataSerializable;
using DragonRunes.Scripts.Network;
using Godot;
using LiteNetLib;

namespace DragonRunes.Client.Scripts.PlayerScript
{
    public partial class LocalPlayerController : RemotePlayerController
    {
        public ClientPacketProcessor packetProcessor { get; set; }
        public NetPeer serverPeer;
        private PlayerMoveModel playerMoveModel;

        public override void _Ready()
        {
            base._Ready();

            playerMoveModel = new PlayerMoveModel();
            playerMoveModel.Position = playerDataModel.Position;
            playerMoveModel.Direction = playerDataModel.Direction;
        }

        // --> This is the only difference between LocalPlayerController and RemotePlayerController
        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventKey)
            {
                SetInputDirection();
                SetInputRunning();
                SendPlayerMove();
            }
        }

        private void SetInputDirection()
        {
            Direction.X = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
            Direction.Y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
            Direction.Normalized();
        }

        private void SetInputRunning()
        {
            isRunning = Input.IsActionPressed("ui_running");
        }

        public void SendPlayerMove()
        {
            if (isMoving)
            {
                if (packetProcessor == null)
                {
                    GD.PrintErr("packetProcessor is null");
                    return;
                }

                playerMoveModel.IsRunning = isRunning;
                //GD.Print($"PlayerMoveModel: Enviado Position` X: {Position.X} Y: {Position.Y}");
                //GD.Print($"PlayerMoveModel: Enviado Direction` X: {Direction.X} Y: {Direction.Y}");


                //playerMoveModel.Position = Position;
                //playerMoveModel.Direction = Direction;
                packetProcessor.ClientPlayerMove(serverPeer, playerMoveModel);
            }
        }
    }
}
