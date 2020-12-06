// <copyright file="RegisterWindow.xaml.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

using BLL.DataTransferObjects;
using BLL.Interfaces;
using BLL.Services;
using DAL;

namespace Presentation
{
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
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            UserDTO user = new UserDTO
            {
                Email = emailTextBox.Text,
                Password = passwordTextBox.Password
            };
            service.CreateUser(user);
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
