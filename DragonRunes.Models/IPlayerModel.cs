using DragonRunes.Models.CustomData;

namespace DragonRunes.Models
{
    public interface IPlayerModel
    {
        string Name { get; set; }
        Gender Gender { get; set; }
        Class Class { get; set; }
        Position Position { get; set; }
        Direction Direction { get; set; }
    }
}
