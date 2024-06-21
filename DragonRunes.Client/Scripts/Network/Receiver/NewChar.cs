using DragonRunes.Client.Scripts.SceneScript.MainMenu.Windows;
using DragonRunes.Network.Packet;
using DragonRunes.Network.Packet.Server;
using Godot;
using LiteNetLib;

namespace DragonRunes.Scripts.Network
{
    public partial class ClientPacketProcessor
    {
        public void NewChar(SNewChar obj, NetPeer netPeer)
        {

            var allWindows = NodeManager.GetNodes<WindowBase>();

            foreach (var item in allWindows)
            {
                item.CallDeferred("_Hide");

                if (item is winNewChar newCharWindow)
                {
                    newCharWindow.CallDeferred("_Show");
                }
            }
        }
    }
}
