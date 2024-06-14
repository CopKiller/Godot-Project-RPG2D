using DragonRunes.Network;
using DragonRunes.Scripts.Network;
using Godot;

namespace DragonRunes.Client.Scripts
{
    public partial class ClientManager : Node
    {
        public readonly ClientPlayer _player;

        public readonly NetworkManager _clientManager;

        public readonly ClientNetworkService _networkService;

        public ClientManager()
        {
            _player = new ClientPlayer();
            _networkService = new ClientNetworkService();
            _clientManager = new NetworkManager(_networkService);
        }

        public override void _Ready()
        {
            _clientManager.Start();
        }
    }
}
