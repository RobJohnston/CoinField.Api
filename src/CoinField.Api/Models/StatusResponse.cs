using Newtonsoft.Json;

namespace CoinField.Api.Models
{
    public class StatusResponse
    {
        /// <summary>
        /// Status of the system (ok, maintenance, down).
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
