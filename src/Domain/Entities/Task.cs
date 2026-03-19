namespace Domain.Entities
{
    public class Task
    {
        public string RecId { get; set; } = string.Empty;
        public string? Subject { get; set; }
        public string? Status { get; set; }
    }
}
