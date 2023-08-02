using Sanitize.Shared;

namespace Sanitize.Services.Interfaces
{
    public interface ISanitizeServices
    {
        SensitiveWordDetails AddOrUpdateSensitiveWord(SensitiveWordDetails sensitiveWordDetails);

        List<SensitiveWordDetails> GetSensitiveWords();

        SensitiveWordDetails GetSensitiveWordDetails(int id);

        void RemoveSensitiveWord(int id);
    }
}
