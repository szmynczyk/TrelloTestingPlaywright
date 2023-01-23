﻿using System.Text.Json.Serialization;

namespace TrelloApi.Models
{
    public record BoardList
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("closed")]
        public bool Closed { get; set; }
    }
}