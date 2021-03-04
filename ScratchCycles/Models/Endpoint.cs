using System.Text.Json.Serialization;

namespace ScratchCycles.Models
{
    public class Endpoint
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("gender")]
        public string Gender { get; set; }
        [JsonPropertyName("ip_address")]
        public string IPAddress { get; set; }

        public int[] Ratings { get; set; }

        public string Image =>
            "https://is2-ssl.mzstatic.com/image/thumb/Purple128/v4/2c/2e/77/2c2e7707-1585-be11-3f93-23c1a7d14258/source/256x256bb.jpg";

        public override string ToString()
        {
            return $"{nameof(ID)}: {ID}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Email)}: {Email}, {nameof(Gender)}: {Gender}, {nameof(IPAddress)}: {IPAddress}";
        }
    }
}
