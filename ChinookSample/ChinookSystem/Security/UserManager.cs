using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespace
using Microsoft.AspNet.Identity.EntityFramework; //UserStore
using Microsoft.AspNet.Identity;                //UserManager
using System.ComponentModel;                    //for ods
using ChinookSystem.DAL;                        //context class
#endregion

namespace ChinookSystem.Security
{
    [DataObject]
    public class UserManager : UserManager<ApplicationUser>
    {
        public UserManager()
            : base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
        {
        }
        //setting up the default WebMaster
        #region Constants
        private const string STR_DEFAULT_PASSWORD = "Pa$$word1";
        private const string STR_USERNAME_FORMAT = "{0}.{1}";
        private const string STR_EMAIL_FORMAT = "{0}@Chinook.ca";
        private const string STR_WEBMASTER_USERNAME = "Webmaster";
        #endregion

        public void AddWebmaster()
        {
            if (!Users.Any(u => u.UserName.Equals(STR_WEBMASTER_USERNAME)))
            {
                var webMasterAccount = new ApplicationUser()
                {
                    UserName = STR_WEBMASTER_USERNAME,
                    Email = string.Format(STR_EMAIL_FORMAT, STR_WEBMASTER_USERNAME)
                };
                //this create command is from the inherited UserManager class
                //this command creates a record on the security users table (AspNetUsers)
                this.Create(webMasterAccount, STR_DEFAULT_PASSWORD);

                //this AddToRole Command is from the inherited UserManager class
                //this command creates a record on the security UserRole table (AspNetUserRoles)
                this.AddToRole(webMasterAccount.Id, SecurityRoles.WebsiteAdmins);
            }
        }

        //Create the CRUD methods for adding a user to the security User Tables
        //read of data to display on gridview
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UnRegisteredUserProfile> ListAllUnregisteredUsers()
        {
            using (var context = new ChinookContext())
            {
                //the data needs to be in memory for execution by the next query
                //to accomplish this use .ToList() which will force the quey to execute
                //List() set containing the list of employeesId
                var registeredEmployees =( from emp in Users
                                          where emp.EmployeeID.HasValue
                                          select emp.EmployeeID).ToList();  //Added to list to the end

                //compare the IEnumerable set of the user data table Employees
                var unregisteredEmployes = (from emp in context.Employees
                                           where !registeredEmployees.Any(eid => emp.EmployeeId == eid)
                                           select new UnRegisteredUserProfile()
                                           {
                                               CustomerEmployeeId = emp.EmployeeId,
                                               FirstName = emp.FirstName,
                                               LastName = emp.LastName,
                                               UserType = UnRegisteredUserType.Employee
                                           }).ToList();

                //IEnumerable set containing the list of customerId
                var registeredCustomer = (from cus in Users
                                          where cus.CustomerID.HasValue
                                          select cus.CustomerID).ToList();

                //compare the IEnumerable set of the user data table Employees
                var unregisteredCustomer = (from cus in context.Customers
                                           where !registeredCustomer.Any(cid => cus.CustomerId == cid)
                                           select new UnRegisteredUserProfile()
                                           {
                                               CustomerEmployeeId = cus.CustomerId,
                                               FirstName = cus.FirstName,
                                               LastName = cus.LastName,
                                               UserType = UnRegisteredUserType.Employee
                                           }).ToList();
                return unregisteredEmployes.Union(unregisteredCustomer).ToList();

            }
        }

        //register a user to the user table (gridView)
        public void RegisterUser(UnRegisteredUserProfile userinfo)
        {
            //the basic infromation need for the security usr record
            //password, emailAddress and username
            //you could randomly generate a password, we will use the default password
            //the instance of the required user is based on our applicationUser
            var newuseraccount = new ApplicationUser()
            {
                UserName = userinfo.UserName,
                Email = userinfo.EmailAddress
            };

            //set the CustomerId of EmployeeId
            switch (userinfo.UserType)
            {
                case UnRegisteredUserType.Customer:
                    {
                        newuseraccount.Id = userinfo.UserId.ToString();
                        break;
                    }
                case UnRegisteredUserType.Employee:
                    {
                        newuseraccount.Id = userinfo.UserId.ToString();
                        break;
                    }
            }

            //Create the actual AspNetUser record
            this.Create(newuseraccount, STR_DEFAULT_PASSWORD);

            //assign user to appropriate role
            switch (userinfo.UserType)
            {
                case UnRegisteredUserType.Customer:
                    {
                        this.AddToRole(newuseraccount.Id, SecurityRoles.RegisteredUser);
                        break;
                    }
                case UnRegisteredUserType.Employee:
                    {
                        this.AddToRole(newuseraccount.Id, SecurityRoles.Staff);
                        break;
                    }
            }
        }

        //add a user to the user table (ListView)

        //delete a user from the user Tables (ListView)
    }
}
