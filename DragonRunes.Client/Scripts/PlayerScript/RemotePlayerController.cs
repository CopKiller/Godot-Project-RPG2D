using DragonRunes.Client.Scripts.ControlsBase;
using DragonRunes.Logger;
using DragonRunes.Models.CustomData;
using DragonRunes.Network.CustomDataSerializable;
using DragonRunes.Scripts.Network;
using Godot;
using LiteNetLib;

namespace DragonRunes.Client.Scripts.PlayerScript
{
    public partial class RemotePlayerController : PlayerBase
    {
        public PlayerDataModel playerDataModel;

        public override void _Ready()
        {
            if (playerDataModel == null)
            {
                GD.PrintErr("playerDataModel is null");
                playerDataModel = new PlayerDataModel();
                playerDataModel.Name = "Player";
                playerDataModel.Position = new Position();
                playerDataModel.Position.X = 18.0f;
                playerDataModel.Position.Y = 5.0f;
            }

            InitializePlayer();
        }

        public override void InitializePlayer()
        {
            base.InitializePlayer();

            base.PlayerName = playerDataModel.Name;

            UpdatePlayerPosition();

            AddPlayerNameBackground();
        }

        private void AddPlayerNameBackground()
        {
            GetNode<RichTextLabel>("PlayerName").Text = playerDataModel.Name;
            GetNode<ColorRect>("ColorRect").Size = GetNode<RichTextLabel>("PlayerName").GetMinimumSize() + new Godot.Vector2(6, 0);
            Godot.Vector2 position = GetNode<RichTextLabel>("PlayerName").Position;
            position.X = -GetNode<ColorRect>("ColorRect").Size.X / 2;
            GetNode<ColorRect>("ColorRect").Position = position;
        }

        public void ReceivePlayerMove(PlayerMoveModel playerMoveModel)
        {
            Position = playerMoveModel.Position;
            //Direction = playerMoveModel.Direction;
            //playerDataModel.Position = playerMoveModel.Position;
            //playerDataModel.Direction = playerMoveModel.Direction;

            isRunning = playerMoveModel.IsRunning;
        }

        private void UpdatePlayerPosition()
        {
            Position = playerDataModel.Position;

            //Direction = playerDataModel.Direction;
        }
    }
}
