using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CW.ViewModels.InsideViewModels
{
    public class VisitHistoryViewModel : BaseViewModel
    {
        public ObservableCollection<string> LastVisits { get; protected set; }

        public VisitHistoryViewModel(List<string> dates)
        {
            LastVisits = new ObservableCollection<string>(dates);
        }
    }
}
