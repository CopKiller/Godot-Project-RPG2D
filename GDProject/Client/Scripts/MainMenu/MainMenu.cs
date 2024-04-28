using Godot;

public partial class MainMenu : Control
{

    public void InitMenu()
    {
        this.Show();

        // Traz o nó gerenciador de janelas
        var windows = NodeManager.GetNode<Windows>(nameof(Windows));

        // Fecha todas as janelas
        windows.CloseAll();

        // Abre a janela de login
        windows.Open(NodeManager.GetNode<Window>(nameof(MenuWindow)));
    }

    public void HideMenu()
    {
        // Traz o nó gerenciador de janelas
        var windows = NodeManager.GetNode<Windows>(nameof(Windows));

        // Fecha todas as janelas
        windows.CloseAll();

        this.Hide();
    }
}
