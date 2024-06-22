using DragonRunes.Network;
using Godot;
using System.Globalization;
using System;
using System.Text;
using System.Linq;
using DragonRunes.Logger;
using DragonRunes.Client.Scripts;

public partial class winRegister : WindowBase
{
    public override void _Ready()
    {
        base._Ready();

        // Inicia os componentes da cena
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        AssignButtons();
        AssignTextFields();

    }
    private void AssignButtons()
    {
        NodeManager.GetNode<Button>("btnRGo").Pressed += () => GoRegister();
    }
    private void AssignTextFields()
    {
        NodeManager.GetNode<LineEdit>("txtRLogin").TextChanged += (text) => UserText(text);
        NodeManager.GetNode<LineEdit>("txtRPass").TextChanged += (text) => PassText(text);
        NodeManager.GetNode<LineEdit>("txtRPass2").TextChanged += (text) => RetypePassText(text);
        NodeManager.GetNode<LineEdit>("txtRMail").TextChanged += (text) => EmailText(text);
        NodeManager.GetNode<LineEdit>("txtRBirthday").TextChanged += (text) => BirthText(text);
    }

    private void GoRegister()
    {
        var loginField = NodeManager.GetNode<LineEdit>("txtRLogin").Text;
        var passField = NodeManager.GetNode<LineEdit>("txtRPass").Text;
        var pass2Field = NodeManager.GetNode<LineEdit>("txtRPass2").Text;
        var mailField = NodeManager.GetNode<LineEdit>("txtRMail").Text;
        var birthdayField = NodeManager.GetNode<LineEdit>("txtRBirthday").Text;

        if (loginField.IsValidName() && 
            passField.IsValidPassword() && 
            pass2Field.IsValidRetypePassword(passField) && 
            mailField.IsValidEmail() && 
            birthdayField.IsValidBirthDate())
        {
            var clientManager = NodeManager.GetNode<ClientManager>(nameof(ClientManager));

            var serverPeer = clientManager._player.CurrentPeer;

            var packetProcessor = clientManager._networkService._clientPacketProcessor;

            packetProcessor.ClientRegister(serverPeer, loginField, passField, mailField, birthdayField);
        }
    }

    private void UserText(string text)
    {
        if (text.IsValidName())
            NodeManager.GetNode<LineEdit>("txtRLogin").AddThemeColorOverride("font_color", new Color(0, 1, 0));
        else
            NodeManager.GetNode<LineEdit>("txtRLogin").AddThemeColorOverride("font_color", new Color(1, 0, 0));
    }
    private void PassText(string text)
    {
        if (text.IsValidPassword())
            NodeManager.GetNode<LineEdit>("txtRPass").AddThemeColorOverride("font_color", new Color(0, 1, 0));
        else
            NodeManager.GetNode<LineEdit>("txtRPass").AddThemeColorOverride("font_color", new Color(1, 0, 0));
    }
    private void RetypePassText(string text)
    {
        var pass = NodeManager.GetNode<LineEdit>("txtRPass").Text;

        if (text.IsValidRetypePassword(pass))
            NodeManager.GetNode<LineEdit>("txtRPass2").AddThemeColorOverride("font_color", new Color(0, 1, 0));
        else
            NodeManager.GetNode<LineEdit>("txtRPass2").AddThemeColorOverride("font_color", new Color(1, 0, 0));
    }
    private void EmailText(string text)
    {
        if (text.IsValidEmail())
            NodeManager.GetNode<LineEdit>("txtRMail").AddThemeColorOverride("font_color", new Color(0, 1, 0));
        else
            NodeManager.GetNode<LineEdit>("txtRMail").AddThemeColorOverride("font_color", new Color(1, 0, 0));
    }
    private void BirthText(string text)
    {
        if (text.IsValidBirthDate())
            NodeManager.GetNode<LineEdit>("txtRBirthday").AddThemeColorOverride("font_color", new Color(0, 1, 0));
        else
        {
            var formatted = new StringBuilder();
            var input = text;

            input = new string(input.Where(char.IsDigit).ToArray());
            if (input.Length < 2)
                formatted.Append(input);
            else if (input.Length < 4)
                formatted.Append(input.Substring(0, 2) + "/" + input.Substring(2, Math.Min(2, input.Length - 2)));
            else if (input.Length <= 8)
                formatted.Append(input.Substring(0, 2) + "/" + input.Substring(2, 2) + "/" + input.Substring(4, Math.Min(4, input.Length - 4)));

            var txtRBirthday = NodeManager.GetNode<LineEdit>("txtRBirthday");
            txtRBirthday.Text = formatted.ToString();
            txtRBirthday.CaretColumn = txtRBirthday.Text.Length;
            txtRBirthday.AddThemeColorOverride("font_color", new Color(1, 0, 0));
        }
    }
}
