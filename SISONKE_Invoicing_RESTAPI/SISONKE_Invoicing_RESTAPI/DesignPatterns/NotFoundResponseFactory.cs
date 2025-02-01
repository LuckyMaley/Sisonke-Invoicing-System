using Microsoft.AspNetCore.Mvc;

namespace SISONKE_Invoicing_RESTAPI.DesignPatterns
{
    public class NotFoundResponseFactory : ResponseFactory
    {
        public override IActionResult CreateResponse(object data)
        {
            return new NotFoundObjectResult(new { message = data });
        }
    }
}
