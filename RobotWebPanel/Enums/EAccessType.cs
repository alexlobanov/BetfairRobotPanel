using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RobotWebPanel.Enums
{
    public enum EaccessType
    {
        SuperAdminAccess = -1,
        AdminAccess = 0,
        ModeratorAccess = 1,
        UserAccess = 2,
        BannedUserAccess = 3
    }
}