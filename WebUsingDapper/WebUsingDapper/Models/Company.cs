namespace WebUsingDapper.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string? CompanyDescription { get; set;}
        public string CompanyPhone { get; set;}=string.Empty;
        public string CompanyLocation { get; set;} = string.Empty;
    }
}
