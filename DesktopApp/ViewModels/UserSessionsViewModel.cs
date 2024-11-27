using Database.Records;
using Database.Repositories;
using DesktopApp.Tools;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DesktopApp.ViewModels;

internal class UserSessionsViewModel : ViewModel
{
    private Random _random = new();
    private ObservableCollection<GameSessionRecord> _sessions;
    private GameSessionRepository _sessionRepository;
    public UserSessionsViewModel()
    {
        _sessionRepository = App.ServiceProvider.GetRequiredService<GameSessionRepository>();
        _sessions = new ObservableCollection<GameSessionRecord>(_sessionRepository.GetByUserId(App.CurrentUser!.Id).Result);
    }

    

    public ObservableCollection<GameSessionRecord> Sessions
    {
        get => _sessions;
        set => Set(ref  _sessions, value);
    }

    public ICommand AddRandomSessionCommand => new RelayCommand(AddRandomSession);

    private async void AddRandomSession()
    {
        var randomSession = new GameSessionRecord
        {
            Id = Guid.NewGuid(),
            UserId = App.CurrentUser!.Id,
            SessionDate = DateTime.Now,
            Score = _random.Next(1, 1000),
            Duration = Math.Round(_random.NextDouble() * _random.Next(1, 100), 2),
            Result = (GameResults)_random.Next(0,3)

        };

        Sessions.Add(randomSession);
        await _sessionRepository.AddAsync(randomSession);
    }
}

