using Microsoft.AspNetCore.Mvc;

namespace SISONKE_Invoicing_RESTAPI.DesignPatterns
{
    public class SuccessResponseFactory : ResponseFactory
    {
        public override IActionResult CreateResponse(object data)
        {
            return new OkObjectResult(data);
        }
    }
}
