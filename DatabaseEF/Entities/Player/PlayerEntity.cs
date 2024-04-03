using EntityFramework.Entities.Account;
using EntityFramework.Entities.ValueObjects.Player;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.Entities.Player;

public class PlayerEntity : BaseEntity
{
    public const byte MaxNameCaracteres = 20;

    [MaxLength(MaxNameCaracteres)]
    public string Name { get; set; } = string.Empty;
    public Position Position { get; set; } = new Position();
    public Direction Direction { get; set; } = new Direction();

    public PlayerEntity()
    {
        
    }
}