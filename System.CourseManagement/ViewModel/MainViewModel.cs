using System;
using System.Collections.Generic;
using System.CourseManagement.Common;
using System.CourseManagement.Model;
using System.CourseManagement.View;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace System.CourseManagement.ViewModel
{
    public class MainViewModel:NotifyBase
    {
        public UserModel UserInfo { get; set; }

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; this.DoNotify(); }
        }

        private FrameworkElement _mainContent;

        public FrameworkElement MainContent
        {
            get { return _mainContent; }
            set { _mainContent = value; this.DoNotify(); }
        }

        public CommandBase NavChangedCommand { get; set; }

        public MainViewModel()
        {
            UserInfo = new UserModel();
            //this.NavChangedCommand = new CommandBase();
            //this.NavChangedCommand = new Action<object>(DoNavChanged);
            //this.NavChangedCommand.DoCanExecute = new Func<object, bool>((o) => true);
            this.NavChangedCommand = new CommandBase
            {
                DoExecute = DoNavChanged,
                DoCanExecute = (o) => true
            };
            DoNavChanged("FirstPageView");
        }

        private void DoNavChanged(object obj)
        {
            Type type = Type.GetType("System.CourseManagement.View."+obj.ToString());
            ConstructorInfo cti = type.GetConstructor(System.Type.EmptyTypes);
            this.MainContent = (FrameworkElement)cti.Invoke(null);
        }
    }
}
