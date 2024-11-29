using Database.Records;
using Database.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace DesktopApp.ViewModels;

internal class AdminSessionsViewModel : ViewModel
{
    private readonly GameSessionRepository _sessionRepository;

    private ObservableCollection<GameSessionRecord> _sessions = [];

    public ObservableCollection<GameSessionRecord> Sessions
    {
        get => _sessions;
        set => Set(ref _sessions, value);
    }

    public AdminSessionsViewModel()
    {
        _sessionRepository = App.ServiceProvider.GetRequiredService<GameSessionRepository>();

        Sessions = new ObservableCollection<GameSessionRecord>(_sessionRepository.GetAllAsync().Result);
    }
}


