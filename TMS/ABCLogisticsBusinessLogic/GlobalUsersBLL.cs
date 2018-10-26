using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABCLogisticsBusinessLogic
{
    public class GlobalUsersBLL : BaseBLL
    {
        public GlobalUsersBLL() : base()
        {

        }

        public GlobalUsersBLL(string connectionString) : base(connectionString)
        {
            
        }

        public Global_AuthenticateUserByEmail_Result AuthenticateUserByOpenID(string email, Guid identifier)
        {
            return entities.Global_AuthenticateUserByEmail(email, identifier).SingleOrDefault();
        }

        public Global_AuthenticateUser_Result AuthenticateUser(string userName, string password)
        {
            return entities.Global_AuthenticateUser(userName, password).SingleOrDefault();
        }

        public int GetApplicationUserByUser(int globalUserID, ABCLogisticsApp application)
        {
            var applicationUserID = entities.Global_GetUserAppUser(globalUserID, (int)application).SingleOrDefault();
            return applicationUserID.HasValue ? applicationUserID.Value : 0;
        }

        public string GetUserNameByEmail(string email)
        {
            try
            {
                return (from user in entities.Users
                        where user.Email == email
                        select user.FirstName + " " + user.LastName).DefaultIfEmpty(string.Empty).SingleOrDefault();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public bool UpdatePassword(string email, string newPassword)
        {
            try
            {
                var user = (from u in entities.Users
                            where u.Email == email
                            select u).SingleOrDefault();
                if (user != null)
                {
                    user.Password = GlobalUtil.EncryptString(newPassword);
                    user.UpdateDate = DateTime.Now;
                    entities.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                (new GlobalErrorLogsBLL()).LogGlobalError(ex, "TMS - GlobalUsersBLL - UpdatePassword");
                return false;
            }
        }
    }
}
