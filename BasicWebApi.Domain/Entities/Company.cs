using System.Text.Json.Serialization;

namespace BasicWebApi.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string CompanyName { get; set; }

        [JsonIgnore]
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    }
}
