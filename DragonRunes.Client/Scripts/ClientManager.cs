using DragonRunes.Client.Scripts.Logger;
using DragonRunes.Network;
using DragonRunes.Scripts.Network;
using Godot;

namespace DragonRunes.Client.Scripts
{
    public partial class ClientManager : Node
    {
        public readonly ClientPlayer _player;

        public readonly ClientNetworkService _networkService;

        public readonly NetworkManager _networkManager;

        public ClientManager()
        {
            _player = new ClientPlayer();
            _networkService = new ClientNetworkService();
            _networkManager = new NetworkManager(_networkService);

            AssignNetworkEventsToPlayer();
        }
        
        private void AssignNetworkEventsToPlayer()
        {
            _networkService.CurrentPeerConnectedEvent += _player.OnLocalPeerConnected;
            _networkService.CurrentPeerDisconnectedEvent += _player.OnLocalPeerDisconnected;
            _networkService.RemotePeerConnectedEvent += _player.OnRemotePeerConnected;
            _networkService.RemotePeerDisconnectedEvent += _player.OnRemotePeerDisconnected;
        }

        public override void _Ready()
        {
            _networkManager.Start();
        }

        public void AlertMsg(string text)
        {
            var alertmsgWindow = new winAlertMsg();
            NodeManager.GetNode<Node>("MainMenuWindows").AddChild(alertmsgWindow);
            alertmsgWindow.SetText(text);
        }
    }
}
