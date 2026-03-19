namespace Domain.Entities
{
    public class SoftwareAction
    {
        public string RecId { get; set; } = string.Empty;
        public string? TypeOfRequest { get; set; }
        public string? Status { get; set; }
    }
}
