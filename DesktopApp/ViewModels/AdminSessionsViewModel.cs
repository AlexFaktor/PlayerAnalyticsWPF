using Database.Records;
using System.Collections.ObjectModel;

namespace DesktopApp.ViewModels;

internal class AdminSessionsViewModel : ViewModel
{
    private ObservableCollection<GameSessionRecord> _sessions = [];

    public ObservableCollection<GameSessionRecord> Sessions
    {
        get => _sessions;
        set => Set(ref _sessions, value);
    }
}


