using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TayaIT.Enterprise.Neyaba.DB
{
    public static class UserHelper
    {
        #region methods

        public static User Login(string username, string pass)
        {
            try
            {
                using (NeyabatDemoEntities context = new NeyabatDemoEntities())
                {
                    return context.Users.Where(c => c.Name == username && c.Pass == pass).First();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

    }
}

