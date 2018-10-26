using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMSBusinessLogic
{
    public class CommonMessages
    {
        public const string GLOBAL_ERROR_MESSAGE = "Please contact administrator about Error ID ";

        #region Master Page
        public const string MSG_WELCOME = "Welcome ";
        public const string MSG_PW_UPDATE_SUCCESS = "Password is updated successfully";
        #endregion

        #region Google OpenID
        public const string ERROR_GOOGLE_ACCOUNT_NOT_FOUND = "Google account is not found";
        public const string ERROR_GOOGLE_ACCOUNT_NOT_FOUND_IN_ABCLogistics_GLOBAL = "The Google Account isn't found in our system";
        public const string ERROR_INVALID_GOOGLE_IDENTIFIER = "Invalid Google identifier";
        public const string MSG_GOOGLE_AUTHENTICATION_FAILED = "Google OpenID authentication failed";
        public const string MSG_GOOGLE_AUTHENTICATION_CANCELED = "Google OpenID authentication canceled";
        public const string MSG_GOOGLE_AUTHENTICATION_SETUP_REQUIRED = "Google OpenID authentication requires setup";
        public const string MSG_GOOGLE_AUTHENTICATION_EXTENSIONS_ONLY = "Google OpenID authentication extensions only";
        #endregion

        #region Log In
        public const string MSG_LOG_IN_EMPTY_EMAIL = "Please enter email address";
        public const string MSG_LOG_IN_EMPTY_PASSWORD = "Please enter password";
        public const string ERROR_LOG_IN_EMAIL_NOT_FOUND = "This email address is not found";
        public const string ERROR_GLOBAL_ACCOUNT_NOT_FOUND = "Your ABCLogistics Global account is not found";
        public const string ERROR_GLOBAL_NO_APPLICATION_ACCESS = "You don't have access to this application. Please contact administrator for details";
        #endregion

        #region Forgot Password
        public const string ERROR_FORGOT_PW_EMPTY_EMAIL = "Email address should not be empty.";
        public const string ERROR_FORGOT_PW_INVALID_EMAIL = "Email address is not valid.";
        public const string MSG_FORGOT_PW_EMAIL_SENT = "Password reset email has been sent successfully.";
        public const string ERROR_FORGOT_PW_EMAIL_NOT_FOUND = "The email you entered could not be found";
        public const string FORGOT_PW_EMAIL_SUBJECT = "Password Reset Request";

        #endregion

        #region Reset Password
        public const string MSG_PW_RESET_SUCCESS = "Your password has been reset. Please log in with your email address and the new password.";
        public const string ERROR_PW_RESET_FAIL = "Your password could not be reset because your email address is not found or the password is not valid.";
        public const string ERROR_PW_RESET_INVALID_PW = "The password is not valid. Please Contact ABCLogistics IT department for further information.";
        public const string ERROR_PW_RESET_INVALID_REQUEST = "Invalid request. Please Contact ABCLogistics IT department for further information.";
        public const string ERROR_PW_RESET_UNKNOWN_ERROR = "An error occured during processing your request. Please Contact ABCLogistics IT department for further information.";
        #endregion
    }
}
