namespace Practice_6
{
    public class Model_events
    {
        public class Event
        {
            public string Type { get; set; }
            public string SrcNumber { get; set; }
            public string DstNumber { get; set; }
            public DateTime Time { get; set; }
            public int? Duration { get; set; }
        }

        public class Customer
        {
            public string PhoneNumber { get; set; }
        }
   

    }
}
