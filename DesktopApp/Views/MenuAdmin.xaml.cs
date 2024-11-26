using DesktopApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                    vm.SelectedControl = null;
                    break;
            }
        }
        }
}
