using MyControllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.CourseManagement.Common;
using System.CourseManagement.DataAccess;
using System.CourseManagement.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace System.CourseManagement.ViewModel
{
    public class CoursePageViewModel
    {
        public ObservableCollection<CategoryItemModel> CategoryCourses {  get; set; }
        public ObservableCollection<CategoryItemModel> CategoryTechnology {  get; set; }
        public ObservableCollection<CategoryItemModel> CategoryCharge {  get; set; }
        public ObservableCollection<CourseModel> CourseList { get; set; } = new ObservableCollection<CourseModel>();
        private List<CourseModel> CourseAll;
        public CommandBase OpenUrl { get; set; }
        public CommandBase PermanentFilterCommand {  get; set; }
        public CoursePageViewModel()
        {
            this.OpenUrl = new CommandBase();
            this.OpenUrl.DoCanExecute = new Func<object, bool>((o) => true);
            this.OpenUrl.DoExecute = new Action<object>((o) => { System.Diagnostics.Process.Start(o.ToString()); });

            this.PermanentFilterCommand = new CommandBase();
            this.PermanentFilterCommand.DoCanExecute = new Func<object, bool>((o) => true);
            this.PermanentFilterCommand.DoExecute = new Action<object>(DoFilter);
            this.InitCategory();
            this.InitCourseList();
        }
        private void DoFilter(object o)
        {
            string permanent = o.ToString();
            List<CourseModel> temp = CourseAll;
            if(permanent != "全部")
            {
                temp= CourseAll.Where(c => c.Permanents.Exists(e => e == permanent)).ToList();
            }

            CourseList.Clear();
            foreach (var item in temp)
                CourseList.Add(item);
        }
        private void InitCategory()
        {
            this.CategoryCourses = new ObservableCollection<CategoryItemModel>();
            this.CategoryCourses.Add(new CategoryItemModel("全部", true));
            this.CategoryCourses.Add(new CategoryItemModel("公开课"));
            this.CategoryCourses.Add(new CategoryItemModel("专业课"));

            this.CategoryTechnology = new ObservableCollection<CategoryItemModel>();
            this.CategoryTechnology.Add(new CategoryItemModel("全部", true));
            this.CategoryTechnology.Add(new CategoryItemModel("C#"));
            this.CategoryTechnology.Add(new CategoryItemModel("WPF"));
            this.CategoryTechnology.Add(new CategoryItemModel("WinForm"));
            this.CategoryTechnology.Add(new CategoryItemModel("MySQL"));

            this.CategoryCharge = new ObservableCollection<CategoryItemModel>();
            this.CategoryCharge.Add(new CategoryItemModel("全部", true));
            foreach (var item in MySqlDataAccess.GetInstance().GetPermanents())
            {
                this.CategoryCharge.Add(new CategoryItemModel(item));
            }
        }
        private void InitCourseList()
        {
            for(int i = 0;i < 10; i++)
            {
                CourseList.Add(new CourseModel { IsShowSkeleton = true });
            }
            Task.Run(new Action(async () => 
            {
                CourseAll = MySqlDataAccess.GetInstance().GetCourses();
                await Task.Delay(4000);

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    CourseList.Clear();
                    foreach (var item in CourseAll)
                        CourseList.Add(item);
                }));
            }));
        }
    }
}
