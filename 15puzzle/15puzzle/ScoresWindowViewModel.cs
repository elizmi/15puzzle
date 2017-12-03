using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _15puzzle
{
    public class ScoreRow
    {
        public string Name { get; set; }
        public long Score { get; set; }
        public ScoreRow(string _name, long _score)
        {
            Name = _name;
            Score = _score;
        }
    }
    class ScoresWindowViewModel : INotifyPropertyChanged
    {
        private List<ScoreRow> _scores;
        public List<ScoreRow> Scores
        {
            get { return _scores; }
            set
            {
                _scores = value;
                OnPropertyChanged("Scores");
            }
        }
        public ScoresWindowViewModel()
        {
            Scores = new List<ScoreRow>();
            using (SqlConnection db = new SqlConnection(SQLConnectionString.MakeSQLConnectionString()))
            {
                SqlCommand query = new SqlCommand("select name, score from Scores order by score asc", db);
                SqlDataReader reader;
                try
                {
                    db.Open();
                    reader = query.ExecuteReader();
                    SqlDataAdapter adapter = new SqlDataAdapter(query);
                    DataTable table = new DataTable("Scoreboard");
                    db.Close();
                    adapter.Fill(table);
                    foreach (DataRow row in table.Rows)
                    {
                        Scores.Add(new ScoreRow((string)row.ItemArray[0], (long)row.ItemArray[1]));
                    }
                }
                catch
                {
                    MessageBox.Show("Local database is inaccessible");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
