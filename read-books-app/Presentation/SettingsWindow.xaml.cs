// <copyright file="SettingsWindow.xaml.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

using System.Threading;

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
        public static int Hour { get; private set; }
        public static int Minute { get; private set; }
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

        public static void Reminder(object obj)
        {
            bool flag = true;
            while (flag)
            {
                if ((Hour == System.DateTime.Now.Hour) &&
                    (Minute == System.DateTime.Now.Minute))
                {
                    MessageBox.Show($"You need to read now :)");
                    flag = false;
                }

            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Hour = Convert.ToInt32(HourTextBox.Text);
            Minute = Convert.ToInt32(MinuteTextBox.Text);
            int num = 0;
            TimerCallback tm = new TimerCallback(Reminder);
            Timer timer = new Timer(tm, num, 0, 25000);
            this.Close();
        }
    }
}
