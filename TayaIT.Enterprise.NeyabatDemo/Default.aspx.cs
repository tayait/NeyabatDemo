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
    public partial class _Default : BasePage
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gvbind();
            }
           // this.Title = "وزارد العدل - النيابات العامة";
        }

      
        protected void btnAddEditDefAtt_Click(object sender, EventArgs e)
        {
            try
            {

                string filename = "unknown.jpg";
                if (fuAttAvatar.HasFile)
                {
                    string[] temp = fuAttAvatar.FileName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                    string tempstr = DateTime.Now.ToShortTimeString().ToLower().Replace("/", "").Replace(" ", "").Replace(":", "").Replace("am", "").Replace("pm", "");
                    filename = temp[0] + "_" + tempstr + "." + temp[1];
                    filename = filename.Replace("/", "_").Replace(" ", "_").Replace(":", "_");
                    filename = filename.Replace(" ", "").ToLower();

                    string audioPath = AppConfig.GetInstance().AudioServerPath;
                    fuAttAvatar.SaveAs(audioPath + filename);
                    fuAttAvatar.SaveAs(Server.MapPath("~") + "\\SessionFiles\\" + filename);

                    /*  AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
                      IntPtr admin_token = default(IntPtr);
                      System.Security.Principal.WindowsIdentity wid_current = System.Security.Principal.WindowsIdentity.GetCurrent();
                      System.Security.Principal.WindowsIdentity wid_admin = null;
                      System.Security.Principal.WindowsImpersonationContext wic = null;
                      LogonUtility.ImpersonateUser("Develop\\root", "TayaDemo");*/
                   // File.Copy(Server.MapPath("~") + "\\SessionFiles\\" + filename, audioPath + filename);
                    long size = fuAttAvatar.FileContent.Length;
                    AudioFile file = new AudioFile();
                    file.Name = filename;
                    file.FileSize = size;
                    file.CreatedAt = DateTime.Now;
                    file.Status = 1;
                    AudioFileHelper.AddNewFile(file);

                    lblInfo1.Text = "تم الحفظ بنجاح";
                    lblInfo1.Visible = true;

                    gvbind();
                    Response.Redirect("Default.aspx");

                }
            }
            catch (Exception ex)
            {
                lblInfo1.Text = ex.Message;
                lblInfo1.Visible = true;
                //LogHelper.LogException(ex, "add new img");
            }
        }

        protected void gvbind()
        {
            List<AudioFile> AudioFileLst = AudioFileHelper.GetAudioFiles();
            gvAudioFiles.DataSource = AudioFileLst;
            gvAudioFiles.DataBind();
        }

        public string GetFileStatusString(string filename)
        {
            string fileStatusStr = "";
            string xmlFilePath = string.Format("{0}{1}", AppConfig.GetInstance().VecSysServerPath, filename.ToLower().Replace("mp3", "trans.xml"));
            if (File.Exists(xmlFilePath))
            {
                FileInfo fileInfo = new FileInfo(xmlFilePath);
                if (fileInfo.Length > 0)
                    fileStatusStr = "تم";
                else
                {
                    fileStatusStr = "قيد التحويل";
                    if (Directory.Exists("\\\\192.168.0.60\\tmp"))
                    {
                        // This path is a directory
                        fileStatusStr += "<br>" + ProcessDirectory("\\\\192.168.0.60\\tmp", filename);
                    }
                   
                }

            }
            else fileStatusStr = "لم يتم البدء فى الملف";

            return fileStatusStr;
        }

        public static string ProcessDirectory(string targetDirectory,string fileName)
        {
            string txt = "";
            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                if (subdirectory.IndexOf(fileName.Replace(".mp3","")) > 0)
                {
                    string fileprocesspath = subdirectory + "\\input.idp";
                    if(File.Exists(fileprocesspath))
                    {
                        txt = System.IO.File.ReadAllText(fileprocesspath);
                        txt = txt.Replace("vrbs_trans:", "").Replace("\\n","");
                        txt = "(" + txt + "%" + ")";
                    }
                }
            }

            return txt;
        }

        protected void DeleteAudioFile(object sender, EventArgs e)
        {
            LinkButton lnkRemove = (LinkButton)sender;
            long audioFileID = long.Parse(lnkRemove.CommandArgument);//Convert.ToInt64(gvProcedures.DataKeys[e.RowIndex].Value.ToString());
            AudioFileHelper.DeleteAudioFileById(audioFileID);
            gvbind();

        }
    }
}
