using Database.Records;
using Database.Repositories;
using DesktopApp.Tools;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    internal class UserReportViewModel : ViewModel
    {
        private MainViewModel _mainViewModel = App.ServiceProvider.GetRequiredService<MainViewModel>();

        private string _details;
        private string _selectedReportType;
        private string _customReportType;
        private bool _isCustomTypeVisible;

        public UserReportViewModel()
        {
            ReportTypes = new ObservableCollection<string>
            {
                "Візуальна помилка",
                "Функціональна помилка",
                "Системна помилка",
                "Інше"
            };

            SubmitReportCommand = new RelayCommand(SubmitReport);
        }

        public ObservableCollection<string> ReportTypes { get; }

        public string SelectedReportType
        {
            get => _selectedReportType;
            set
            {
                if (Set(ref _selectedReportType, value))
                {
                    IsCustomTypeVisible = _selectedReportType == "Інше";
                    if (!IsCustomTypeVisible)
                    {
                        CustomReportType = string.Empty;
                    }
                }
            }
        }

        public string CustomReportType
        {
            get => _customReportType;
            set => Set(ref _customReportType, value);
        }

        public bool IsCustomTypeVisible
        {
            get => _isCustomTypeVisible;
            set => Set(ref _isCustomTypeVisible, value);
        }

        public string Details
        {
            get => _details;
            set => Set(ref _details, value);
        }

        public ICommand SubmitReportCommand { get; }

        private async void SubmitReport()
        {
            var reportType = SelectedReportType == "Інше" && !string.IsNullOrWhiteSpace(CustomReportType)
                ? CustomReportType
                : SelectedReportType;

            if (string.IsNullOrWhiteSpace(reportType) || (SelectedReportType == "Інше" && string.IsNullOrWhiteSpace(CustomReportType)))
            {
                MessageBox.Show("Введіть тип помилки.","Увага!", MessageBoxButton.OK,icon:MessageBoxImage.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(Details))
            {
                MessageBox.Show("Опишіть помилку.", "Увага!", MessageBoxButton.OK, icon: MessageBoxImage.Warning);
                return;
            }
            

            var newReport = new ReportRecord
            {
                Id = Guid.NewGuid(),
                GeneratedBy = App.CurrentUser!.Id,
                ReportDate = DateTimeOffset.Now,
                ReportType = reportType,
                Details = Details,
            };

            var reportRes = App.ServiceProvider.GetRequiredService<ReportRepository>();
            await reportRes.AddAsync(newReport);

            SelectedReportType = null;
            Details = string.Empty;

            MessageBox.Show($"Помилку надіслано.\nІндефікатор звернення: «{newReport.Id}»", "Дякуєм!", MessageBoxButton.OK, icon: MessageBoxImage.Information);
        }
    }
}
