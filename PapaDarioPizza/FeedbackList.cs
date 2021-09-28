using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaDarioPizza
{
    class FeedbackList : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Feedback { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<FeedbackList> GetFeedbacks(string connectionString)
        {
            const string GetFeedbacksQuery = "select id, Email, FeedbackContent from Feedbacks";

            var feedbacks = new ObservableCollection<FeedbackList>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetFeedbacksQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var feedbackList = new FeedbackList();
                                    feedbackList.Id = reader.GetInt32(0);
                                    feedbackList.Email = reader.GetString(1);
                                    feedbackList.Feedback = reader.GetString(2);
                                    feedbacks.Add(feedbackList);
                                }
                            }
                        }
                    }
                }
                return feedbacks;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }
    }
}
