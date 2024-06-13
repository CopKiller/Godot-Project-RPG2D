using DragonRunes.Logger;
using Godot;

namespace DragonRunes.Client.Scripts
{
    public partial class Root : Node
    {

        public override void _Ready()
        {
            // Inicia o logger
            new LogManager();

            // Inicia o gerenciador de cenas
            Logg.Logger.Log("Iniciando o SceneManager...");
            SceneManager.Initialize();

            // Carrega a cena inicial do MainMenu
            Logg.Logger.Log("Carregando a cena MainMenu...");

            this.LoadScene("MainMenu");


            //// Obtém a raiz do nó "Root" e adiciona ao gerenciador
            //var tree = GetTree().Root.GetNode<Node>("Root");

            //AddToNodeManager(tree);

            //Logg.Logger.Log("Nós carregados: " + nodeMap.Count());

            //// Remove o gerenciador de nós mas mantém seus métodos acessíveis de qualquer parte do código
            //this.QueueFree();
        }
    }
}
