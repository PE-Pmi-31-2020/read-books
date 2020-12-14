// <copyright file="AddBookWindow.xaml.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interop;
    using BLL.DataTransferObjects;
    using BLL.Interfaces;
    using BLL.Services;
    using Presentation.Classes;

    /// <summary>
    /// Interaction logic for AddBookWindow.xaml.
    /// </summary>
    public partial class AddBookWindow : Window
    {
        private IStatisticService service = new StatisticService();
        private string currentlySelectedBookName = "";
        

        /// <summary>
        /// Initializes a new instance of the <see cref="AddBookWindow"/> class.
        /// </summary>
        public AddBookWindow()
        {
            this.InitializeComponent();
            this.Loaded += this.MainWindow_Loaded;
            List<BookDTO> book_list = this.service.GetBooksToRead(UserHelper.User.Id).ToList();
            List<BookDTO> read_book_list = this.service.GetReadedBooks(UserHelper.User.Id).ToList();

            foreach (var p in book_list)
            {
                this.PlannedList.Items.Add(p.Name.ToString());
            }

            foreach (var p in read_book_list)
            {
                this.ReadBooksList.Items.Add(p.Name.ToString());
            }
        }

        private void PlannedList_Selected(object sender, RoutedEventArgs e)
        {
            this.currentlySelectedBookName = this.PlannedList.SelectedItem.ToString();
        }

        private void ReadBooksList_Selected(object sender, RoutedEventArgs e)
        {
            this.currentlySelectedBookName = this.ReadBooksList.SelectedItem.ToString();
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
                this.Validate();
                BookDTO book = new BookDTO
                {
                    Name = this.NameTextBox.Text,
                    Author = this.AuthorTextBox.Text,
                    Pages = Convert.ToInt32(this.AllTextBox.Text)
                };
                service.CreateStatistic(book, UserHelper.User, Convert.ToInt32(this.ReadTextBox.Text), this.ReviewTextBox.Text);

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
            if (string.IsNullOrEmpty(this.NameTextBox.Text) || string.IsNullOrEmpty(this.AuthorTextBox.Text) || string.IsNullOrEmpty(this.ReadTextBox.Text) ||
                string.IsNullOrEmpty(this.AllTextBox.Text))
            {
                throw new FormatException("Error! Required field is not filled!");
            }

            if (!Regex.IsMatch(this.AuthorTextBox.Text, @"^[A-Za-z-А-Яа-яёЁЇїІіЄєҐґ _]*[A-Za-z-А-Яа-яёЁЇїІіЄєҐґ][A-Za-z-А-Яа-яёЁЇїІіЄєҐґ _]*$") ||
                !Regex.IsMatch(this.NameTextBox.Text, @"^[A-Za-z-А-Яа-яёЁЇїІіЄєҐґ0-9 _]*[A-Za-z-А-Яа-яёЁЇїІіЄєҐґ0-9][A-Za-z-А-Яа-яёЁЇїІіЄєҐґ0-9 _]*$"))
            {
                throw new FormatException("Error! Only letters can be entered in the field!");
            }

            if (!this.ReadTextBox.Text.All(c => c >= '0' && c <= '9') || !this.AllTextBox.Text.All(c => c >= '0' && c <= '9'))
            {
                throw new FormatException("Error! Only numbers can be entered in the field!");
            }

            if (Convert.ToInt32(this.ReadTextBox.Text) > Convert.ToInt32(this.AllTextBox.Text))
            {
                throw new FormatException(
                    "Error! The number of pages read is less than the total number of pages!");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Validate();
                BookDTO book = new BookDTO
                {
                    Name = this.NameTextBox.Text,
                    Author = this.AuthorTextBox.Text,
                    Pages = Convert.ToInt32(this.AllTextBox.Text)
                };
                service.UpdateStatistic(book, UserHelper.User, Convert.ToInt32(this.ReadTextBox.Text), this.ReviewTextBox.Text, this.currentlySelectedBookName);

                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                service.DeleteStatistic(UserHelper.User, this.currentlySelectedBookName);

                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
