using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TayaIT.Enterprise.Neyaba.DB
{
    public static class AudioFileHelper
    {
        #region methods
        public static bool AddNewFile(string text,long size,DateTime createdAt)
        {
            try
            {
                using (NeyabatDemoEntities context = new NeyabatDemoEntities())
                {
                    AudioFile item = new AudioFile
                    {
                        Name = text,
                        FileSize = size,
                        CreatedAt = createdAt
                    };
                    context.AudioFiles.AddObject(item);
                    context.SaveChanges();
                    //context.Refresh(System.Data.Objects.RefreshMode.StoreWins, item);
                    return true;
                }
            }
            catch (Exception ex)
            {
               // LogHelper.LogException(ex, "TayaIT.Enterprise.Neyaba.DB.AudioFileHelper.AddNewFile(" + text + ")");
                return false;
            }

        }

        public static bool AddNewFile(AudioFile audioFile)
        {
            try
            {
                using (NeyabatDemoEntities context = new NeyabatDemoEntities())
                {
                    context.AudioFiles.AddObject(audioFile);
                    context.SaveChanges();
                    //context.Refresh(System.Data.Objects.RefreshMode.StoreWins, item);
                    return true;
                }
            }
            catch (Exception ex)
            {
                // LogHelper.LogException(ex, "TayaIT.Enterprise.Neyaba.DB.AudioFileHelper.AddNewFile(" + text + ")");
                return false;
            }

        }
        public static List<AudioFile> GetAudioFiles()
        {
            try
            {
                using (NeyabatDemoEntities context = new NeyabatDemoEntities())
                {
                    return context.AudioFiles.Where(c => c.Status == 1).OrderByDescending(c => c.CreatedAt).ToList();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.LogException(ex, "TayaIT.Enterprise.EMadbatah.DAL.AgendaHelper.GetAgendaItemsByAgendaID(" + agendaID + ")");
                return null;
            }
        }

        public static AudioFile GetAudioFileByID(long id)
        {
            try
            {
                using (NeyabatDemoEntities context = new NeyabatDemoEntities())
                {
                    return context.AudioFiles.FirstOrDefault(c => c.ID == id);
                }
            }
            catch (Exception ex)
            {
                //LogHelper.LogException(ex, "TayaIT.Enterprise.EMadbatah.DAL.AgendaHelper.GetAgendaItemsByAgendaID(" + agendaID + ")");
                return null;
            }
        }

        public static int DeleteAudioFileById(long AudioFileID)
        {
            try
            {
                AudioFile AudioFileForDelete = null;
                using (NeyabatDemoEntities context = new NeyabatDemoEntities())
                {
                    AudioFileForDelete = context.AudioFiles.FirstOrDefault(a => a.ID == AudioFileID);
                    if (AudioFileForDelete != null)
                    {
                        AudioFileForDelete.Status = 2;
                    }

                    int res = context.SaveChanges();
                    return res;
                }
            }
            catch (Exception ex)
            {
                //LogHelper.LogException(ex, "TayaIT.Enterprise.EMadbatah.DAL.StageHelper.DeleteStageById(" + StageID + ")");
                return -1;
            }
        }

        #endregion

    }
}

