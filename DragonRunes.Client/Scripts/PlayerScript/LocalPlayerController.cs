using DragonRunes.Models.Enum;
using DragonRunes.Network.CustomDataSerializable;
using DragonRunes.Network.CustomDataSerializable.Extension;
using DragonRunes.Scripts.Network;
using Godot;
using LiteNetLib;
using System;

namespace DragonRunes.Client.Scripts.PlayerScript
{
    public partial class LocalPlayerController : RemotePlayerController
    {
        public ClientPacketProcessor packetProcessor { get; set; }
        public NetPeer serverPeer;
        private PlayerMoveModel playerMoveModel;

        private long tmrDirection = 0;

        public override void _Ready()
        {
            base._Ready();

            playerMoveModel = new PlayerMoveModel();
            playerMoveModel.Position = playerDataModel.Position;
            playerMoveModel.Direction = playerDataModel.Direction;
        }

        public override void _PhysicsProcess(double delta)
        {

            base._PhysicsProcess(delta);

            SetInputRunning();

            SetInputDirection();
        }

        private void SetInputDirection()
        {
            var tickCount = System.Environment.TickCount64;

            if (tmrDirection < tickCount)
            {
                var direction = new Vector2(); //Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
                direction.X = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
                direction.Y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

                if (direction == Vector2.Zero)
                    return;
                if (isMoving)
                    return;

                if (direction.X > 0 && direction.Y < 0)
                    Direction = Direction.UpRight;
                else if (direction.X < 0 && direction.Y < 0)
                    Direction = Direction.UpLeft;
                else if (direction.X > 0 && direction.Y > 0)
                    Direction = Direction.DownRight;
                else if (direction.X < 0 && direction.Y > 0)
                    Direction = Direction.DownLeft;
                else if (direction.X > 0)
                    Direction = Direction.Right;
                else if (direction.X < 0)
                    Direction = Direction.Left;
                else if (direction.Y < 0)
                    Direction = Direction.Up;
                else if (direction.Y > 0)
                    Direction = Direction.Down;
                else
                    GD.PrintErr("Direction not found");

                PositionGrid.X += direction.X;
                PositionGrid.Y += direction.Y;

                offSetX = GridSize * -direction.X;
                offSetY = GridSize * -direction.Y;

                GD.Print($"PositionGrid: X: {PositionGrid.X} Y: {PositionGrid.Y}");

                isMoving = true;
                tmrDirection = tickCount + 25;
            }
        }

        private void SetInputRunning()
        {
            isRunning = Input.IsActionPressed("ui_running");

        }
    }
}
