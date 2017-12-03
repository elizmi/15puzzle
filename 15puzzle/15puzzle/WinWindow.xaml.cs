using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;

namespace _15puzzle
{
    public partial class WinWindow : Window
    {
        public WinWindow(int moves)
        {
            InitializeComponent();
            DataContext = new WinWindowViewModel(moves);
        }
    }
}
