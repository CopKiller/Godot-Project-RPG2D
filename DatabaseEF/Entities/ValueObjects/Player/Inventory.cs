using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Entities.ValueObjects.Player
{
    public class Inventory : BaseEntity
    {
        public int ItemId { get; set; } = 0;
        public int ItemAmount { get; set; } = 0;
        public byte Bounding { get; set; } = 0;
    }
}
