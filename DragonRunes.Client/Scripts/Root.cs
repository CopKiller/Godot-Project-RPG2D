using DragonRunes.Logger;
using DragonRunes.Network;
using DragonRunes.Scripts.Network;
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
            AddSceneManager();

            // Inicia o gerenciador de clientes
            Logg.Logger.Log("Iniciando o ClientManager...");
            AddClientManager();

            // Carrega a cena inicial do MainMenu
            var sceneManager = NodeManager.GetNode<SceneManager>(nameof(SceneManager));
            sceneManager.LoadScene(nameof(MainMenu));

        }

        private void AddClientManager()
        {
            var clientManager = new ClientManager();
            clientManager.Name = nameof(ClientManager);
            AddChild(clientManager);
            NodeManager.AddNode(clientManager);
        }

        private void AddSceneManager()
        {
            var sceneManager = new SceneManager();
            sceneManager.Name = nameof(SceneManager);
            AddChild(sceneManager);
            NodeManager.AddNode(sceneManager);
            sceneManager.Initialize();
        }
    }
}
