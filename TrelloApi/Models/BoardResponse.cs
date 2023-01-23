using System.Text.Json.Serialization;

namespace TrelloApi.Models
{
    public record BoardResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("lists")]
        public List<BoardListResponse> Lists { get; set; }
    }
}
