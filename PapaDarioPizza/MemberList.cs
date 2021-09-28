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
    class MemberList : INotifyPropertyChanged
    {
        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<MemberList> GetMembers(string connectionString)
        {
            const string GetMembersQuery = "select Name, Email, Password from Members";

            var members = new ObservableCollection<MemberList>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetMembersQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var memberList = new MemberList();
                                    memberList.Name = reader.GetString(0);
                                    memberList.Email = reader.GetString(1);
                                    memberList.Password = reader.GetString(2);
                                    members.Add(memberList);
                                }
                            }
                        }
                    }
                }
                return members;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }
    }
}
