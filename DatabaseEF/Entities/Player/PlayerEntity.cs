using EntityFramework.Entities.Interface;
using EntityFramework.Entities.ValueObjects.Player;
using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Entities.Player;

public class PlayerEntity : BaseEntity, IPlayerEntity
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