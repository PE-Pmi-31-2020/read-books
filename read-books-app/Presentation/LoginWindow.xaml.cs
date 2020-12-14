// <copyright file="LoginWindow.xaml.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace Presentation
{
    using BLL.DataTransferObjects;
    using BLL.Interfaces;
    using BLL.Services;
    using Presentation.Classes;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for LoginWindow.xaml.
    /// </summary>
    public partial class LoginWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginWindow"/> class.
        /// </summary>
        private IStatisticService service = new StatisticService();
        public LoginWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Display main window.
        /// </summary>
        /// <param name="sender">send.</param>
        /// <param name="e">e.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserDTO user = new UserDTO
            {
                Email = emailTextBox1.Text,
                Password = passwordTextBox1.Password
            };
            UserHelper.User = new UserDTO();
            UserHelper.User.Email = user.Email;
            UserHelper.User.Password = user.Password;
            UserHelper.User.Id = service.GetUserId(UserHelper.User);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Display register window.
        /// </summary>
        /// <param name="sender">send.</param>
        /// <param name="e">e.</param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegisterWindow regWindow = new RegisterWindow();
            regWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Drag window.
        /// </summary>
        /// <param name="sender">send.</param>
        /// <param name="e">e.</param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= this.TextBox_GotFocus;
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = (PasswordBox)sender;
            pb.Password = string.Empty;
            pb.GotFocus -= this.PasswordBox_GotFocus;
        }
    }
}
