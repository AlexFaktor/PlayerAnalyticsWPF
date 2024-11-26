using DesktopApp.ViewModels;
using System.Windows.Controls;

namespace DesktopApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MenuAdmin.xaml
    /// </summary>
    public partial class MenuAdmin : UserControl
    {
        public MenuAdmin()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = DataContext as MenuAdminViewModel;
            if (vm == null)
                throw new Exception("AdminMenuVM is missed");

            switch (TabMenu.SelectedIndex)
            {
                case 0:
                    vm.SelectedControl = new AdminUsersViewModel();
                    break;
                case 1:
                    vm.SelectedControl = new AdminSessionsViewModel();
                    break;
                case 2:
                    vm.SelectedControl = new AdminStatisticViewModel();
                    break;
                case 3:
                    vm.SelectedControl = new AdminAchievementsViewModel();
                    break;
                case 4:
                    vm.SelectedControl = new AdminReportsViewModel();
                    break;
                case 5:
                    vm.SelectedControl = new AdminFeedbacksViewModel();
                    break;
                default:
                    vm.SelectedControl = new AdminUsersViewModel();
                    break;
            }
        }
    }
}
