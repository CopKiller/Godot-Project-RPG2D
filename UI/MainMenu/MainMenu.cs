using Godot;
using System;

public partial class MainMenu : Control
{
    // Variável para armazenar o grupo de botões
    private ButtonGroup buttonGroup;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Obtém uma referência ao grupo de botões
        buttonGroup = GetNode<ButtonGroup>("Buttons");

        buttonGroup = 

        // Conecta o sinal "button_pressed" do grupo de botões à função OnButtonPressed
        buttonGroup.Connect("button_pressed", new Callable(this, nameof(OnButtonPressed)), 0);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }

    // Chamado quando um botão é pressionado
    private void OnButtonPressed(int index)
    {
        // Obtém o botão pressionado pelo índice
        var button = buttonGroup.GetButtons()[index];

        // Obtém o nome do botão
        string buttonName = button.Name;

        // Imprime o nome do botão
        GD.Print("Botão pressionado: ", buttonName);

        // Se o nome do botão for "Login", você pode fazer algo como mudar de cena
        if (buttonName == "Login")
        {
            GetTree().ChangeSceneToFile("res://scenes/game.tscn");
        }
    }
}
