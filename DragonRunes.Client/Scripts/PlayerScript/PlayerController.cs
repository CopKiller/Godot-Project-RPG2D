using DragonRunes.Client.Scripts.ControlsBase;
using DragonRunes.Logger;
using DragonRunes.Network.CustomDataSerializable;

namespace DragonRunes.Client.Scripts.PlayerScript
{
    public partial class PlayerController : PlayerBase
    {
        private PlayerDataModel playerDataModel;

        public override void _Ready()
        {
            if (playerDataModel == null)
            {
                Logg.Logger.Log("PlayerDataModel is null");
                return;
            }
            InitializePlayerBase();
        }

        public override void InitializePlayerBase()
        {
            base.InitializePlayerBase();

            base.PlayerName = playerDataModel.Name;
            base.Position = playerDataModel.Position;
            base.LastDirection = playerDataModel.Direction;
        }
    }
}
