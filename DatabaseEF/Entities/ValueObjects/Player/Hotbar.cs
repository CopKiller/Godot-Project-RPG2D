using EntityFramework.Entities.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Entities.ValueObjects.Player
{
    public class Hotbar : BaseEntity
    {
        public int SlotId { get; set; } = 0;
        public HotbarSlotType SlotType { get; set; } = HotbarSlotType.Clear;
    }
}
