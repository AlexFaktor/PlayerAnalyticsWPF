using Database.Records;
using Database.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace DesktopApp.ViewModels;

internal class AdminFeedbacksViewModel : ViewModel
{
    private readonly FeedbackRepository _feedbackRepository;

    private ObservableCollection<FeedbackRecord> _feedbacks = [];

    public ObservableCollection<FeedbackRecord> Feedbacks
    {
        get => _feedbacks;
        set => Set(ref _feedbacks, value);
    }

    public AdminFeedbacksViewModel()
    {
        _feedbackRepository = App.ServiceProvider.GetRequiredService<FeedbackRepository>();

        Feedbacks = new ObservableCollection<FeedbackRecord>(_feedbackRepository.GetAllAsync().Result);
    }
}


