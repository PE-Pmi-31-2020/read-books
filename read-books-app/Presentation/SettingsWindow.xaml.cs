// <copyright file="SettingsWindow.xaml.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

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
    /// Interaction logic for SettingsWindow.xaml.
    /// </summary>
    public partial class SettingsWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
        /// </summary>
        public SettingsWindow()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int time;
            int time_m;

            if (Convert.ToInt32(this.MinuteTextBox.Text) > 58 && Convert.ToInt32(this.HourTextBox.Text) > 22)
            {
                time = 0;
                this.HourTextBox.Text = Convert.ToString(time);
                this.MinuteTextBox.Text = Convert.ToString(time);
            }
            else if (Convert.ToInt32(this.MinuteTextBox.Text) > 58)
            {
                time_m = 0;
                time = Convert.ToInt32(this.HourTextBox.Text) + 1;
                this.HourTextBox.Text = Convert.ToString(time);
                this.MinuteTextBox.Text = Convert.ToString(time_m);
            }
            else
            {
                time = Convert.ToInt32(this.MinuteTextBox.Text) + 1;
                this.MinuteTextBox.Text = Convert.ToString(time);
            }
        }

        private void SubClockButton_Click(object sender, RoutedEventArgs e)
        {
            int time;
            if (Convert.ToInt32(this.MinuteTextBox.Text) < 1 && Convert.ToInt32(this.HourTextBox.Text) < 1)
            {
                this.HourTextBox.Text = Convert.ToString(23);
                this.MinuteTextBox.Text = Convert.ToString(59);
            }
            else if (Convert.ToInt32(this.MinuteTextBox.Text) < 2)
            {
                time = Convert.ToInt32(this.HourTextBox.Text) - 1;
                this.HourTextBox.Text = Convert.ToString(time);
                this.MinuteTextBox.Text = Convert.ToString(59);
            }
            else
            {
                time = Convert.ToInt32(this.MinuteTextBox.Text) - 1;
                this.MinuteTextBox.Text = Convert.ToString(time);
            }
        }

        private void MinuteTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }

        private void HourTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }
    }
}
