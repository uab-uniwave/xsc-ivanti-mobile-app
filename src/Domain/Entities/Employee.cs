namespace Domain.Entities
{
    public class Employee
    {
        public string RecId { get; set; } = string.Empty;
        public string? DisplayName { get; set; }
        public string? PrimaryEmail { get; set; }
    }
}
