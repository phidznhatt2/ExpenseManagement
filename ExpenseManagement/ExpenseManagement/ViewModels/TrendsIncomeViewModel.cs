using ExpenseManagement.Consts;
using ExpenseManagement.Models;
using ExpenseManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace ExpenseManagement.ViewModels
{
    public class TrendsIncomeViewModel : BaseViewModel
    {
        public ObservableCollection<DataChart> DataList { get; set; }
        public TypeMethod typeConst;
        public ChartsServices chartsServices;
        private string idUser = Application.Current.Properties["userId"].ToString();
        public class Month
        {
            public int key { get; set; }

            public string name { get; set; }
        }

        private string _name { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private List<Month> _listMonth { get; set; }
        public List<Month> ListMonth
        {
            get { return _listMonth; }
            set
            {
                _listMonth = value;
                OnPropertyChanged("ListMonth");
            }
        }
        public TrendsIncomeViewModel()
        {
            Title = "Trends";

            ListMonth = new List<Month>
            {
                new Month{key = 1, name = "January"},
                new Month{key = 2, name = "February"},
                new Month{key = 3, name = "March"},
                new Month{key = 4, name = "April"},
                new Month{key = 5, name = "May"},
                new Month{key = 6, name = "June"},
                new Month{key = 7, name = "July"},
                new Month{key = 8, name = "August"},
                new Month{key = 9, name = "September"},
                new Month{key = 10, name = "October"},
                new Month{key = 11, name = "November"},
                new Month{key = 12, name = "December"},
            };

            DataList = new ObservableCollection<DataChart>();
        }

        private int _selectedKeyMonth { get; set; }
        public int SelectedKeyMonth
        {
            get { return _selectedKeyMonth; }
            set
            {
                _selectedKeyMonth = value;
                OnPropertyChanged("SelectedKeyMonth");
            }
        }

        private bool _isVisible { get; set; }
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        private Month _selectedMonth { get; set; }

        public Month SelectedMonth
        {
            get { return _selectedMonth; }
            set
            {
                _selectedMonth = value;
                OnPropertyChanged("SelectedMonth");

                HandleSelectedMonthChanged(_selectedMonth);
            }
        }

        private bool _isVisibleAlert { get; set; }

        public bool IsVisibleAlert
        {
            get { return _isVisibleAlert; }
            set
            {
                _isVisibleAlert = value;
                OnPropertyChanged("IsVisibleAlert");
            }
        }

        private async void HandleSelectedMonthChanged(Month item)
        {
            if (item != null)
            {
                await ExecuteLoadChartCommand(item.key);
            }
        }

        public async Task ExecuteLoadChartCommand(int id)
        {
            DataList.Clear();

            typeConst = new TypeMethod();
            chartsServices = new ChartsServices();
            var dataList = await chartsServices.GetDataChart(idUser, id, typeConst.Income);
            Name = ListMonth[id - 1].name;
            if (dataList.data.items.Count > 0)
            {
                foreach (DataChart item in dataList.data.items)
                {
                    DataList.Add(item);
                }

                IsVisible = true;
                IsVisibleAlert = false;
            }
            else
            {
                IsVisibleAlert = true;
                IsVisible = false;
            }
        }
    }
}
