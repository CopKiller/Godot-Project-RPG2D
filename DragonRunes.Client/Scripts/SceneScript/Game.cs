using DragonRunes.Logger;
using Godot;

public partial class Game : Node2D
{
    public override void _Ready()
    {
        // Adiciona o nó ao gerenciador de nós
        Logg.Logger.Log("Adicionando nó ao gerenciador de nós...");
        NodeManager.AddToNodeManager(this);

        // Inicia os componentes da cena
        Logg.Logger.Log("Iniciando componentes da cena...");
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        
    }
}
