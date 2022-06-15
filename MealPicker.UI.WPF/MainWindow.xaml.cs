﻿using MealPicker.UI.WPF.Resources;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MealPicker.UI.WPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        private void ResizeButton_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState switch {
                WindowState.Normal => WindowState.Maximized,
                WindowState.Maximized => WindowState.Normal,
                _ => WindowState.Normal
            };
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
