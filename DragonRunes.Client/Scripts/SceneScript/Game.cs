﻿using DragonRunes.Logger;
using Godot;

public partial class Game : Node2D
{
    public override void _Ready()
    {
        // Inicia os componentes da cena
        Logg.Logger.Log("Iniciando componentes da cena...");
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        
    }
}
