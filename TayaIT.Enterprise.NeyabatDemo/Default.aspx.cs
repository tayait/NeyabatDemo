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
using System.Diagnostics;
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
                    if (fuAttAvatar.PostedFile.ContentType.ToLower().IndexOf("mp4") != -1)
                    {
                        string videoPath = Server.MapPath("~") + "\\SessionFiles\\" + filename;
                        fuAttAvatar.SaveAs(videoPath);

                        Process _process = new Process();
                        /* _process.StartInfo.RedirectStandardInput = true;
                         _process.StartInfo.RedirectStandardOutput = true;
                         _process.StartInfo.RedirectStandardError = true;*/
                        _process.StartInfo.UseShellExecute = false;
                        _process.StartInfo.CreateNoWindow = true;
                        _process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        _process.StartInfo.RedirectStandardOutput = false;
                        _process.StartInfo.FileName = "e:\\ffmpeg\\bin\\ffmpeg.exe";
                        _process.StartInfo.Arguments = " -i " + videoPath + " -vn -f mp3 -ab 192k " + videoPath.Replace(".mp4", ".mp3");
                        try
                        {
                            _process.Start();
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                        // _process.StandardOutput.ReadToEnd();
                        // _out = _process.StandardError.ReadToEnd();
                        _process.WaitForExit();
                        /* if (_process.HasExited)
                         {
                             _process.Kill();
                         }*/
                        _process.Close();

                        string audioPath = AppConfig.GetInstance().AudioServerPath;
                        File.Copy(Server.MapPath("~") + "\\SessionFiles\\" + filename.Replace(".mp4", ".mp3"), audioPath + filename.Replace(".mp4", ".mp3"));

                        long size = fuAttAvatar.FileContent.Length;
                        AudioFile file = new AudioFile();
                        file.Name = filename;
                        file.FileSize = size;
                        file.CreatedAt = DateTime.Now;
                        file.Status = 1;
                        file.FileType = 1;
                        AudioFileHelper.AddNewFile(file);
                        lblInfo1.Text = "تم الحفظ بنجاح";
                        lblInfo1.Visible = true;
                        gvbind();
                        Response.Redirect("Default.aspx");
                    }
                    else if (fuAttAvatar.PostedFile.ContentType.ToLower().IndexOf("mp3") != -1)
                    {
                        string audioPath = AppConfig.GetInstance().AudioServerPath;
                        fuAttAvatar.SaveAs(audioPath + filename);
                        fuAttAvatar.SaveAs(Server.MapPath("~") + "\\SessionFiles\\" + filename);

                        long size = fuAttAvatar.FileContent.Length;
                        AudioFile file = new AudioFile();
                        file.Name = filename;
                        file.FileSize = size;
                        file.CreatedAt = DateTime.Now;
                        file.Status = 1;
                        file.FileType = 0;
                        AudioFileHelper.AddNewFile(file);
                        lblInfo1.Text = "تم الحفظ بنجاح";
                        lblInfo1.Visible = true;
                        gvbind();
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        lblInfo1.Text = "ملف غير صحيح";
                        lblInfo1.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblInfo1.Text = ex.Message;
                lblInfo1.Visible = true;
            }
        }

        protected void gvbind()
        {
            List<AudioFile> AudioFileLst = AudioFileHelper.GetAudioFiles();
            gvAudioFiles.DataSource = AudioFileLst;
            gvAudioFiles.DataBind();
        }

        public string postbackurl(string id,string type)
        {
            string url = "";
            if (int.Parse(type) == 0)
            {
                url = String.Format("~/EditSessionFile.aspx?sfid={0}", id);
            }
            else
            {
                url = String.Format("~/EditVideoSessionFile.aspx?sfid={0}", id);
            }
            return url;
        }

        public string GetFileStatusString(string filename,string type)
        {
            string fileStatusStr = "";
            string filetype = "mp3";
            if (type=="1")
                filetype = "mp4";
            string xmlFilePath = string.Format("{0}{1}", AppConfig.GetInstance().VecSysServerPath, filename.ToLower().Replace(filetype, "trans.xml"));
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
                        fileStatusStr += "<br>" + ProcessDirectory("\\\\192.168.0.60\\tmp", filename, type);
                    }
                }
            }
            else fileStatusStr = "لم يتم البدء فى الملف";

            return fileStatusStr;
        }

        public static string ProcessDirectory(string targetDirectory, string fileName, string type)
        {
            string txt = "";
            string filetype = "mp3";
            if (type == "1")
                filetype = "mp4";
            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                if (subdirectory.IndexOf(fileName.Replace("." + filetype, "")) > 0)
                {
                    string fileprocesspath = subdirectory + "\\input.idp";
                    if (File.Exists(fileprocesspath))
                    {
                        txt = System.IO.File.ReadAllText(fileprocesspath);
                        txt = txt.Replace("vrbs_trans:", "").Replace("\\n", "");
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
