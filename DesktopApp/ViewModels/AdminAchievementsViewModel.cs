using Database.Records;
using Database.Repositories;
using DesktopApp.Tools;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;

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

    public ICommand DeleteAchievementCommand { get; set; }

    public AdminAchievementsViewModel()
    {
        _achievementRepository = App.ServiceProvider.GetRequiredService<AchievementRepository>();

        Achievements = new ObservableCollection<AchievementRecord>(_achievementRepository.GetAllAsync().Result);

        DeleteAchievementCommand = new RelayCommand<AchievementRecord>(DeleteAchievement);
    }

    private void DeleteAchievement(AchievementRecord achievement)
    {
        if (achievement != null)
        {
            Achievements.Remove(achievement);
            _achievementRepository.DeleteAsync(achievement.Id);
        }
    }
}


