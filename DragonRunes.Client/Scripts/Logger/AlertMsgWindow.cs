using Godot;


namespace DragonRunes.Client.Scripts.Logger
{
    public partial class AlertMsgWindow : AcceptDialog
    {
        public AlertMsgWindow()
        {
            GetLabel().HorizontalAlignment = HorizontalAlignment.Center;
            this.Size = new Vector2I(300, 100);
            this.TransparentBg = true;
            this.Title = "Alert";
            this.AlwaysOnTop = true;
            this.DialogAutowrap = true;
        }

        public void SetText(string text)
        {
            GetLabel().Text = text;
            this.Show();
            this.PopupCentered();
        }
    }
}
