namespace BasicWebApi.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public string ContactName { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
