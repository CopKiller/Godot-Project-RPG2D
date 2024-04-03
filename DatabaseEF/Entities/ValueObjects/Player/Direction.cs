using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Entities.ValueObjects.Player
{
    public class Direction : BaseEntity
    {
        public float X { get; set; }
        public float Y { get; set; }
    }
}
