using Database.Records;
using System.Collections.ObjectModel;

namespace DesktopApp.ViewModels;

internal class AdminAchievementsViewModel : ViewModel
{
    private ObservableCollection<AchievementRecord> _achievements = [];

    public ObservableCollection<AchievementRecord> Achievements
    {
        get => _achievements;
        set => Set(ref _achievements, value);
    }
}


