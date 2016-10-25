using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Security Namespace
using ChinookSystem.Security;
#endregion
public partial class Admin_Security_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void RoleListView_ItemDeleted(object sender, ListViewDeletedEventArgs e)
    {
        DataBind();
    }

    protected void RoleListView_ItemInserted(object sender, ListViewInsertedEventArgs e)
    {
        DataBind();
    }

    protected void RefreshAll(object sender, EventArgs e)
    {
        DataBind();
    }

    protected void UnregisteredUsersGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //position the gridview to the slection index (row) that caused
        //the postback
        UnregisteredUsersGridView.SelectedIndex = e.NewSelectedIndex;

        //setup a variable that will be the physical pointer to the selected row
        GridViewRow agvrow = UnregisteredUsersGridView.SelectedRow; 

        //you can always check a pointer to see if something has been 
        //obtainted
        if (agvrow != null)
        {
            //access information contained in a text box on the gridveiw row
            //use the method .FindControl("control id name") as controltype
            //once you have a pointer to the control, you can acces the 
            //data content of the control using the control's acces mothod
            string assignedusername = "";
            TextBox inputControl = agvrow.FindControl("AssignedUserName") as TextBox;   
            if (inputControl != null)
            {
                assignedusername = inputControl.Text;
            }
            string assignedemail = (agvrow.FindControl("AssignedEmail") as TextBox).Text; //This is how to do it in one statment

            //create the UnregisteredUser instance
            //during creation I will pass to it the needed data 
            //to load the instance attributes

            //accessing boundfields on a gridview row uses .Cells[index].Text
            //index represents the column of the grid
            //columns are index (starting at 0)
            UnRegisteredUserProfile user = new UnRegisteredUserProfile()
            {
                UserId = int.Parse(UnregisteredUsersGridView.SelectedDataKey.Value.ToString()),
                UserType = (UnRegisteredUserType)Enum.Parse(typeof(UnRegisteredUserType), agvrow.Cells[1].Text),
                FirstName = agvrow.Cells[2].Text,
                LastName = agvrow.Cells[3].Text,
                UserName = assignedusername,
                EmailAddress = assignedemail
            };

            //register the user via the Chinook.UserManager controller
            UserManager sysmgr = new UserManager();
            sysmgr.RegisterUser(user);

            //assumed sccessful creation of a user
            //refresh the form
            DataBind();
        }
    }
}