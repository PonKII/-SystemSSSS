using System;
using System.Collections.Generic;
using System.CourseManagement.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.CourseManagement.Model
{
    public class LoginModel:NotifyBase
    {
		private string _userName;

		public  string UserName
		{
			get { return _userName; }
			set 
			{ 
				_userName = value;
				this.DoNotify();
			}
		}

		private string _password;

		public string PassWord
		{
			get { return _password; }
			set 
			{ 
				_password = value;
                this.DoNotify();
            }
		}

		private string _verificationcode;
		public string VerificationCode
        {
			get { return _verificationcode; }
			set
			{
                _verificationcode = value;
				this.DoNotify();
			}
		}

	}
}
