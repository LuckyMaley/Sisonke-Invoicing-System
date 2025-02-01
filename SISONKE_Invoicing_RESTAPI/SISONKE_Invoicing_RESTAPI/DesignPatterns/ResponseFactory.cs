using Microsoft.AspNetCore.Mvc;

namespace SISONKE_Invoicing_RESTAPI.DesignPatterns
{
    public abstract class ResponseFactory
    {
        public abstract IActionResult CreateResponse(object data);
    }
}
