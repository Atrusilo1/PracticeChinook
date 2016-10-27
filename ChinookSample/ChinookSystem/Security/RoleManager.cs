using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.AspNet.Identity;                    //RoleManager
using Microsoft.AspNet.Identity.EntityFramework;    //IdentityRole, RoleStore
using System.ComponentModel;                        //ODS
#endregion

namespace ChinookSystem.Security
{
    [DataObject] //You will need this for OBJECT DATA SOURCE
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
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<RoleProfile> ListAllRoles()
        {
            var um = new UserManager();
            //the data from roles needs to me in memory
            //for use by the query --> use .ToList
            var result = from role in Roles.ToList()
                         select new RoleProfile
                         {
                             RoleId = role.Id,      //This is coming from the security table
                             RoleName = role.Name,  //Coming from Securty tables
                             UserName = role.Users.Select(r => um.FindById(r.UserId).UserName)
                         };
            return result.ToList();
        }
        [DataObjectMethod(DataObjectMethodType.Insert,true)]
        public void AddRole(RoleProfile role)
        {
            //Any business roles to consider
            //the role should not already exist on the roles tables
            if (!this.RoleExists(role.RoleName))
            {
                this.Create(new IdentityRole(role.RoleName));
            }
        }
        [DataObjectMethod(DataObjectMethodType.Delete,true)]
        public void RemoveRole(RoleProfile role)
        {
            this.Delete(this.FindById(role.RoleId));
        }

        //this method will produce a list of all rolesNames
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<string> ListAllRoleNames()
        {
            return this.Roles.Select(r => r.Name).ToList();
        }
    }
}
