using DragonRunes.Models.CustomData;

namespace DragonRunes.Models
{
    public interface IAccountModel
    {
        string User { get; set; }
        string Password { get; set; }
        DateTime BirthDate { get; set; }
        string Email { get; set; }
        PlayerModel Player { get; set; }
    }
}
