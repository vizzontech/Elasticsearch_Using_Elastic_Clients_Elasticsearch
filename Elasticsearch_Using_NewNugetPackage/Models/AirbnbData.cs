namespace Elasticsearch_Using_Elastic.Clients.Elasticsearch.Models
{
    public class AirbnbData
    {
        public string id { get; set; }

        public string name { get; set; }

        public string host_id { get; set; }

        public string host_name { get; set; }

        public string neighbourhood_group { get; set; }

        public string neighbourhood { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }

        public string room_type { get; set; }

        public string price { get; set; }

        public string minimum_nights { get; set; }

        public string number_of_reviews { get; set; }

        public DateTime? last_review { get; set; }

        public string reviews_per_month { get; set; }

        public string calculated_host_listings_count { get; set; }

        public string availability_365 { get; set; }
    }

}
