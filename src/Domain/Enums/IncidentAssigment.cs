using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public  enum IncidentAssigment
    {
        Unassigned = 0, 
        MyTeam = 1,
        Mine = 2,
        Team = 3,
        Owner = 4
    }
}


/*0
: 
{Name: "Unassigned", IsDefault: true,…}
1
: 
{Name: "My Team", IsDefault: true,…}
2
: 
{Name: "Mine", IsDefault: true,…}
3
: 
{Name: "Team", IsDefault: true, SearchConditions: [], ControlType: "Pick List", FieldRef: "OwnerTeam",…}
4
: 
{Name: "Owner", IsDefault: false, SearchConditions: 
*/
