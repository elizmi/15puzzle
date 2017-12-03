using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace _15puzzle
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private string[] _buttons = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", string.Empty };
        public Array Buttons
        {
            get { return _buttons; }
            set
            {
                if (value is string[])
                {
                    _buttons = (string[])value;
                    OnPropertyChanged("Buttons");
                }
            }
        }
        public Command GameClick { get; set; }
        public Command NewGame { get; set; }
        public Command InstantWin { get; set; }
        public Command Scores { get; set; }
        private int moves = 0;
        private bool _isInGame;
        public bool IsInGame
        {
            get { return _isInGame; }
            set
            {
                _isInGame = value;
                OnPropertyChanged("IsInGame");
            }
        }
        public MainWindowViewModel()
        {            
            GameClick = new Command();
            GameClick.ExecFunc = ChangeNums;
            NewGame = new Command();
            NewGame.ExecFunc = Shuffle;
            InstantWin = new Command();
            InstantWin.ExecFunc = MakeWin;
            Scores = new Command();
            Scores.ExecFunc = OpenScores;
            IsInGame = false;
        }
        private void ChangeNums(object parameter)
        {
            if (parameter == null || !(parameter is string) || (string)parameter == string.Empty) return;
            string[] btns = (string[])Buttons;            
            int ClickedTag = int.Parse((string)parameter) - 1;
            int x = ClickedTag % 4 + 1;
            int y = ClickedTag / 4 + 1;
            int EmptyTag = -1;
            for (int i = 0; i < btns.Length; i++)
                if (btns[i] == string.Empty)
                {
                    EmptyTag = i;
                    break;
                }
            if (EmptyTag == -1) return;
            int emptyx = EmptyTag % 4 + 1;
            int emptyy = EmptyTag / 4 + 1;
            int vertdif = Math.Abs(y - emptyy);
            int hordif = Math.Abs(x - emptyx);
            if (vertdif + hordif == 1)
            {
                string hold = btns[EmptyTag];
                btns[EmptyTag] = btns[ClickedTag];
                btns[ClickedTag] = hold;
                Buttons = btns;
                moves++;
            }
            if (Check())
            {
                IsInGame = false;
                Window w = new WinWindow(moves);
            }
        }
        private void Shuffle(object parameter)
        {
            moves = 0;
            IsInGame = true;
            string[] random = new string[16];
            Random k = new Random();
            int i;
            for (i = 0; i < 15; i++)
                while (true)
                {
                    int id = k.Next(16);
                    if (random[id] == null) { random[id] = (i + 1).ToString(); break; }
                }
            int empty = 0;
            for (i = 0; i < 16; i++)
                if (random[i] == null) { random[i] = string.Empty; empty = i; break; }
            i = 0;
            int j, sum = 0;
            for (i = 0; i < random.Length - 1; i++)
                for (j = i + 1; j < random.Length; j++)
                {
                    if (random[i] == string.Empty || random[j] == string.Empty) continue;
                    if (int.Parse(random[i]) > int.Parse(random[j])) sum++;
                }
            if ((sum + (empty / 4) + 1) % 2 != 0) Shuffle(null);
            else Buttons = random;
        }
        private void MakeWin(object parameter)
        {
            string[] correct = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", string.Empty };
            Buttons = correct;
        }
        private bool Check()
        {
            string[] correct = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", string.Empty };
            if (Enumerable.SequenceEqual(correct, (string[])Buttons)) return true;
            else return false;
        }
        private void OpenScores(object parameter)
        {
            Window w = new ScoresWindow();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
