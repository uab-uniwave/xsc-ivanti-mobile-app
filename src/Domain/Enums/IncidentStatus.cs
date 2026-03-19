using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public enum IncidentStatus
    {
        [Display(Name = "Open")]
        Open = 0,

        [Display(Name = "Logged")]
        Logged = 1,

        [Display(Name = "Active")]
        Active = 2,

        [Display(Name = "Waiting")]
        Waiting = 3,

        [Display(Name = "Resolved")]
        Resolved = 4,

        [Display(Name = "Closed")]
        Closed = 5
    }
}