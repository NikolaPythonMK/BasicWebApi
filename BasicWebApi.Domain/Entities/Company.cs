namespace BasicWebApi.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string CompanyName { get; set; }
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    }
}
