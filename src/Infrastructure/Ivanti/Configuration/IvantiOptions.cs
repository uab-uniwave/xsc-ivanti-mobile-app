using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Ivanti.Configuration
{
    public class IvantiOptions
    {
        public string BaseUrl { get; set; } = "https://stg-heat20254.synergy.lt/HEAT";
        public string Tenant { get; set; } = "stg-heat20254.synergy.lt";
        public string ApiKey { get; set; } = "9012070FAACF48DC90BD2BA337CCAA44";
        public string Cookie { get; set; } = "SID=stg-heat20254.synergy.lt#05UPILFVAKQ6JO1SLC5MP7UQCUGB5TFD#5";

        public string WorkSpaceObjectId{ get; set; } = "Incident#";
        public string WorkspaceLayout { get; set; } = "IncidentLayout.ResponsiveAnalyst";
        public string FormLayout { get; set; } = "IncidentLayout.ResponsiveAnalyst";
        public string FormView { get; set; } = "responsive.analyst.new";
        public int TzOffsetMinutes { get; set; }= -120;

    }
}
