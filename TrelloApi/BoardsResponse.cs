using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TrelloApi
{
    internal class BoardsResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
