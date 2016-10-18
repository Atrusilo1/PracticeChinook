using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.Security
{
    public enum UnRegisteredUserType { Underfined, Employee, Customer }
    public class UnRegisteredUserProfile
    {
        public string UserId { get; set; } //generated 
        public string UserName { get; set; } //collected
        public string EmailAddress { get; set; } //collected 
        public string FirstName { get; set; } //Comes from User Table
        public string LastName { get; set; } //Comes from the User Table
        public UnRegisteredUserType UserType { get; set; }

    }
}
