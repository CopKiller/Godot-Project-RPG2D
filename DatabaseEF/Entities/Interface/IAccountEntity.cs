using EntityFramework.Entities.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
