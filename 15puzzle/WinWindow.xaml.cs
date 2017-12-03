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
            //REVIEW: Не должно быть показа окна в конструкторе самого окна. Это разные действия, их надо отделять.
            Show();
            InitializeComponent();
            DataContext = new WinWindowViewModel(moves);
        }
    }
}
