
using DragonRunes.Scripts.Network;
using LiteNetLib;
using System;

namespace DragonRunes.Client.Scripts
{
    public class ClientPlayer
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
        public GameState GameState { get; set; } = GameState.Disconnect;

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
