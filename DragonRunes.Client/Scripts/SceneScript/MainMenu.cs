using DragonRunes.Logger;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class MainMenu : Control
{
    public override void _Ready()
    {

        // Inicia os componentes da cena
        Logg.Logger.Log("Iniciando componentes da cena...");
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        NodeManager.GetNode<Window>("winMenu").Show();

        //this.LoadScene("Game");
    }
}
