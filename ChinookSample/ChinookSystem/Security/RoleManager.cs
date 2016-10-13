using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.AspNet.Identity;                    //RoleManager
using Microsoft.AspNet.Identity.EntityFramework;    //IdentityRole, RoleStore
#endregion

namespace ChinookSystem.Security
{
    public class RoleManager:RoleManager<IdentityRole>
    {
        public RoleManager():base(new RoleStore<IdentityRole>(new ApplicationDbContext()))
        {

        } 

        //this method will be executed when the appication starts up 
        //under IIS(internet iformation services)
        public void AddStartupRoles()
        {
            foreach (string roleName in SecurityRoles.StartupSecurityRoles)
            {
                //Check if the role already exist in the security tables 
                //located in the database
                if (!Roles.Any(r => r.Name.Equals(roleName)))
                {
                    //role is currently not on the database
                    this.Create(new IdentityRole(roleName));
                }
            }
        }
    }
}
