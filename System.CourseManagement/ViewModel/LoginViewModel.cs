using System;
using System.Collections.Generic;
using System.CourseManagement.Common;
using System.CourseManagement.DataAccess;
using System.CourseManagement.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace System.CourseManagement.ViewModel
{
    public class LoginViewModel:NotifyBase
    {
        public LoginModel loginModel { get; set; } = new LoginModel();
        
        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase LoginCommand { get; set; }
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value;
                this.DoNotify();
            }
        }
        private Visibility _showProgress = Visibility.Collapsed;

        public Visibility ShowProgress
        {
            get { return _showProgress; }
            set
            {
                _showProgress = value; 
                this.DoNotify();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public LoginViewModel() {
            //this.loginModel = new Model.LoginModel();
            //this.loginModel.UserName = "AAA猪肉批发";
            //this.loginModel.PassWord = "123123";
            this.CloseWindowCommand = new CommandBase();
            this.CloseWindowCommand.DoExecute = new Action<object>((o) =>
            {
                (o as Window).Close();
            });
            this.CloseWindowCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            this.LoginCommand = new CommandBase();
            this.LoginCommand.DoExecute = new Action<object>(DoLogin);
            this.LoginCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });
            this.LoginCommand.DoCanExecute = new Func<object, bool>((o) => { return ShowProgress == Visibility.Collapsed; });
        }
        private void DoLogin(object o)
        {
            this.ShowProgress = Visibility.Visible;
            this.ErrorMessage = "";
            if (string.IsNullOrEmpty(loginModel.UserName))
            {
                this.ErrorMessage = "请输入用户名！";
                this.ShowProgress = Visibility.Collapsed;
                return;
            }

            if (string.IsNullOrEmpty(loginModel.PassWord))
            {
                this.ErrorMessage = "请输入密码！";
                this.ShowProgress = Visibility.Collapsed;
                return;
            }
            if (string.IsNullOrEmpty(loginModel.VerificationCode))
            {
                this.ErrorMessage = "请输入验证码！";
                this.ShowProgress = Visibility.Collapsed;
                return;
            }
            if (loginModel.VerificationCode.ToLower() != "abcd")
            {
                this.ErrorMessage = "您输入的验证码有误，请重新输入！";
                this.ShowProgress = Visibility.Collapsed;
                return;
            }

            Task.Run(new Action(() =>
            {
                try
                {
                    var user = MySqlDataAccess.GetInstance().CheckUserInfo(loginModel.UserName, loginModel.PassWord);
                    if (user == null)
                    {
                        throw new Exception("登录失败！用户名或密码错误！");
                    }

                    GlobalValues.UserInfo = user;


                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    { 
                        (o as Window).DialogResult = true;
                    }));
                }
                catch (Exception ex)
                {
                    this.ErrorMessage = ex.Message;
                }
            }));
        }
    }
}
