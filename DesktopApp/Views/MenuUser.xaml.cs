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
    /// Логика взаимодействия для MenuUser.xaml
    /// </summary>
    public partial class MenuUser : UserControl
    {
        public MenuUser()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = DataContext as MenuUserViewModel;
            if (vm == null)
                throw new Exception("MenuUserVM is missed");

            switch (TabMenu.SelectedIndex)
            {
                case 0:
                    vm.SelectedControl = new UserProfileViewModel();
                    break;
                case 1:
                    vm.SelectedControl = new UserAchievementsViewModel();
                    break;
                case 2:
                    vm.SelectedControl = new UserSessionsViewModel();
                    break;
                case 3:
                    vm.SelectedControl = new UserReportViewModel();
                    break;
                case 4:
                    vm.SelectedControl = new UserFeedbackViewModel();
                    break;
                default:
                    vm.SelectedControl = new UserProfileViewModel();
                    break;
            }
        }
    }
}
