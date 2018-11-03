using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace CoinField.Api.Models
{
    public class DepthResponse : CoinFieldResponse
    {
        /// <summary>
        /// Market of the requested depth in the format of "basequote" e.g. btcxrp.
        /// </summary>
        [JsonProperty("market")]
        public string Market { get; set; }

        /// <summary>
        /// Array of objects containing bids.
        /// </summary>
        [JsonProperty("bids")]
        public IEnumerable<BidAsk> Bids { get; set; }

        /// <summary>
        /// Array of objects containing asks.
        /// </summary>
        [JsonProperty("asks")]
        public List<BidAsk> Asks { get; set; }

        [JsonConverter(typeof(BidAskConverter))]
        public class BidAsk
        {
            [JsonProperty("price")]
            public string Price { get; set; }

            [JsonProperty("volume")]
            public string Volume { get; set; }

            private class BidAskConverter : JsonConverter
            {
                public override bool CanConvert(Type objectType)
                {
                    throw new NotImplementedException();
                }

                public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
                {
                    JArray ja = JArray.Load(reader);

                    BidAsk data = new BidAsk
                    {
                        Price = (string)ja[0],
                        Volume = (string)ja[1]
                    };

                    return data;
                }

                public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
