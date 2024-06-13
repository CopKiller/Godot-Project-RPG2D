using System;
using System.Collections.Generic;
using DragonRunes.Logger;
using Godot;


public static class SceneManager
{
    private static Dictionary<string, string> sceneMap = new Dictionary<string, string>();

    public static void Initialize()
    {
        // Adiciona as cenas ao gerenciador
        AddScene("MainMenu", "res://Scenes/MainMenu.tscn");

        AddScene("Game", "res://Scenes/Game.tscn");
        AddScene("Player", "res://Scenes/Player.tscn");
        AddScene("Dungeon", "res://Scenes/Maps/Dungeon.tscn");


        // Remove o gerenciador de cenas mas mantém seus métodos acessíveis de qualquer parte do código
        //this.QueueFree();
    }

    public static void AddScene(string name, string scene)
    {
        if (sceneMap.ContainsKey(name))
        {
            Logg.Logger.Log("A cena '" + name + "' já existe e não será adicionada novamente.");
            return;
        }

        sceneMap.Add(name, scene);
        //Logg.Logger.Log("Adicionada cena: " + name);
    }

    public static void LoadScene(this Node node, string name)
    {
        if (sceneMap.ContainsKey(name))
        {
            var scenePath = sceneMap[name];

            var scene = GD.Load<PackedScene>(scenePath);

            var pathRoot = node.GetTree().Root.GetChild<Node>(0);

            foreach (var item in pathRoot.GetChildren())
            {
                item.QueueFree();
            }

            var instance = scene.Instantiate();

            NodeManager.AddToNodeManager(instance);
            pathRoot.AddChild(instance);
        }
        else
        {
            Logg.Logger.Log("A cena '" + name + "' não foi encontrada.");
        }
    }
}

