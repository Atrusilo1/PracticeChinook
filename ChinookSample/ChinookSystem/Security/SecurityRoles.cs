﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.Security
{
    //changed from internal to public 
    public static class SecurityRoles
    {
        public const string WebsiteAdmins = "WebsiteAdmins";
        public const string RegisteredUser = "RegisteredUsers";
        public const string Staff = "Staff";
        public const string Auditor = "Auditor";


        //readonly
        internal static List<string> StartupSecurityRoles
        {
            get
            {
                List<string> roleList = new List<string>();
                roleList.Add(WebsiteAdmins);
                roleList.Add(RegisteredUser);
                roleList.Add(Staff);
                roleList.Add(Auditor);
                return roleList;

            }
        }

    }
}
