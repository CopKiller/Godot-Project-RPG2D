namespace Network.Packet
{
    public class SLeft : IRecv
    {
        public int Index { get; set; }

        public void ReadPacket(int peerId)
        {
            var playerNode = NodeManager.GetNode<Player>(Index.ToString());

            if (playerNode == null) return;

            playerNode.QueueFree();
        }
    }
}
