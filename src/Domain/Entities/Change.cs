namespace Domain.Entities
{
    public class Change
    {
        public string RecId { get; set; } = string.Empty;
        public string? Category { get; set; }
        public string? Status { get; set; }
    }
}
