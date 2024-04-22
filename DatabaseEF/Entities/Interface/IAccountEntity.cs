using EntityFramework.Entities.Player;

namespace EntityFramework.Entities.Interface
{
    public interface IAccountEntity : IEntity
    {
        string Login { get; set; }
        string Password { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime LastLoginDate { get; set; }
        PlayerEntity Player { get; set; }
    }
}
