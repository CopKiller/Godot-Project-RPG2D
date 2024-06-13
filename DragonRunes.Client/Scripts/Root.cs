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

            // Carrega a cena inicial do MainMenu
            Logg.Logger.Log("Carregando a cena MainMenu...");
            this.LoadScene("MainMenu");

            // Realiza a conexão com o servidor
            var netManager = new NetworkManager(new ClientNetworkService());
            netManager.Start();
        }
    }
}
