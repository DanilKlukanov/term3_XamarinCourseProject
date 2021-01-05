using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CW.ViewModels.InsideViewModels
{
    public class VisitHistoryViewModel : BaseViewModel
    {
        public ObservableCollection<object> LastVisits { get; protected set; }

        public VisitHistoryViewModel()
        {
            LastVisits = new ObservableCollection<object>();
        }
    }
}
