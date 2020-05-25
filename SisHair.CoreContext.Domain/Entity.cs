using System.ComponentModel.DataAnnotations;

namespace SisHair.CoreContext
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
