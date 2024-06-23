using DragonRunes.Models.CustomData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonRunes.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public Class Class { get; set; }
        public Position Position { get; set; } = new Position();
        public Direction Direction { get; set; } = new Direction();

        public PlayerModel()
        {
        }
    }
}
