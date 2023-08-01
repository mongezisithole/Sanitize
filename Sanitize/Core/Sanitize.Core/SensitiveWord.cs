using System.ComponentModel.DataAnnotations;

namespace Sanitize.Core
{
    public class SensitiveWord : BaseEntity
    {
        public string Word { get; set; } = string.Empty;
    }
}
