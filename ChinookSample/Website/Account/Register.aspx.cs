using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.UI;
using Website;

#region Additonal NameSpace for security
using ChinookSystem.Security;   //This handles the AppicationUser, applicationDbContext, UserManager
#endregion

public partial class Account_Register : Page
{
    protected void CreateUser_Click(object sender, EventArgs e)
    {
        var manager = new UserManager();
        var user = new ApplicationUser() { UserName = UserName.Text };
        IdentityResult result = manager.Create(user, Password.Text);
        if (result.Succeeded)
        {
            //this method will add the new registered user
            //to the role of RegisteredUser
            //this will be recorded in the AspNetUserRoles
            manager.AddToRole(user.Id, SecurityRoles.RegisteredUser);

            IdentityHelper.SignIn(manager, user, isPersistent: false);
            IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
        }
        else
        {
            ErrorMessage.Text = result.Errors.FirstOrDefault();
        }
    }
}