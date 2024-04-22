

namespace Network.Packet
{
    public class SNewChar : IRecv
    {
        public void ReadPacket(int peerId)
        {
            var windowsNode = NodeManager.GetNode<Windows>("Windows");

            windowsNode.CallDeferred(nameof(windowsNode.CloseAllWindows));
            windowsNode.CallDeferred(nameof(windowsNode.AddActiveWindow), NodeManager.GetNode<CharacterWindow>("CharacterWindow"));
        }
    }
}
