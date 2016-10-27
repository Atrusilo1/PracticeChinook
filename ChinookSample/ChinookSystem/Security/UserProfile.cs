using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.Security
{
    public class UserProfile
    {
        public string UserId { get; set; }  //AspNetUser Table
        public string UserName { get; set; }    //AspNetUserTable 
        public int? EmployeeId { get; set; }    //AspNetUserTable
        public int? CustomerId { get; set; }    //AspNetUserTable
        public string FirstName { get; set; }   //Employee or Customer Tables
        public string LastName { get; set; }    //Employee or customer tables
        public string Email { get; set; }   //AspNetUser Table
        public bool EmailConfirmed { get; set; }  //AspNetUser Table
        public IEnumerable<string> RoleMemerships { get; set; }
    }
}
