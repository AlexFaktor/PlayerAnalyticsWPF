using Database.Records;
using Database.Repositories;
using DesktopApp.Tools;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DesktopApp.ViewModels;

internal class UserFeedbackViewModel : ViewModel
{
    private int? _selectedRating;
    private string _comment = string.Empty;

    public UserFeedbackViewModel()
    {
        Ratings = [1, 2, 3, 4, 5];

        var possibleRecord = App.ServiceProvider.GetRequiredService<FeedbackRepository>().GetByUserId(App.CurrentUser!.Id).Result;
        if (possibleRecord != null)
        {
            SelectedRating = possibleRecord.Rating;
            Comment = possibleRecord.Comment;
        }

        SubmitFeedbackCommand = new RelayCommand(SubmitFeedback);
    }

    public ObservableCollection<int> Ratings { get; }

    public int? SelectedRating
    {
        get => _selectedRating;
        set => Set(ref _selectedRating, value);
    }

    public string Comment
    {
        get => _comment;
        set => Set(ref _comment, value);
    }

    public ICommand SubmitFeedbackCommand { get; }

    private void SubmitFeedback()
    {
        if (SelectedRating == null || string.IsNullOrWhiteSpace(Comment))
        {
            System.Windows.MessageBox.Show("Будь ласка, заповніть всі поля перед надсиланням.");
            return;
        }

        var feedback = new FeedbackRecord
        {
            Id = Guid.NewGuid(),
            UserId = App.CurrentUser!.Id, 
            FeedbackDate = DateTimeOffset.Now,
            Rating = SelectedRating.Value,
            Comment = Comment
        };

        App.ServiceProvider.GetRequiredService<FeedbackRepository>().Save(feedback);

        SelectedRating = null;
        Comment = string.Empty;

        System.Windows.MessageBox.Show("Ваш відгук успішно надіслано. Дякуємо!", "Дякуєм!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
    }
}


