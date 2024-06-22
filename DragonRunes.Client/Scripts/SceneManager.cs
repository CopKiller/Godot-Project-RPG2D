using System;
using System.Collections.Generic;
using DragonRunes.Client.Scripts.SceneScript;
using DragonRunes.Logger;
using Godot;

public partial class SceneManager : Node
{
    private Dictionary<string, string> sceneMap = new Dictionary<string, string>();

    private Node _currentScene;

    public override void _Ready()
    {
        Initialize();
    }

    public void Initialize()
    {
        // Adiciona as cenas ao gerenciador
        AddScene("MainMenu", "res://Scenes/MainMenu.tscn");

        AddScene("Game", "res://Scenes/Game.tscn");
        AddScene("Players", "res://Scenes/Players.tscn");
        AddScene("Player", "res://Scenes/Player.tscn");
        AddScene("Dungeon", "res://Scenes/Maps/Dungeon.tscn");


        // Remove o gerenciador de cenas mas mantém seus métodos acessíveis de qualquer parte do código
        //this.QueueFree();
    }

    public void AddScene(string name, string scene)
    {
        if (sceneMap.ContainsKey(name))
        {
            Logg.Logger.Log("A cena '" + name + "' já existe e não será adicionada novamente.");
            return;
        }

        sceneMap.Add(name, scene);
        //Logg.Logger.Log("Adicionada cena: " + name);
    }

    public void LoadScene(string name)
    {
        if (sceneMap.ContainsKey(name))
        {
            var scenePath = sceneMap[name];

            UnloadScene();

            var scene = GD.Load<PackedScene>(scenePath);

            _currentScene = scene.Instantiate();

            NodeManager.AddToNodeManager(_currentScene);

            AddChild(_currentScene);
        }
        else
        {
            Logg.Logger.Log("A cena '" + name + "' não foi encontrada.");
        }
    }

    private void UnloadScene()
    {
        if (_currentScene != null)
        {
            _currentScene.QueueFree();
            NodeManager.Clear();
        }
    }

    public PackedScene GetScene(string name)
    {
        if (sceneMap.ContainsKey(name))
        {
            var scenePath = sceneMap[name];

            var scene = GD.Load<PackedScene>(scenePath);

            return scene;
        }
        else
        {
            Logg.Logger.Log("A cena '" + name + "' não foi encontrada.");
            return null;
        }
    }

    //private void AddChild<T>(T node) where T : Node
    //{
    //    AddChild(node);
    //}
}

