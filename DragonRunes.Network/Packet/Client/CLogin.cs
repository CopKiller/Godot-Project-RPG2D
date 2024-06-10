
using LiteNetLib;

namespace DragonRunes.Network.Packet.Client
{
    public class CLogin
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
