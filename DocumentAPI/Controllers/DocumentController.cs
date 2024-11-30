using Microsoft.AspNetCore.Mvc;
using DocumentDomain.Entities;
using MediatR;
using DocumentApplication.Commands;
using DocumentAPI.Filters;

namespace DocumentAPI.Controllers
{
    [ApiController]
    public class DocumentController(ISender mediator) : ControllerBase
    {
        [HttpPost]
        [Route("format-converter")]
        [AuthorizeFilter]
        public async Task<IActionResult> FormatConverter([FromBody] FormatRequest Xml)
        {
            var result = await mediator.Send(new processDocumentCommand(Xml));
            return Ok(result);
        }

    }
}
