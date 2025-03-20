using Newtonsoft.Json;

namespace Practice_6
{
    public class Model_events
    {
        public class Event
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("srcNumber")]
            public string SrcNumber { get; set; }

            [JsonProperty("dstNumber")]
            public string DstNumber { get; set; }

            [JsonProperty("time")]
            public DateTime Time { get; set; }

            [JsonProperty("duration")]
            public int? Duration { get; set; }

            [JsonProperty("srcLoc")]
            public List<double?> SrcLoc { get; set; }

            [JsonProperty("dstLoc")]
            public List<double?> DstLoc { get; set; }
        }

        public class Customer
        {
            [JsonProperty("phoneNumber")]
            public string PhoneNumber { get; set; }
        }
    }
}