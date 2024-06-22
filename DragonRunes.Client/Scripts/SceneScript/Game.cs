using DragonRunes.Client.Scripts;
using DragonRunes.Client.Scripts.SceneScript;
using DragonRunes.Logger;
using DragonRunes.Network.Packet.Server;
using Godot;
using Godot.NativeInterop;
using System.Collections.Generic;
using System.Linq;
using static Godot.Projection;

public partial class Game : Node2D
{
    public override void _Ready()
    {
        // Inicia os componentes da cena
        Logg.Logger.Log("Iniciando componentes da cena...");
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        this.Show();

        var players = NodeManager.GetNode<Players>(nameof(Players));

        AddChild(players);
    }

    //private void ProcessPlayerDataReceivedWhenLoadingScene() {
    //    var clientManager = NodeManager.GetNode<ClientManager>(nameof(ClientManager));
    //    var playerPeer = clientManager._player.CurrentPeer;
    //    var packetProcessor = clientManager._networkService._clientPacketProcessor;

    //    clientManager._player.GameState = GameState.InGame;

    //    Logg.Logger.Log($"{packetProcessor._packetQueue.Count} de pacotes do jogador");

    //    while (packetProcessor._packetQueue.Count > 0)
    //    {
    //        var packet = packetProcessor._packetQueue.Dequeue();

    //        if (packet is SPlayerData)
    //        {
    //            packetProcessor.ServerPlayerData(packet, playerPeer);
    //        }
    //    }
    //}
}
