using Database.Records;
using Database.Repositories;
using DesktopApp.Tools;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DesktopApp.ViewModels;

internal class UserAchievementsViewModel : ViewModel
{
    private readonly AchievementRepository _achievementRepository;
    private readonly Random _random = new();

    public UserAchievementsViewModel()
    {
        _achievementRepository = App.ServiceProvider.GetRequiredService<AchievementRepository>();
        Achievements = new ObservableCollection<AchievementRecord>(_achievementRepository.GetByUserId(App.CurrentUser!.Id).Result);

        AddRandomAchievementCommand = new RelayCommand(AddRandomAchievement);
    }

    public ObservableCollection<AchievementRecord> Achievements { get; }

    public ICommand AddRandomAchievementCommand { get; }

    private async void AddRandomAchievement()
    {
        var randomAchievement = new AchievementRecord
        {
            Id = Guid.NewGuid(),
            UserId = App.CurrentUser!.Id,
            AchievementName = $"Досягнення #{_random.Next(1, 100)}",
            DateUnlocked = DateTimeOffset.Now
        };

        Achievements.Add(randomAchievement);

        await _achievementRepository.AddAsync(randomAchievement);
    }
}


