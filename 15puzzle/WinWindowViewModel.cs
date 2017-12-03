using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using System.Diagnostics;

namespace _15puzzle
{
    class WinWindowViewModel : INotifyPropertyChanged
    {
        private string _result;
        public string Result
        {
            private set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
            get { return _result; }
        }
        private string _name;
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        public Command CloseWindow { get; set; }
        public Command SaveData { get; set; }
        public Command Share { get; set; }
        private bool _canShare;
        public bool CanShare
        {
            get { return _canShare; }
            set
            {
                _canShare = value;
                OnPropertyChanged("CanShare");
            }
        }
        private bool _canSave { get; set; }
        public bool CanSave
        {
            get { return _canSave; }
            set
            {
                _canSave = value;
                OnPropertyChanged("CanSave");
            }
        }
        public int moves;
        public WinWindowViewModel(int _moves)
        {
            moves = _moves;
            CloseWindow = new Command();
            CloseWindow.ExecFunc = CloseWindowFunc;
            Result = $"You have finished in {moves} steps!";
            CanShare = true;
            SaveData = new Command();
            SaveData.ExecFunc = SaveDataFunc;
            CanSave = true;
            Share = new Command();
            Share.ExecFunc = ShareFunc;
        }
        private void CloseWindowFunc(object parameter)
        {            
            if (!(parameter is Window) || parameter == null) return;
            else ((Window)parameter).Close();
        }
        private void SaveDataFunc(object parameter)
        {
            if (!(parameter is TextBox) || parameter == null) return;
            TextBox NameField = (TextBox)parameter;
            if (Validation.GetHasError(NameField))
            {
                MessageBox.Show(Validation.GetErrors(NameField)[0].ErrorContent.ToString());
                return;
            }
            using (SqlConnection db = new SqlConnection((new SQLConnectionString()).ConnectionString))
            {
                SqlCommand query = new SqlCommand($"insert into Scores ([name], score) values (N\'{NameField.Text}\', {moves})", db);
                SqlDataReader reader;
                try
                {
                    db.Open();
                    reader = query.ExecuteReader();
                    db.Close();                    
                }
                catch { MessageBox.Show("Local database is inaccessible, your score would not be saved"); }
            }
            CanSave = false;
        }
        private void ShareFunc(object parameter)
        {
            Uri url = new Uri("https://oauth.vk.com/authorize?response_type=token&scope=wall,groups&client_id=6278853");
            Window w = new ShareWindow(url, moves);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
