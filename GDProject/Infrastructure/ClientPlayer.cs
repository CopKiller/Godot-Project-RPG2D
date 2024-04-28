using GdProject.Model;
using GdProject.Network;
using LiteNetLib;
using System;

namespace GdProject.Infrastructure
{
    internal class ClientPlayer
    {
        public event Action OnDisconnect;

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
        public GameState GameState { get; set; }

        public PlayerDataModel _playerData { get; set; } = null;

        public PacketProcessor PacketProcessor { get; set; }

        public ClientPlayer()
        {

        }

        public void Disconnect()
        {
            CurrentPeer.Disconnect();
            OnDisconnect?.Invoke();
        }
    }
}
