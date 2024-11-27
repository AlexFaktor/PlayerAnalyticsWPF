using Database.Records;
using System.Collections.ObjectModel;

namespace DesktopApp.ViewModels;

internal class AdminFeedbacksViewModel : ViewModel
{
    private ObservableCollection<FeedbackRecord> _feedbacks = [];

    public ObservableCollection<FeedbackRecord> Feedbacks
    {
        get => _feedbacks;
        set => Set(ref _feedbacks, value);
    }
}


