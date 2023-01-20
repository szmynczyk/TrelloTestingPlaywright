using System.Text.Json.Serialization;

namespace TrelloApi.Models
{
    internal record BoardResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("lists")]
        public List<BoardList> Lists { get; set; }
    }
}
