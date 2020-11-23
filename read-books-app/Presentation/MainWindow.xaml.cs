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
using BLL.DataTransferObjects;
<<<<<<< Updated upstream
using BLL.Services;
using BLL.Interfaces;

=======
using BLL.Interfaces;
using BLL.Services;
>>>>>>> Stashed changes

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IStatisticService service = new StatisticService();
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            List<BookDTO> book_list = service.GetBooksToRead(27).ToList();
            List<BookDTO> read_book_list = service.GetReadedBooks(27).ToList();

            foreach (var p in book_list)
            {
                PlannedListBox.Items.Add(p.Name.ToString());
<<<<<<< Updated upstream
=======
            }
            foreach (var p in read_book_list)
            {
                ReadListBox.Items.Add(p.Name.ToString());
>>>>>>> Stashed changes
            }
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
           AddBookWindow window = new AddBookWindow();
           window.Show();
           this.Close();
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
