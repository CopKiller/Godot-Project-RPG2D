
using DragonRunes.Logger;
using DragonRunes.Network.CustomData;
using DragonRunes.Scripts.Network;
using DragonRunes.Network.CustomDataSerializable;
using LiteNetLib;
using System;

namespace DragonRunes.Client.Scripts
{
    public class ClientPlayer
    {
        //public event Action OnDisconnect;

        /// <summary>
        /// Get the current latency between server and client
        /// </summary>
        public int Ping { get; set; }
        /// <summary>
        /// The network server peer
        /// </summary>
        public NetPeer RemotePeer { get; set; } = null;
        /// <summary>
        /// The current peer
        /// </summary>
        public NetPeer CurrentPeer { get; set; } = null;
        /// <summary>
        /// Get the current game state
        /// </summary>
        public GameState GameState { get; set; } = GameState.Disconnect;
        /// <summary>
        ///  Player Data recebido pelo network
        /// </summary>
        public PlayerDataModel PlayerData { get; set; }

        public ClientPlayer()
        {

        }

        private void Disconnect()
        {
            CurrentPeer.Disconnect();
            //OnDisconnect?.Invoke();
        }

        public void OnLocalPeerConnected(NetPeer peer)
        {
            CurrentPeer = peer;
        }
        public void OnRemotePeerConnected(NetPeer peer)
        {
            GameState = GameState.InLogin;
            RemotePeer = peer;
            Logg.Logger.Log("Conectado ao servidor");
        }
        public void OnLocalPeerDisconnected(NetPeer peer)
        {
            GameState = GameState.Disconnect;
            CurrentPeer = null;
        }
        public void OnRemotePeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            Logg.Logger.Log($"Desconectado do servidor: {disconnectInfo.Reason}");

            GameState = GameState.Disconnect;
            RemotePeer = null;
        }
    }
}
