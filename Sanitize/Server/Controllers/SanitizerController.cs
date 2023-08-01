using Microsoft.AspNetCore.Mvc;
using Sanitize.Services.Interfaces;

namespace Sanitize.Server.Controllers
{
    /// <summary>
    /// A controller for sanitizing input string from the user
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SanitizerController : ControllerBase
    {
        private readonly ISanitizer _sanitizer;

        public SanitizerController(ISanitizer sanitizer)
        {
            _sanitizer = sanitizer;
        }

        /// <summary>
        /// Sanitizing input string from the user
        /// </summary>
        /// <returns></returns>
        /// <response code="200">A sanitized string with sensitive words hidden</response>
        /// <response code="400">Something went wrong</response>
        [HttpGet]
        public IResult Get(string inputString)
        {
            try
            {
                var sanitizedStr = _sanitizer.SanitizeWords(inputString);

                return Results.Ok(sanitizedStr);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
