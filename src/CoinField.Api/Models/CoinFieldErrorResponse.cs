using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinField.Api.Models
{
    public class CoinFieldErrorResponse
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("errors")]
        public IEnumerable<Error> Errors { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        public class Error
        {
            [JsonProperty("field")]
            public string Field { get; set; }

            [JsonProperty("location")]
            public string Location { get; set; }

            [JsonProperty("messages")]
            public string[] Messages { get; set; }

            [JsonProperty("types")]
            public string[] Types { get; set; }
        }
    }
}
