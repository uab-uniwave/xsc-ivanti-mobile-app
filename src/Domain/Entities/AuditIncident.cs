using System;

namespace Domain.Entities
{
    public class AuditIncident
    {
        public string RecId { get; set; } = string.Empty;
        public DateTimeOffset AuditHistoryDateTime { get; set; }
        public byte AuditHistoryEventType { get; set; }
    }
}
