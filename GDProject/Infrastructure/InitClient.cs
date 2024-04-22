using GdProject.Logger;
using GdProject.Network;
using Godot;
using LiteNetLib;
using System;

namespace GdProject.Infrastructure
{
    internal class InitClient
    {
        public static ClientPlayer LocalPlayer { get; set; } = null;

        public NetworkManager _networkManager;

        public bool IsRunning { get; private set; } = false;

        public InitClient()
        {

        }

        public void Start()
        {
            if (IsRunning)
            {
                return;
            }

            _networkManager = new NetworkManager();

            _networkManager._clientNetwork.RemotePeerConnectedEvent += RemotePeerConnected;
            _networkManager._clientNetwork.RemotePeerDisconnectedEvent += RemotePeerDisconnected;
            _networkManager._clientNetwork.CurrentPeerConnectedEvent += LocalPlayerConnected;
            _networkManager._clientNetwork.CurrentPeerDisconnectedEvent += LocalPlayerDisconnected;
            _networkManager._clientNetwork.LatencyUpdatedEvent += LocalPlayerLatencyUpdated;

            _networkManager.Start();

            IsRunning = true;

        }

        public void Stop()
        {
            if (!IsRunning)
            {
                return;
            }

            _networkManager.Stop();

            IsRunning = false;
        }

        public void RemotePeerConnected(NetPeer netPeer)
        {
            //LocalPlayer = new ClientPlayer();
            LocalPlayer.RemotePeer = netPeer;
            LocalPlayer.OnDisconnect += LocalPlayerDisconnect;

            // log
            ExternalLogger.Print("Remote peer connected");
        }

        public void RemotePeerDisconnected(NetPeer netPeer, DisconnectInfo disconnectInfo)
        {
            LocalPlayer = null;
        }

        public void LocalPlayerConnected(NetPeer netPeer)
        {
            LocalPlayer = new ClientPlayer();
            LocalPlayer.CurrentPeer = netPeer;
            LocalPlayer.PacketProcessor = _networkManager._clientNetwork.NetPacketProcessor;
            LocalPlayer.OnDisconnect += LocalPlayerDisconnect;

            // log
            ExternalLogger.Print("Local player connected");
        }

        private void LocalPlayerDisconnect()
        {
            LocalPlayer = null;
        }

        public void LocalPlayerDisconnected(NetPeer netPeer)
        {
            LocalPlayer = null;
        }

        public void LocalPlayerLatencyUpdated(int latency)
        {
            LocalPlayer.Ping = latency;
        }

    }
}
