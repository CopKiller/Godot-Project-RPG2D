namespace EntityFramework.Entities.Account;

using Player;
using System.ComponentModel.DataAnnotations;

public class AccountEntity : BaseEntity
{
    public const byte MaxChar = 3;
    public const byte MaxAccountCaracteres = 20;
    public const byte MaxEmailCaracteres = Byte.MaxValue;
    public const byte MaxCriptographyCaracteres = 50;

    [MaxLength(MaxAccountCaracteres)] // Defina o tamanho máximo desejado aqui
    public string Login { get; set; } = string.Empty;

    [MaxLength(MaxCriptographyCaracteres)] // Defina o tamanho máximo desejado aqui
    public string Password { get; set; } = string.Empty;

    [MaxLength(MaxCriptographyCaracteres)] // Defina o tamanho máximo desejado aqui
    public string Salt { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime LastLoginDate { get; set; }
    public PlayerEntity Player { get; set; } = new PlayerEntity();

    public AccountEntity()
    {   
    
    }
}