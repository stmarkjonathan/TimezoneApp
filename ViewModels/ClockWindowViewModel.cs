using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TimezoneApp.ViewModels
{
    public class ClockWindowViewModel : INotifyPropertyChanged
    {

        private ReadOnlyCollection<TimeZoneInfo> _timeZoneList = TimeZoneInfo.GetSystemTimeZones();

        public List<string> TimeZoneList
        {
            get 
            {
                //use linq to reference searched item later
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
                UpdateTimezone(value);
                OnPropertyChanged("SelectedTimeZone");
            }
        }

        public ClockWindowViewModel() 
        {
        }

        private void UpdateTimezone(string timezoneID)
        {     
            DisplayedTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(timezoneID));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
