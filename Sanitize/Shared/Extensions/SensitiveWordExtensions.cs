using Sanitize.Core;

namespace Sanitize.Shared.Extensions
{
    public static class SensitiveWordExtensions
    {
        public static SensitiveWordDetails ToSensitiveWordDetails(this SensitiveWord sensitiveWord) 
        {
            return new SensitiveWordDetails
            {
                Id = sensitiveWord.Id,
                Word = sensitiveWord.Word
            };
        }
    }
}
