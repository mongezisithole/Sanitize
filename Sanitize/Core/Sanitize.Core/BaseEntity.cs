using System.ComponentModel.DataAnnotations;

namespace Sanitize.Core
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
