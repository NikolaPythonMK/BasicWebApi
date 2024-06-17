namespace BasicWebApi.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string CountryName { get; set; }
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
