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
            SceneManager.Initialize();

            // Inicia o gerenciador de clientes
            Logg.Logger.Log("Iniciando o ClientManager...");
            CallDeferred(nameof(AddClientManager));

            // Carrega a cena inicial do MainMenu
            Logg.Logger.Log("Carregando a cena MainMenu...");
            this.LoadScene("MainMenu");

        }

        private void AddClientManager()
        {
            var clientManager = new ClientManager();
            clientManager.Name = "ClientManager";
            GetTree().Root.AddChild(clientManager);
        }
    }
}
