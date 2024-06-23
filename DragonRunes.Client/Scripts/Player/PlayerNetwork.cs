


using DragonRunes.Network.CustomDataSerializable;
using DragonRunes.Scripts.Network;
using LiteNetLib;
using DragonRunes.Models.CustomData;

public partial class PlayerNetwork : PlayerData
{

    private ClientPacketProcessor packetProcessor;
    private NetPeer serverPeer;

    public void InitializePlayerNetwork()
    {
        var clientManager = NodeManager.GetNode<ClientManager>(nameof(ClientManager));
        packetProcessor = clientManager._networkService._clientPacketProcessor;
        serverPeer = clientManager._player.CurrentPeer;
        playerMoveModel = new PlayerMoveModel();
        playerMoveModel.Position = new Position();
        playerMoveModel.Direction = new Direction();

        this.OnPlayerMove += SendPlayerMove;
    }

    public void SendPlayerMove()
    {
        playerMoveModel.Direction.X = LastDirection.X;
        playerMoveModel.Direction.Y = LastDirection.Y;
        playerMoveModel.Position.X = Position.X;
        playerMoveModel.Position.Y = Position.Y;
        playerMoveModel.IsRunning = isRunning;

        packetProcessor.ClientPlayerMove(serverPeer, playerMoveModel);
    }

    public void ReceivePlayerMove(PlayerMoveModel playerMoveModel)
    {
        this.playerMoveModel = playerMoveModel;
        CallDeferred(nameof(UpdatePlayerMove));
    }

    private void UpdatePlayerMove()
    {
        Position = new Godot.Vector2(playerMoveModel.Position.X, playerMoveModel.Position.Y);
        Direction = new Godot.Vector2(playerMoveModel.Direction.X, playerMoveModel.Direction.Y);
        isRunning = playerMoveModel.IsRunning;
    }
}
