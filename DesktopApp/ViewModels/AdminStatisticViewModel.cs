using Database.Records;
using System.Collections.ObjectModel;

namespace DesktopApp.ViewModels;

internal class AdminStatisticViewModel : ViewModel
{
    private ObservableCollection<PlayerStatisticRecord> _stats = [];

    public ObservableCollection<PlayerStatisticRecord> UserStats
    {
        get => _stats;
        set => Set(ref _stats, value);
    }
}


