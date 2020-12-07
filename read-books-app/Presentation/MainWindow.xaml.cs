// <copyright file="MainWindow.xaml.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Interop;
    using BLL.DataTransferObjects;
    using BLL.Interfaces;
    using BLL.Services;
    using LiveCharts;
    using LiveCharts.Wpf;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private IStatisticService service = new StatisticService();
        public SeriesCollection SCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.Loaded += this.MainWindow_Loaded;
            List<BookDTO> book_list = this.service.GetBooksToRead(1).ToList();
            List<BookDTO> read_book_list = this.service.GetReadedBooks(1).ToList();
            ChartValues<int> readedPages = new ChartValues<int> { 4, 6, 5, 2, 4, 8, 10, 1, 2 };
            SCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Readed Books",
                    Values = readedPages,
                    PointGeometry = null
                }
            };
            string[] days = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"};
            Labels = days;
            YFormatter = value => value.ToString();
            foreach (var p in book_list)
            {
                this.PlannedListBox.Items.Add(p.Name.ToString());
            }

            foreach (var p in read_book_list)
            {
                this.ReadListBox.Items.Add(p.Name.ToString());
            }
            this.DataContext = this;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr windowHandle = new WindowInteropHelper(this).Handle;
            HwndSource hwndSource = HwndSource.FromHwnd(windowHandle);
            hwndSource.AddHook(new HwndSourceHook(this.WndProc));
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == 0xa4)
            {
                this.ShowContextMenu();
                handled = true;
            }

            return IntPtr.Zero;
        }

        private void ShowContextMenu()
        {
            var contextMenu = this.Resources["contextMenu"] as ContextMenu;
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

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            List<BookDTO> book_list = this.service.GetBooksToRead(1).ToList();
            // List<StatisticDTO> read_book_statistic = this.service.GetStatisticAll(1).ToList();

            for (int i = 0; i < this.PlannedListBox.Items.Count; i++)
            {

                if (FindTextBox.Text == this.PlannedListBox.Items[i].ToString() || FindTextBox.Text == this.ReadListBox.Items[i].ToString())
                {
                    AddBookWindow addBookWindow = new AddBookWindow();
                    addBookWindow.Show();
                    this.Close();
                    foreach (var p in book_list)
                    {
                        List<StatisticDTO> read_book_statistic = this.service.GetStatisticAll(1, p.Id).ToList();
                        if (FindTextBox.Text == p.Name.ToString())
                        {
                            addBookWindow.NameTextBox.Text = p.Name.ToString();
                            addBookWindow.AuthorTextBox.Text = p.Author.ToString();
                            addBookWindow.AllTextBox.Text = p.Pages.ToString();
                            foreach (var k in read_book_statistic)
                            {
                                addBookWindow.ReadTextBox.Text = k.ReadedPages.ToString();
                                addBookWindow.ReviewTextBox.Text = k.Review.ToString();
                            }
                        }
                    }

                }
                else if (FindTextBox.Text != this.PlannedListBox.Items[i].ToString() || FindTextBox.Text != this.ReadListBox.Items[i].ToString())
                {
                    MessageBox.Show("Такої книги не має в базі, спробуйте іншу назву");
                }
            }
        }
    }
}
