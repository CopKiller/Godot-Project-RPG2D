
using Godot;
using SharedLibrary.Extensions;
using System.Collections.Generic;
using System.Linq;

// Classe para gerenciar os nós do jogo
public partial class NodeManager : Node
{

    public override void _Ready()
    {
        var tree = GetTree().Root.GetNode<RPG2D>(nameof(RPG2D));

        AddToNodeManager(tree);
    }


    // Dicionário para mapear os nós por seus nomes
    private static DictionaryWrapper<string, Node> nodeMap = new DictionaryWrapper<string, Node>();

    // Método para adicionar um nó ao gerenciador
    public static void AddNode<T>(T node) where T : Node
    {
        if (nodeMap.ContainsKey(node.Name))
        {
            return;
        }

        nodeMap.AddItem(node.Name, node);

        GD.Print("Adicionado nó: " + node.Name + " do tipo " + node.GetType().Name);
    }
    // Método para obter um nó do gerenciador pelo nome
    public static T GetNode<T>(string name) where T : Node
    {
        //foreach (var item in nodeMap.GetItems())
        //{
        //    GD.Print("Nó: " + item.Key + " do tipo " + item.Value.GetType().Name);
        //}

        if (nodeMap.ContainsKey(name))
        {
            // Obtém o nó do mapa de nós
            Node node = nodeMap.GetItem(name);

            // Verifica se o nó é do tipo desejado
            if (node is T)
            {
                return (T)node;
            }
            else
            {
                GD.Print("O nó '" + name + "' não é do tipo esperado: " + typeof(T).Name);
                return null;
            }
        }
        else
        {
            GD.Print("O nó '" + name + "' não foi encontrado.");
            return null;
        }
    }
    // Método recursivo para adicionar um nó e seus filhos ao gerenciador
    public static void AddToNodeManager<T>(T node) where T : Node
    {
        // Adiciona o nó ao gerenciador
        AddNode(node);

        // Obtém os filhos do nó
        var childs = node.GetChildren(true);

        // Se o nó não tiver filhos, retorna
        if (childs.Count == 0)
        {
            return;
        }
        // Adiciona os filhos ao gerenciador
        foreach (var child in childs)
        {
            AddToNodeManager(child);
        }
    }

    public static List<T> GetNodes<T>() where T : Node
    {
        var nodes = nodeMap.GetItems().Values.Where(n => n is T);

        return nodes.Select(n => (T)n).ToList();
    }
}
