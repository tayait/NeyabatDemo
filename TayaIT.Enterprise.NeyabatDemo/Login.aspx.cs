using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using TayaIT.Enterprise.EMadbatah.Config;
using System.Web.Script.Serialization;
using TayaIT.Enterprise.Neyaba.DB;
using System.Runtime.InteropServices; // DllImport
using System.Security.Principal; // WindowsImpersonationContext
using System.Security.Permissions; // PermissionSetAttribute

namespace TayaIT.Enterprise.Neyabat.Web
{
    public partial class _Login : BasePage
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["userObj"] != null)
                {
                    Response.Redirect("Default.aspx");
                }
             
            }
           // this.Title = "وزارد الصحة - النيابات العامة";
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string pass = txtPass.Text;

            Neyaba.DB.User userObj = Neyaba.DB.UserHelper.Login(username, pass);
            if (userObj == null)
            {
                Session["userObj"] = null;
                lblInfo1.Text = "invalid username or password";
                lblInfo1.Visible = true;
            }
            else
            {
                Session["userObj"] = userObj;
                Response.Redirect("Default.aspx");
            }
        }
}
}
