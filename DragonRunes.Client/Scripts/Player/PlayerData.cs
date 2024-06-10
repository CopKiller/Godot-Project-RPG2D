using GdProject.Model;
using Godot;


namespace GdProject.Client.Scripts.Entities.Player
{
    public partial class PlayerData : PlayerPhysic
    {
        public void InitializePlayerData()
        {
            GetNode<RichTextLabel>("PlayerName").Text = playerDataModel.Name;
            GetNode<ColorRect>("ColorRect").Size = GetNode<RichTextLabel>("PlayerName").GetMinimumSize() + new Vector2(6, 1);
            Vector2 position = GetNode<RichTextLabel>("PlayerName").Position;
            position.X = -GetNode<ColorRect>("ColorRect").Size.X / 2;
            GetNode<ColorRect>("ColorRect").Position = position;
        }
    }
}
