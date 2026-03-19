using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Incident
{
    public sealed class IncidentDetailsDto
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Impact { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string? OwnerName { get; set; }
    }
}
