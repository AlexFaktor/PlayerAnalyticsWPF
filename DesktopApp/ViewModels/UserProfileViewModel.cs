using Database.Records;
using Database.Repositories;
using DesktopApp.Tools;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;

namespace DesktopApp.ViewModels;

internal class UserProfileViewModel : ViewModel
{
    private UserRepository _userRepository;
    private PlayerStatisticRepository _playerStatisticRepository;
    private GameSessionRepository _gameSessionRepository;
    private AchievementRepository _achievementRepository;

    public PlayerStatisticRecord PlayerStatisticRecord { get; set; }
    public List<GameSessionRecord> Sessions { get; set; } = [];
    public List<AchievementRecord> Achievements { get; set; } = [];

    private string _userName;


    public string UserName
    {
        get => _userName;
        set => Set(ref _userName, value);
    }

    public int GameLose { get; set; }
    public double Winrate { get; set; }
    public double TimePlayed { get; set; }
    public int AchievementsCount { get; set; }

    public ICommand ChangeNameCommand => new RelayCommand(ChangeName);

    public UserProfileViewModel()
    {
        var serviceProvider = App.ServiceProvider;
        _userRepository = serviceProvider.GetRequiredService<UserRepository>();
        _playerStatisticRepository = serviceProvider.GetRequiredService<PlayerStatisticRepository>();
        _gameSessionRepository = serviceProvider.GetRequiredService<GameSessionRepository>();
        _achievementRepository = serviceProvider.GetRequiredService<AchievementRepository>();

        Sessions = _gameSessionRepository.GetByUserId(App.CurrentUser!.Id).Result;
        Achievements = _achievementRepository.GetByUserId(App.CurrentUser!.Id).Result;

        var user = App.CurrentUser!;
        _userName = user.Name;

        PlayerStatisticRecord = _playerStatisticRepository.GetByUserId(user.Id).Result!;

        int gamesCount = PlayerStatisticRecord.GamesPlayed;
        int wins = PlayerStatisticRecord.GamesWon;
        int draws = PlayerStatisticRecord.GamesDraw;
        int loses = gamesCount - (wins + draws);

        GameLose = loses;
        TimePlayed = Math.Round( PlayerStatisticRecord.TotalTimePlayed, 2);
        AchievementsCount = Achievements.Count;

        if (PlayerStatisticRecord.GamesPlayed != 0)
            Winrate = Math.Round((double)((double)wins / (double)gamesCount * 100), 2);
    }

    private void ChangeName()
    {
        var result = _userRepository.ChangeName(App.CurrentUser!.Name, UserName).Result;
        if(result == true)
        {
            MessageBox.Show("Ви успішно змінили ім'я", "info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else
        {
            MessageBox.Show("Ім'я вже є в системі", "info", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

    }
}

