using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.Entities
{
    public class BaseEntity
    {
        [ForeignKey("Id")]
        public int Id { get; set; }
    }
}
