﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="The Watson Laboratory">
//   Copyright © 2019 The Watson Laboratory
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StringComparer
{
    using System.Windows;
    using System.Windows.Controls;
    using StringComparer.ViewModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.MainWindowViewModel = new MainWindowViewModel();
            this.DataContext = this.MainWindowViewModel;
            this.InitializeComponent();
        }

        public MainWindowViewModel MainWindowViewModel { get; }

        private void TextBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= this.TextBox1_GotFocus;
        }

        private void TextBox2_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= this.TextBox2_GotFocus;
        }
    }
}