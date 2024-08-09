using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.SeriesAlgorithms;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.CourseManagement.Common;
using System.CourseManagement.DataAccess;
using System.CourseManagement.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.CourseManagement.ViewModel
{
    public class FirstPageViewModel:NotifyBase
    {
		private int _instrumentValue;

		public int InstrumentValue 
		{
			get { return _instrumentValue; }
			set { _instrumentValue = value; this.DoNotify(); }
		}

        private int _itemCount;

        public int ItemCount
        {
            get { return _itemCount; }
            set { _itemCount = value; }
        }

        public ObservableCollection<CourseSeriesModel> CourseSeriesList { get; set; } = new ObservableCollection<CourseSeriesModel>();

        Random random = new Random();
        bool taskSwitch = true;
        List<Task> taskList = new List<Task>();
        public FirstPageViewModel()
        {
            this.RefreshInstrumentValue();

            this.InitCourseSeries();
        }

        private void InitCourseSeries()
        {
            var cList = MySqlDataAccess.GetInstance().GetCoursePlayRecord();
            this.ItemCount = cList.Max(c => c.SeriesList.Count);
            foreach (var item in cList) 
                this.CourseSeriesList.Add(item);
        }
        private void RefreshInstrumentValue()
        {
           var task = Task.Factory.StartNew(new Action(async() =>
            {
                while (true)
                {
                    InstrumentValue = random.Next(Math.Max(this.InstrumentValue - 5,0),Math.Min(this.InstrumentValue + 5,100));
                    
                    await Task.Delay(1000);
                }
            }));
        }

        public void Dispose()
        {
            try
            {
                taskSwitch = false;
                Task.WaitAll(this.taskList.ToArray());
            }
            catch { }
        }
    }
}
