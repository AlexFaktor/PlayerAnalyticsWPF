using Database.Records;
using Database.Repositories;
using DesktopApp.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DesktopApp.ViewModels;

internal class AdminReportsViewModel : ViewModel
{
    private readonly ReportRepository _reportRepository;

    private ObservableCollection<ReportRecord> _reports = [];

    public ObservableCollection<ReportRecord> Reports
    {
        get => _reports;
        set => Set(ref _reports, value);
    }

    public AdminReportsViewModel()
    {
        _reportRepository = App.ServiceProvider.GetRequiredService<ReportRepository>();

        Reports = new ObservableCollection<ReportRecord>(_reportRepository.GetAllAsync().Result);
    }

    private RelayCommand createErrorReportCommand;
    public ICommand CreateErrorReportCommand => new RelayCommand(CreateErrorReport);

    private void CreateErrorReport()
    {
        SaveFileDialog saveFileDialog = new()
        {
            InitialDirectory = @"c:\",
            Filter = "Save as docx (*.docx)|*.docx "
        };
        if (saveFileDialog.ShowDialog() == true)
        {
            var selectedFilePath = saveFileDialog.FileName;
            DocumentCreator.CreateListOfRepots(_reports.ToList(), selectedFilePath);
        }
    }
}


