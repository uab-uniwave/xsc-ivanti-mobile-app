using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Ivanti.Configuration
{
    public class IvantiOptions
    {
        public string BaseUrl { get; set; }  = string.Empty;  
        public string Tenant { get; set; } = string.Empty;
        public string ApiKey { get; set; } = "rest_api_key = 9012070FAACF48DC90BD2BA337CCAA44";
        public string Cookie { get; set; } = "stg-heat20254.synergy.lt#TIGEC426VRDDJGB57PI83OB4TC540PBL#3";

        public string WorkSpaceObjectId{ get; set; } = "Incident#";
        public string WorkspaceLayout { get; set; } = "IncidentLayout.ResponsiveAnalyst";
        public string FormLayout { get; set; } = "IncidentLayout.ResponsiveAnalyst";
        public string FormView { get; set; } = "responsive.analyst.new";
        public int TzOffsetMinutes { get; set; }= -120;

    }
}
