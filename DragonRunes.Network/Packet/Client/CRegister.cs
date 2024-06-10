

namespace DragonRunes.Network.Packet.Client
{
    public class CRegister
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;

        
    }
}
