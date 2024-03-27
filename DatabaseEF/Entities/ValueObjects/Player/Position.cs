using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.Entities.ValueObjects.Player
{
    public class Position : BaseEntity
    {
        public int MapNum { get; set; } = 0;
        public int MapX { get; set; } = 0;
        public int MapY { get; set; } = 0;
    }
}
