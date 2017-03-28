using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TayaIT.Enterprise.EMadbatah.Model.VecSys;
using TayaIT.Enterprise.EMadbatah.Vecsys;
using TayaIT.Enterprise.EMadbatah.Model;
using System.Collections;
using TayaIT.Enterprise.EMadbatah.Config;
using System.Text;
using System.IO;

namespace TayaIT.Enterprise.Neyabat.Web
{
    public partial class EditSessionFile : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }
       
        private void BindData()
        {
            if (!string.IsNullOrEmpty(SessionFileID))
            {
                Neyaba.DB.AudioFile file = Neyaba.DB.AudioFileHelper.GetAudioFileByID(long.Parse(SessionFileID));
                if (file != null && file.Status == 1)
                {
                    string mp3FilePath = string.Format("{0}{1}", AppConfig.GetInstance().AudioServerPath, file.Name);
                    string txtFilePath = string.Format("{0}{1}", AppConfig.GetInstance().VecSysServerPath, file.Name.ToLower().Replace("mp3", "txt"));
                    string xmlFilePath = string.Format("{0}{1}", AppConfig.GetInstance().VecSysServerPath, file.Name.ToLower().Replace("mp3", "trans.xml"));

                    // XmlFilePath.Value = xmlFilePath;
                    MP3FilePath.Value = string.Format("{0}://{1}:{2}/sessionfiles/", Request.Url.Scheme, Request.Url.Host, Request.Url.Port) + file.Name; //"http://localhost:13000/SessionFiles/" + file.Name;//"http://localhost:13000/SessionFiles/S114_4_24_PM.mp3"; //"D:\\Dina\\Audios & videso\\Quran\\007.mp3";// mp3FilePath;// mp3FilePath;
                    lblMP3FileName.Text = file.Name;
                    lblFileDate.Text = file.CreatedAt.ToString();
                    lblFileSize.Text = CalculateSize(file.FileSize.ToString());

                    // set editor text
                    if (File.Exists(xmlFilePath))
                    {
                        FileInfo fileInfo = new FileInfo(xmlFilePath);
                        if (fileInfo.Length > 0)
                        {
                            //string text = System.IO.File.ReadAllText(txtFilePath);
                            string text = loadxml(xmlFilePath);
                            elm1.Value = text;
                        }
                        else
                        {
                            lblInfo1.Text = "ما زال الملف قيد التحويل";
                            lblInfo1.Visible = true;
                            elm1.Value = "";
                        }
                    }
                    else
                    {
                        lblInfo1.Text = "لم يتم البدء فى الملف";
                        lblInfo1.Visible = true;
                        elm1.Value = "";
                    }
                }
                else
                {
                    lblInfo1.Text = "هذا الملف غير متاح حاليا";
                    lblInfo1.Visible = true;
                    elm1.Value = "";
                }
            }
        }

        private string loadxml(string xmlFilePath)
        {
            string html = "";
            TransFile tf = new TransFile();
            tf = VecsysParser.ParseTransXml(xmlFilePath);
            List<Paragraph> pLst = VecsysParser.combineSegments(tf.SpeechSegmentList);
            foreach (Paragraph p in pLst)
            {
                html += getHTML(p);
            }
            return html;
        }

        private string getHTML(Paragraph p)
        {
            string output = "";
            foreach (SpeechSegment segment in p.speechSegmentsList)
            {
                foreach (TayaIT.Enterprise.EMadbatah.Model.VecSys.Word word in segment.words)
                {
                    output += "<span class='segment' data-stime='" + word.stime.ToString() + "'>" + word.value.Replace(".", "،") + "</span> ";
                }
            }
            return output;
        }

     
    }
}