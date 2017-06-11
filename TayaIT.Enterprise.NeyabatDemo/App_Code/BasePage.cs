using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TayaIT.Enterprise.EMadbatah.Config;
using System.Xml;
using System.Xml.XPath;
using System.Web.UI;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TayaIT.Enterprise.Neyaba.DB;

/// <summary>
/// Summary description for BasePage
/// </summary>
/// 
namespace TayaIT.Enterprise.Neyabat.Web
{
    public class BasePage : Page
    {
        public Preferences _preferences = null;

        public BasePage()
        {
        }

        public string CalculateSize(string y)
        {
            int size = Convert.ToInt32(y);
            double x = 0;
            if (size < 1024)
            {
                return size.ToString() + "Byte";
            }
            else
            {

                x = size / 1024;
                if (x < 1024)
                {
                    return Math.Round(x, 2).ToString() + "KB";
                }
                else
                {
                    x = x / 1024;
                    return Math.Round(x, 2).ToString() + "MB";
                }
            }

        }

        public string SessionFileID
        {
            get
            {
                string val = Context.Request.QueryString[Constants.QSKeyNames.SESSION_FILE_ID];
                if (!string.IsNullOrEmpty(val) && val.Trim() != string.Empty)
                {
                    return val;
                }
                else
                    return null;
            }
        }

        public string AjaxFunctionName
        {
            get
            {
                string val = Context.Request.QueryString[Constants.QSKeyNames.AJAX_FUNCTION_NAME];
                if (!string.IsNullOrEmpty(val) && val.Trim() != string.Empty)
                {
                    return val;
                }
                else
                    return null;
            }
        }

        public Label ErrorMessagePlace
        {
            get
            {
                return (Label)this.Master.FindControl("lblErrorMsg");
            }
        }

        public Label WarnMessagePlace
        {
            get
            {
                return (Label)this.Master.FindControl("lblWarnMsg");
            }
        }

        public Label InfoMessagePlace
        {
            get
            {
                return (Label)this.Master.FindControl("lblInfo");
            }
        }

        public User CurrentUser
        {
            get
            {
                return (User)Session["userObj"];
            }
            set
            {
                Session["userObj"] = value;
            }
        }

        public void HideMainPageContent()
        {
            ((ContentPlaceHolder)this.Master.FindControl("MainContent")).Visible = false;
        }

        public void ShowMainError(string errorMessage)
        {
            ErrorMessagePlace.Visible = true;
            ErrorMessagePlace.Text = errorMessage;
            HideMainPageContent();
        }

        public void ShowWarn(string warnMessage)
        {
            WarnMessagePlace.Visible = true;
            WarnMessagePlace.Text = warnMessage;
            //HideMainPageContent();
        }

        public void ShowInfo(string infoMsg)
        {
            InfoMessagePlace.Visible = true;
            InfoMessagePlace.Text = infoMsg;
            //HideMainPageContent();
        }

        /// <summary>
        /// Make sure the browser does not cache this page
        /// </summary>
        public void DisablePageCaching()
        {
            Response.Expires = 0;
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
        }

        protected override void InitializeCulture()
        {
            _preferences = new Preferences();

            if (Request.Cookies[AppConfig.GetInstance().PrefCookieName] == null)
            {
                _preferences.SavePreferencesToCookie();
            }
           
            DisablePageCaching();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }
    }
}
