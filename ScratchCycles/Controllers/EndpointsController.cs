using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScratchCycles.Services;
using Endpoint = ScratchCycles.Models.Endpoint;

namespace ScratchCycles.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EndpointsController : ControllerBase
    {
        public JsonFileEndpointsService Service { get; }

        public EndpointsController(JsonFileEndpointsService service)
        {
            Service = service;
        }

        [HttpGet]
        public IEnumerable<Endpoint> Get()
        {
            return Service.GetEndpoints();
        }

        [Route("rate")]
        [HttpGet]
        public ActionResult Get([FromQuery] int productId,
            [FromQuery] int rating)
        {
            Service.AddRating(productId, rating);
            return Ok();
        }
    }
}
