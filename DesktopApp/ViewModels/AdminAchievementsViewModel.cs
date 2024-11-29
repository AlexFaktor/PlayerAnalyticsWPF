using Database.Records;
using Database.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace DesktopApp.ViewModels;

internal class AdminAchievementsViewModel : ViewModel
{
    private readonly AchievementRepository _achievementRepository;


    private ObservableCollection<AchievementRecord> _achievements = [];

    public ObservableCollection<AchievementRecord> Achievements
    {
        get => _achievements;
        set => Set(ref _achievements, value);
    }

    public AdminAchievementsViewModel()
    {
        _achievementRepository = App.ServiceProvider.GetRequiredService<AchievementRepository>();

        Achievements = new ObservableCollection<AchievementRecord>(_achievementRepository.GetAllAsync().Result);
    }
}


