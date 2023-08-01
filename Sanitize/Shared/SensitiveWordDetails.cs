namespace Sanitize.Shared
{
    public class SensitiveWordDetails
    {
        /// <summary>
        /// id
        /// </summary>
        /// <example>55</example>
        public int? Id { get; set; }

        /// <summary>
        /// sensitive word
        /// </summary>
        /// <example>WRITE</example>
        public string Word { get; set; } = string.Empty;
    }
}
