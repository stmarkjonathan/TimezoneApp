using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TimezoneApp.Services;

namespace TimezoneApp.ViewModels
{
    public class ClockWindowViewModel : INotifyPropertyChanged
    {
        private ReadOnlyCollection<TimeZoneInfo>  _systemTimeZones = TimeZoneInfo.GetSystemTimeZones();

        private List<TimeZoneInfo> _timeZoneList;

        public List<string> TimeZoneList
        {
            get 
            {
                if (_timeZoneList == null)
                {
                    _timeZoneList = _systemTimeZones.ToList();
                }

                return _timeZoneList.Select(x => x.Id).OrderBy(x => x).ToList();

            }
        }

        private DateTime _displayedTime;
        public DateTime DisplayedTime 
        {  
            get 
            { 
                if(_displayedTime == DateTime.MinValue)
                {
                    _displayedTime = DateTime.Now;
                }
                return _displayedTime;
            }
            set 
            { 
                _displayedTime = value;
                OnPropertyChanged("DisplayedTime");
            }
        }

        private string _searchQuery;

        public string SearchQuery
        {
            get 
            {
                return _searchQuery;
            }
            set
            {
                _searchQuery = value.Trim();
                OnPropertyChanged("SearchQuery");
            }
        }

        private string _selectedTimeZone;
        public string SelectedTimeZone
        {
            get 
            {
                if(_selectedTimeZone == null)
                {
                    _selectedTimeZone = TimeZoneInfo.Local.Id;
                }
                return _selectedTimeZone; 
            }
            set 
            { 
                _selectedTimeZone = value;
                UpdateClock();
                OnPropertyChanged("SelectedTimeZone");
            }
        }

        public void UpdateList(string searchQuery)
        {
            if(String.IsNullOrWhiteSpace(searchQuery))
            {
                _timeZoneList = _systemTimeZones.ToList();
                OnPropertyChanged("TimeZoneList");
                return;
            }

            _timeZoneList.Clear();

            foreach(var item in _systemTimeZones)
            {
                if (item.Id.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                {
                    _timeZoneList.Add(item);
                }
            }
            OnPropertyChanged("TimeZoneList");
        }

        public void UpdateClock()
        {     
            DisplayedTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(SelectedTimeZone));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
