using Microsoft.AspNetCore.Mvc;

namespace SISONKE_Invoicing_RESTAPI.DesignPatterns
{
    public class ErrorResponseFactory : ResponseFactory
    {
        public override IActionResult CreateResponse(object data)
        {
            return new BadRequestObjectResult(new { message = data });
        }
    }
}
