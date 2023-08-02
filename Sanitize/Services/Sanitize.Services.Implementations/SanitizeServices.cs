using Sanitize.Core;
using Sanitize.Repository.Interfaces;
using Sanitize.Services.Interfaces;
using Sanitize.Shared;
using Sanitize.Shared.Extensions;

namespace Sanitize.Services.Implementations
{
    public class SanitizeServices : ISanitizeServices
    {
        private readonly IRepository<SensitiveWord> _repository;

        public SanitizeServices(IRepository<SensitiveWord> repository)
        {
            _repository = repository;
        }

        public SensitiveWordDetails AddOrUpdateSensitiveWord(SensitiveWordDetails sensitiveWordDetails)
        {
            SensitiveWord savedWord = null;

            if (sensitiveWordDetails.Id.HasValue)
            {
                savedWord = _repository.GetById(sensitiveWordDetails.Id.Value)!;
                
                if(savedWord != null) 
                {
                    var sameWord = _repository.GetAll.FirstOrDefault(o => o.Word.ToUpper() == sensitiveWordDetails.Word.ToUpper());

                    if(sameWord != null) return savedWord.ToSensitiveWordDetails();
                    else
                    {
                        savedWord.Word = sensitiveWordDetails.Word;

                        _repository.Update(savedWord);
                    }
                }
            }
            
            if( savedWord == null ) 
            {
                savedWord = new SensitiveWord { Word = sensitiveWordDetails.Word };

                _repository.Insert(savedWord);
            }

            return savedWord.ToSensitiveWordDetails();
        }

        public SensitiveWordDetails GetSensitiveWordDetails(int id)
        {
            var savedWord = _repository.GetById(id);

            if( savedWord != null ) return savedWord.ToSensitiveWordDetails();

            throw new ArgumentNullException(nameof(savedWord));
        }

        public List<SensitiveWordDetails> GetSensitiveWords()
        {
            return _repository.GetAll.OrderBy(o => o.Word).Select(o => o.ToSensitiveWordDetails()).ToList();
        }

        public void RemoveSensitiveWord(int id)
        {
            var savedWord = _repository.GetById(id);

            if (savedWord != null)
            {
                _repository.Delete(savedWord);
            }
            else
            {
                throw new ArgumentNullException(nameof(savedWord));
            }
        }
    }
}
