using System.Text.Json.Serialization;

namespace BasicWebApi.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string CountryName { get; set; }

        [JsonIgnore]
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
