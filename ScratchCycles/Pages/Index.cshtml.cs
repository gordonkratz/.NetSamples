using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScratchCycles.Models;
using ScratchCycles.Services;

namespace ScratchCycles.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private JsonFileEndpointsService _endpointsService;

        public IndexModel(ILogger<IndexModel> logger, JsonFileEndpointsService endpointsService)
        {
            _logger = logger;
            _endpointsService = endpointsService;
        }

        public IEnumerable<Endpoint> Endpoints { get; private set; }

        public void OnGet()
        {
            Endpoints = _endpointsService.GetEndpoints();
        }
    }
}
