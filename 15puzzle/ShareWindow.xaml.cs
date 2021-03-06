﻿using System.Windows;
using System.Windows.Controls;
using System.Net;
using System.IO;
using System.Diagnostics;
using System;

namespace _15puzzle
{
    /// <summary>
    /// Interaction logic for WebBrowserWindow.xaml
    /// </summary>
    public partial class ShareWindow : Window
    {
        public ShareWindow(Uri url, int moves)
        {
            InitializeComponent();
            DataContext = new ShareWindowViewModel(url, moves);
        }
    }
}
