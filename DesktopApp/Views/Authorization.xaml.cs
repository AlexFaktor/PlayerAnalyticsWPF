using DesktopApp.ViewModels;
using System.Windows.Controls;

namespace DesktopApp.Views;

/// <summary>
/// Логика взаимодействия для Authorization.xaml
/// </summary>
public partial class Authorization : UserControl
{
    public Authorization()
    {
        InitializeComponent();
    }

    private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
    {
        var passwordBox = sender as PasswordBox;
        var viewModel = DataContext as AuthorizationViewModel;
        if (viewModel != null && passwordBox != null)
        {
            viewModel.Password = passwordBox.Password;
        }
    }
}
