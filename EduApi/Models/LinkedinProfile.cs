using Newtonsoft.Json;

namespace EduApi.Models
{
    public class LinkedinProfile
    {
        public string Id { get; set; }
        [JsonProperty("emailAddress")]
        public string Email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string pictureUrl { get; set; }
    }
}