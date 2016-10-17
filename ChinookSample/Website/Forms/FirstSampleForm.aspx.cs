using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional NameSpaces
using ChinookSystem.Security;
#endregion

public partial class Forms_FirstSampleForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //are you logged in
            if(!Request.IsAuthenticated)
            {
                Response.Redirect("~/Accound/Login.aspx");
            }
            else
            {
                //are you allowed to be on this page
                if (!User.IsInRole(SecurityRoles.WebsiteAdmins))
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }
    }
}