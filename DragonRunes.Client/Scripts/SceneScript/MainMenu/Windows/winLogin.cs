using DragonRunes.Scripts.Network;
using DragonRunes.Shared;
using Godot;

public partial class winLogin : WindowBase
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
        NodeManager.GetNode<Button>("btnGo").Pressed += () => GoLogin();
    }
    private void AssignTextFields()
    {
        NodeManager.GetNode<LineEdit>("txtLogin").TextChanged += (text) => UserText(text);
        NodeManager.GetNode<LineEdit>("txtPass").TextChanged += (text) => PassText(text);
    }

    private void GoLogin()
    {
        
    }

    private void UserText(string text)
    {
        if (text.IsValidName())
            NodeManager.GetNode<LineEdit>("txtLogin").AddThemeColorOverride("font_color", new Color(0, 1, 0));
        else
            NodeManager.GetNode<LineEdit>("txtLogin").AddThemeColorOverride("font_color", new Color(1, 0, 0));
    }
    private void PassText(string text)
    {
        if (text.IsValidPassword())
            NodeManager.GetNode<LineEdit>("txtPass").AddThemeColorOverride("font_color", new Color(0, 1, 0));
        else
            NodeManager.GetNode<LineEdit>("txtPass").AddThemeColorOverride("font_color", new Color(1, 0, 0));
    }
}
