using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        Book modelBook = new Book();
        Statistic modelStatistic = new Statistic();
        public AddBookWindow()
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
            if ((msg == 0xa4))
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Validate();
                modelBook.Name = NameTextBox.Text;
                modelBook.Author = AuthorTextBox.Text;
                modelBook.Pages = Convert.ToInt32(AllTextBox.Text);
                modelStatistic.ReadedPages = Convert.ToInt32(ReadTextBox.Text);
                modelStatistic.Review = ReviewTextBox.Text;

                using (ReadBooksContext db = new ReadBooksContext())
                {
                    db.Books.Add(modelBook);
                    db.SaveChanges();
                }
                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(NameTextBox.Text) || string.IsNullOrEmpty(AuthorTextBox.Text) || string.IsNullOrEmpty(ReadTextBox.Text) ||
                string.IsNullOrEmpty(AllTextBox.Text))
            {
                throw new FormatException("Error! Required field is not filled!");
            }

            if (!Regex.IsMatch(AuthorTextBox.Text, @"^[A-Za-z-А-Яа-яёЁЇїІіЄєҐґ _]*[A-Za-z-А-Яа-яёЁЇїІіЄєҐґ][A-Za-z-А-Яа-яёЁЇїІіЄєҐґ _]*$") ||
                !Regex.IsMatch(NameTextBox.Text, @"^[A-Za-z-А-Яа-яёЁЇїІіЄєҐґ0-9 _]*[A-Za-z-А-Яа-яёЁЇїІіЄєҐґ0-9][A-Za-z-А-Яа-яёЁЇїІіЄєҐґ0-9 _]*$"))
            {
                throw new FormatException("Error! Only letters can be entered in the field!");
            }

            if (!ReadTextBox.Text.All(c => c >= '0' && c <= '9') || !AllTextBox.Text.All(c => c >= '0' && c <= '9'))
            {
                throw new FormatException("Error! Only numbers can be entered in the field!");
            }

            if (Convert.ToInt32(ReadTextBox.Text) > Convert.ToInt32(AllTextBox.Text))
            {
                throw new FormatException(
                    "Error! The number of pages read is less than the total number of pages!");
            }
        }
    }
}
