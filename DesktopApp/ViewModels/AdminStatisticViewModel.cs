using Database.Records;
using Database.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace DesktopApp.ViewModels;

internal class AdminStatisticViewModel : ViewModel
{
    private readonly PlayerStatisticRepository _playerStatisticRepository; 

    private ObservableCollection<PlayerStatisticRecord> _stats = [];

    public ObservableCollection<PlayerStatisticRecord> UserStats
    {
        get => _stats;
        set => Set(ref _stats, value);
    }

    public AdminStatisticViewModel()
    {
        _playerStatisticRepository = App.ServiceProvider.GetRequiredService<PlayerStatisticRepository>();

        UserStats = new ObservableCollection<PlayerStatisticRecord>(_playerStatisticRepository.GetAllAsync().Result.Where(s => s.TotalTimePlayed > 0));
    }
}


