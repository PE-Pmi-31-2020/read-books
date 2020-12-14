// <copyright file="RegisterWindow.xaml.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

using BLL.DataTransferObjects;
using BLL.Interfaces;
using BLL.Services;
using DAL;

namespace Presentation
{
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
    /// Interaction logic for RegisterWindow.xaml.
    /// </summary>
    public partial class RegisterWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterWindow"/> class.
        /// </summary>
         
        private IStatisticService service = new StatisticService();

        public RegisterWindow()
        {
            this.InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserDTO user = new UserDTO
            {
                Email = emailTextBox.Text,
                Password = passwordTextBox.Password
            };
            service.CreateUser(user);
            UserHelper.User = new UserDTO();
            UserHelper.User.Email = user.Email;
            UserHelper.User.Password = user.Password;
            UserHelper.User.Id = service.GetUserId(UserHelper.User);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
