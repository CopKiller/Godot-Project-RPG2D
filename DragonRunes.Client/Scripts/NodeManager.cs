using DragonRunes.Client.Scripts;
using DragonRunes.Logger;
using DragonRunes.Network;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

// Classe para gerenciar os nós do jogo
public class NodeManager
{
    private static DictionaryWrapper<string, Node> nodeMap = new DictionaryWrapper<string, Node>();

    private static List<Type> _exception = new List<Type>
    {
        typeof(SceneManager),
        typeof(ClientManager),
        typeof(Players)
    };

    // Método para adicionar um nó ao gerenciador
    public static void AddNode<T>(T node) where T : Node
    {
        if (nodeMap.ContainsKey<T>(node.Name))
        {
            Logg.Logger.Log("O nó '" + node.Name + "' já existe e não será adicionado novamente.");
            return;
        }

        nodeMap.AddItem(node.Name, node);
        Logg.Logger.Log("Adicionado nó: " + node.Name + " do tipo " + node.GetType().Name);
    }

    // Método para obter um nó do gerenciador pelo nome
    public static T GetNode<T>(string name) where T : Node
    {
        if (nodeMap.ContainsKey<T>(name))
        {
            return nodeMap.GetItem<T>(name);
        }
        else
        {
            Logg.Logger.Log("O nó '" + name + "' não é do tipo esperado: " + typeof(T).Name);
            return default;
        }
    }

    // Método recursivo para adicionar um nó e seus filhos ao gerenciador
    public static void AddToNodeManager<T>(T node) where T : Node
    {
        // Adiciona o nó ao gerenciador
        AddNode(node);

        // Obtém os filhos do nó
        var children = node.GetChildren();

        // Adiciona os filhos ao gerenciador
        foreach (var child in children)
        {
            AddToNodeManager(child);
        }
    }

    // Método para obter todos os nós de um tipo específico
    public static List<T> GetNodes<T>() where T : Node
    {
        var nodes = nodeMap.GetItems().Values.Where(n => n is T);
        return nodes.Select(n => (T)n).ToList();
    }

    // Método para remover um nó pelo tipo e nome
    public static void RemoveNode<T>(string name) where T : Node
    {
        if (nodeMap.ContainsKey<T>(name))
        {
            var node = nodeMap.GetItem<T>(name);

            nodeMap.RemoveItem(name);
            FreeNode(node);

            Logg.Logger.Log("Nó removido: " + name);
        }
        else
        {
            Logg.Logger.Log("O nó '" + name + "' não foi encontrado e não pode ser removido.");
        }
    }

    public static void RemoveNode<T>(T node) where T : Node
    {
        if (nodeMap.ContainsKey<T>(node.Name))
        {
            nodeMap.RemoveItem(node.Name);
            FreeNode(node);

            Logg.Logger.Log("Nó removido: " + node.Name);
        }
        else
        {
            Logg.Logger.Log("O nó '" + node.Name + "' não foi encontrado e não pode ser removido.");
        }
    }

    public static void Clear()
    {
        foreach (var node in nodeMap.GetItems().Values)
        {
            if (_exception.Contains(node.GetType()))
            {
                continue;
            }
            FreeNode(node);
            nodeMap.RemoveItem(node.Name);
        }
    }

    // Método para eliminar um nó da memória
    private static void FreeNode<T>(T node) where T : Node
    {
        node.QueueFree();
    }
}
