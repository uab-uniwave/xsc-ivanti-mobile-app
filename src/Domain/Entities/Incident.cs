using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Incident
    {
        public string RecId { get; set; } = string.Empty;
        public string? Category { get; set; }
        public string? Status { get; set; }
        public string? Subject { get; set; }
        public string? Resolution { get; set; }
        public string? Owner { get; set; }
        public string? OwnerEmail { get; set; }
        public string? Priority { get; set; }
        public string? Urgency { get; set; }
        public string? Service { get; set; }
        public DateTimeOffset? CreatedDateTime { get; set; }
        public DateTimeOffset? LastModDateTime { get; set; }
        public bool? IsVIP { get; set; }
        public bool? IsInFinalState { get; set; }

        // Links to other entities (navigation properties)
        public ICollection<FrsKnowledge> IncidentAssocFRS_Knowledge { get; set; }
        public ICollection<FrsDataEscalationWatch> IncidentAssociatedEscalationWatch { get; set; }
        public ICollection<Task> IncidentContainsTask { get; set; }
        public ICollection<Ci> IncidentAssociatesCI { get; set; }
        public ICollection<SoftwareAction> IncidentAssociatedSoftwareAction { get; set; }
        public ICollection<Attachment> IncidentContainsAttachment { get; set; }
        public ICollection<Change> IncidentAssociatesChange { get; set; }
        public ICollection<Problem> ProblemAssociatesIncident { get; set; }
        public ICollection<IncidentDetail> IncidentContainsIncidentDetail { get; set; }
        public ICollection<ServiceReq> IncidentAssociatedServiceReq { get; set; }
        public ICollection<ComputerProvisionAction> IncidentAssociatedComputerProvisionAction { get; set; }
        public ICollection<AuditIncident> AuditHistoryRelationship { get; set; }
        public ICollection<Employee> IncidentOwnerEmployee { get; set; }
        public ICollection<OrganizationalUnit> IncidentAssocOrganizationalUnit { get; set; }
        public ICollection<FrsMyItem> IncidentAssociatedMyItem { get; set; }
        public ICollection<Journal> IncidentContainsJournal { get; set; }

        public Incident()
        {
            IncidentAssocFRS_Knowledge = new List<FrsKnowledge>();
            IncidentAssociatedEscalationWatch = new List<FrsDataEscalationWatch>();
            IncidentContainsTask = new List<Task>();
            IncidentAssociatesCI = new List<Ci>();
            IncidentAssociatedSoftwareAction = new List<SoftwareAction>();
            IncidentContainsAttachment = new List<Attachment>();
            IncidentAssociatesChange = new List<Change>();
            ProblemAssociatesIncident = new List<Problem>();
            IncidentContainsIncidentDetail = new List<IncidentDetail>();
            IncidentAssociatedServiceReq = new List<ServiceReq>();
            IncidentAssociatedComputerProvisionAction = new List<ComputerProvisionAction>();
            AuditHistoryRelationship = new List<AuditIncident>();
            IncidentOwnerEmployee = new List<Employee>();
            IncidentAssocOrganizationalUnit = new List<OrganizationalUnit>();
            IncidentAssociatedMyItem = new List<FrsMyItem>();
            IncidentContainsJournal = new List<Journal>();
        }
    }
}
