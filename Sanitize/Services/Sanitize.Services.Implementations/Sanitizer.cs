using Sanitize.Core;
using Sanitize.Repository.Interfaces;
using Sanitize.Services.Interfaces;
using Sanitize.Shared.Extensions;

namespace Sanitize.Services.Implementations
{
    public class Sanitizer : ISanitizer
    {
        private readonly IRepository<SensitiveWord> _repository;

        public Sanitizer(IRepository<SensitiveWord> repository)
        {
            _repository = repository;
        }

        public string SanitizeWords(string inputWords)
        {
            var outputWords = inputWords;
            var str = inputWords.ToUpper();
            var savedWords = _repository.GetAll.ToList();
            var matchingWords = savedWords.Where(o => o.Word.ToUpper() == str
            || str.Split(" ").Contains(o.Word.ToUpper())).Select(o => o.Word.ToUpper()).ToList();

            if (matchingWords.Any()) 
            {
                foreach (var word in matchingWords) 
                {
                    outputWords = outputWords.Replace(word, word.Sanitize(), StringComparison.OrdinalIgnoreCase);
                }
            }

            return outputWords;
        }
    }
}
