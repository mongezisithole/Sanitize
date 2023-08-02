using Microsoft.AspNetCore.Mvc;
using Sanitize.Services.Interfaces;
using Sanitize.Shared;

namespace Sanitize.Api.Controllers
{
    /// <summary>
    /// A controller for sensitive words CRUD
    /// </summary>
    [Route("api/SensitiveWords")]
    [ApiController]
    public class SensitiveWordsController : ControllerBase
    {
        private readonly ISanitizeServices _sanitizeServices;

        public SensitiveWordsController(ISanitizeServices sanitizeServices)
        {
            _sanitizeServices = sanitizeServices;
        }

        /// <summary>
        /// Get all saved sensitive words from the database
        /// </summary>
        /// <returns></returns>
        /// <response code="200">A list of saved sensitive words</response>
        /// <response code="400">Something went wrong</response>
        [HttpGet(Name = "GetWords")]
        [ProducesResponseType(typeof(List<SensitiveWordDetails>), 200)]
        [ProducesResponseType(400)]
        public IResult Get()
        {
            try
            {
                var words = _sanitizeServices.GetSensitiveWords();

                return Results.Ok(words);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get a saved sensitive word from the database using the id provided
        /// </summary>
        /// <returns></returns>
        /// <response code="200">A saved sensitive word</response>
        /// <response code="400">Something went wrong</response>
        [HttpGet("{id:int}", Name = "GetWord")]
        [ProducesResponseType(typeof(SensitiveWordDetails), 200)]
        [ProducesResponseType(400)]
        public IResult Get(int id)
        {
            if (id == default)
            {
                return Results.BadRequest("Id is invalid");
            }

            try
            {
                var words = _sanitizeServices.GetSensitiveWordDetails(id);

                return Results.Ok(words);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a saved sensitive word from the database using the id provided
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Word removed message</response>
        /// <response code="400">Something went wrong</response>
        [HttpDelete("{id:int}", Name = "DeleteWord")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IResult Delete(int id)
        {
            if (id == default)
            {
                return Results.BadRequest("Id is invalid");
            }

            try
            {
                _sanitizeServices.RemoveSensitiveWord(id);

                return Results.Ok("Word removed");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Saved sensitive word to the database
        /// </summary>
        /// <returns></returns>
        /// <response code="200">A saved sensitive word</response>
        /// <response code="400">Something went wrong</response>
        [HttpPost(Name = "PostWord")]
        [ProducesResponseType(typeof(SensitiveWordDetails), 200)]
        [ProducesResponseType(400)]
        public IResult Post(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return Results.BadRequest("Word cannot be null");
            }

            try
            {
                var wordDetails = _sanitizeServices.AddOrUpdateSensitiveWord(new SensitiveWordDetails { Word = word });

                return Results.Ok(wordDetails);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates saved sensitive word from the database
        /// </summary>
        /// <returns></returns>
        /// <response code="200">A saved sensitive word</response>
        /// <response code="400">Something went wrong</response>
        [HttpPut(Name = "UpdateWord")]
        [ProducesResponseType(typeof(SensitiveWordDetails), 200)]
        [ProducesResponseType(400)]
        public IResult Update(SensitiveWordDetails updateWordDetails)
        {
            if (!updateWordDetails.Id.HasValue)
            {
                return Results.BadRequest("Id cannot be null");
            }

            try
            {
                var wordDetails = _sanitizeServices.AddOrUpdateSensitiveWord(updateWordDetails);

                return Results.Ok(wordDetails);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
