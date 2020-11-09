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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr windowHandle = new WindowInteropHelper(this).Handle;
            HwndSource hwndSource = HwndSource.FromHwnd(windowHandle);
            hwndSource.AddHook(new HwndSourceHook(WndProc));
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if((msg == 0xa4))
            {
                ShowContextMenu();
                handled = true;
            }
            return IntPtr.Zero;
        }

        private void ShowContextMenu()
        {
            var contextMenu = Resources["contextMenu"] as ContextMenu;
            contextMenu.IsOpen = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddBookButton.Visibility = Visibility.Collapsed;
            AddBookLabel.Visibility = Visibility.Collapsed;
            NameLabel.Visibility = Visibility.Visible;
            NameTextBox.Visibility = Visibility.Visible;
            AuthorLabel.Visibility = Visibility.Visible;
            AuthorTextBox.Visibility = Visibility.Visible;
            ReviewLabel.Visibility = Visibility.Visible;
            ReviewTextBox.Visibility = Visibility.Visible;
            PagesLabel.Visibility = Visibility.Visible;
            ReadLabel.Visibility = Visibility.Visible;
            ReadTextBox.Visibility = Visibility.Visible;
            AllLabel.Visibility = Visibility.Visible;
            AllTextBox.Visibility = Visibility.Visible;
            SaveButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
            AddButton.Visibility = Visibility.Visible;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NameLabel.Visibility = Visibility.Collapsed;
            NameTextBox.Visibility = Visibility.Collapsed;
            AuthorLabel.Visibility = Visibility.Collapsed;
            AuthorTextBox.Visibility = Visibility.Collapsed;
            ReviewLabel.Visibility = Visibility.Collapsed;
            ReviewTextBox.Visibility = Visibility.Collapsed;
            PagesLabel.Visibility = Visibility.Collapsed;
            ReadLabel.Visibility = Visibility.Collapsed;
            ReadTextBox.Visibility = Visibility.Collapsed;
            AllLabel.Visibility = Visibility.Collapsed;
            AllTextBox.Visibility = Visibility.Collapsed;
            SaveButton.Visibility = Visibility.Collapsed;
            DeleteButton.Visibility = Visibility.Collapsed;
            AddButton.Visibility = Visibility.Collapsed;
            AddBookButton.Visibility = Visibility.Visible;
            AddBookLabel.Visibility = Visibility.Visible;
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingWindow = new SettingsWindow();
            settingWindow.Show();
        }

        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            var item = e.OriginalSource as MenuItem;
            MessageBox.Show($"{item.Header} was clicked");
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
       

    }
}
