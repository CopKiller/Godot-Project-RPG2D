using Godot;
using System;

public partial class Alert : Node
{
    public override void _Ready()
    {
        var alertNode = GetNode<Window>("PanelBG/AlertMsg");
        alertNode.Connect("close_requested", new Callable(this, nameof(OnCloseButtonPressed)));
        alertNode.GetNode<Button>("VerticalBox/ButtonCancel").Connect("pressed", new Callable(this, nameof(OnOkButtonPressed)));
    }

    public void ShowAlert(string message)
    {
        GetNode<Panel>("PanelBG").Show();
        GetNode<Panel>("PanelBG").ZIndex = 20;

        var alertNode = GetNode<Window>("PanelBG/AlertMsg");
        var alertMsgLabel = alertNode.GetNode<Label>("VerticalBox/AlertMsgLabel");

        alertMsgLabel.Text = message;

        alertNode.PopupCentered();
        alertNode.Show();
    }

    public void OnCloseButtonPressed()
    {
        GetNode<Panel>("PanelBG").Hide();

        GetNode<Window>("PanelBG/AlertMsg").Hide();
    }

    public void OnOkButtonPressed()
    {
        GetNode<Panel>("PanelBG").Hide();

        GetNode<Window>("PanelBG/AlertMsg").Hide();
    }
}
