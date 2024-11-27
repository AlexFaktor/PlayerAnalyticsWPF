using Database.Records;
using System.Collections.ObjectModel;

namespace DesktopApp.ViewModels;

internal class AdminReportsViewModel : ViewModel
{
    private ObservableCollection<ReportRecord> _reports = [];

    public ObservableCollection<ReportRecord> Reports
    {
        get => _reports;
        set => Set(ref _reports, value);
    }
}


