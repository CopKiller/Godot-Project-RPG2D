using Godot;

namespace Network.Packet
{
    public class SLeft : IRecv
    {
        public int Index { get; set; }

        public void ReadPacket(int peerId)
        {
            var playerNode = NodeManager.GetNode<PlayerController>(Index.ToString());

            if (playerNode == null) return;

            playerNode.CallDeferred(nameof(playerNode.RemovePlayer));
        }
    }
}
