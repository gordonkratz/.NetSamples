using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using ScratchCycles.Models;

namespace ScratchCycles.Services
{
    public class JsonFileEndpointsService
    {
        public JsonFileEndpointsService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName => 
            Path.Combine(WebHostEnvironment.WebRootPath, "data", "endpoints.json");

        public IEnumerable<Endpoint> GetEndpoints()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            return JsonSerializer.Deserialize<Endpoint[]>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public void AddRating(int productId, int rating)
        {
            var points = GetEndpoints();
            var query = points.First(x => x.ID == productId);
            if (query.Ratings == null)
            {
                query.Ratings = new[] {rating};
            }
            else
            {
                var ratings = query.Ratings.ToList();
                ratings.Add(rating);
                query.Ratings = ratings.ToArray();
            }

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize(new Utf8JsonWriter(outputStream, new()
                {
                    SkipValidation = true,
                    Indented = true
                }), points);
            }
        }
    }
}
