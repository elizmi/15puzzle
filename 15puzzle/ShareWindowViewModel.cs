using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Web;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace _15puzzle
{
    class ShareWindowViewModel : INotifyPropertyChanged
    {
        private string _url;
        public string URL
        {
            get { return _url; }
            set
            {
                _url = value;
                OnPropertyChanged("URL");
            }
        }
        public Command OnPageLoad { get; set; }
        private int moves;
        public ShareWindowViewModel(Uri url, int _moves)
        {
            moves = _moves;
            OnPageLoad = new Command();
            OnPageLoad.ExecFunc = MakePost;
            URL = url.AbsoluteUri;
        }
        private void MakePost(object parameter)
        {
            if (parameter == null || !(parameter is WebBrowser)) return;
            Uri uri = ((WebBrowser)parameter).Source;
            string fragment = uri.Fragment.TrimStart('#');
            string token = HttpUtility.ParseQueryString(fragment).Get("access_token");
            string error = HttpUtility.ParseQueryString(fragment).Get("error");
            string error_description = HttpUtility.ParseQueryString(fragment).Get("error_description");
            if (error != null) { MessageBox.Show(error_description); return; }
            string url = $"https://api.vk.com/method/wall.post?access_token={token}&owner_id=-157712266&message=My score is {moves}";
            try
            {
                string response = new StreamReader(new WebClient().OpenRead(url)).ReadToEnd();
                if (response.ToLower().Contains("error"))
                    MessageBox.Show("Your result was not posted");
                else
                {
                    Process.Start("https://vk.com/public157712266");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }   
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
