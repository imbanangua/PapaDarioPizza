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
    class DatabaseAccess : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddMember(string connectionString, string name, string password, string email)
        {
            const string AddMember = "INSERT INTO Members" +
                " (Name, Password, Email)" +
                " values(@Name, @Password, @Email)";

            using (SqlConnection conn
                = new SqlConnection(connectionString))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("Name", name);
                        cmd.Parameters.AddWithValue("Password", password);
                        cmd.Parameters.AddWithValue("Email", email);
                        cmd.CommandText = AddMember;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void AddFeedback(string connectionString, string email, string feedback)
        {
            const string AddFeedback = "INSERT INTO Feedbacks" +
                " (Email, FeedbackContent)" +
                " values(@Email, @FeedbackContent)";

            using (SqlConnection conn
                = new SqlConnection(connectionString))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("Email", email);
                        cmd.Parameters.AddWithValue("FeedbackContent", feedback);
                        cmd.CommandText = AddFeedback;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void AddReceipt(string connectionString, string name, string email, int deal, string pizza, 
            int wings, int sandwiches, int poutine, decimal price, string date)
        {
            if(name == null)
            {
                const string AddReceipt = "INSERT INTO Receipts" +
                " (Email, TodayDeal, Pizza, Wings, Sandwiches, Poutine, Price, OrderDate)" +
                " values(@Email, @TodayDeal, @Pizza, @Wings, @Sandwiches, @Poutine, @Price, @OrderDate)";

                using (SqlConnection conn
                    = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("Email", email);
                            cmd.Parameters.AddWithValue("TodayDeal", deal);
                            cmd.Parameters.AddWithValue("Pizza", pizza);
                            cmd.Parameters.AddWithValue("Wings", wings);
                            cmd.Parameters.AddWithValue("Sandwiches", sandwiches);
                            cmd.Parameters.AddWithValue("Poutine", poutine);
                            cmd.Parameters.AddWithValue("Price", price);
                            cmd.Parameters.AddWithValue("OrderDate", date);
                            cmd.CommandText = AddReceipt;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            else
            {
                const string AddReceipt = "INSERT INTO Receipts" +
                " (Name, Email, TodayDeal, Pizza, Wings, Sandwiches, Poutine, Price, OrderDate)" +
                " values(@Name, @Email, @TodayDeal, @Pizza, @Wings, @Sandwiches, @Poutine, @Price, @OrderDate)";

                using (SqlConnection conn
                    = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("Name", name);
                            cmd.Parameters.AddWithValue("Email", email);
                            cmd.Parameters.AddWithValue("TodayDeal", deal);
                            cmd.Parameters.AddWithValue("Pizza", pizza);
                            cmd.Parameters.AddWithValue("Wings", wings);
                            cmd.Parameters.AddWithValue("Sandwiches", sandwiches);
                            cmd.Parameters.AddWithValue("Poutine", poutine);
                            cmd.Parameters.AddWithValue("Price", price);
                            cmd.Parameters.AddWithValue("OrderDate", date);
                            cmd.CommandText = AddReceipt;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }  
        }

        public bool IsExist(string connectionString, string name)
        {
            const string CountMember = "SELECT COUNT(*) FROM Members WHERE Name = @Name";
            bool exist = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("Name", name);
                            cmd.CommandText = CountMember;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                   if(reader.GetInt32(0) > 0)
                                    {
                                        exist = true;
                                    }
                                }
                            }
                        }
                    }
                }
                return exist;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return exist;
        }

        public bool IsPasswordCorrect(string connectionString, string name, string password)
        {
            const string SelectPassword = "SELECT Password FROM Members WHERE Name = @Name";
            bool isCorrect = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("Name", name);
                            cmd.CommandText = SelectPassword;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetString(0) == password)
                                    {
                                        isCorrect = true;
                                    }
                                }
                            }
                        }
                    }
                }
                return isCorrect;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return isCorrect;
        }

        public string SelectEmail(string connectionString, string name)
        {
            const string SelectEmail = "SELECT Email FROM Members WHERE Name = @Name";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("Name", name);
                            cmd.CommandText = SelectEmail;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    return reader.GetString(0);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }

        public void DeleteMember(string connectionString, string name)
        {
            const string DeleteMember = "DELETE FROM Members WHERE Name = @Name";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("Name", name);
                            cmd.CommandText = DeleteMember;
                            SqlDataReader reader = cmd.ExecuteReader();
                        }
                    }
                }
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            
        }

        public void UpdateMember(string connectionString, string name, string password, string email)
        {
            const string UpdateMember = "update Members " +
                "set Password=@Password, Email=@Email " +
                "where Name=@Name";

            using (SqlConnection conn
                = new SqlConnection(connectionString))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("Name", name);
                        cmd.Parameters.AddWithValue("Password", password);
                        cmd.Parameters.AddWithValue("Email", email);
                        cmd.CommandText = UpdateMember;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void DeleteFeedback(string connectionString, int id)
        {
            const string DeleteFeedback = "DELETE FROM Feedbacks WHERE id = @id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("id", id);
                            cmd.CommandText = DeleteFeedback;
                            SqlDataReader reader = cmd.ExecuteReader();
                        }
                    }
                }
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }

        }

        public bool IsAdminExist(string connectionString, string name)
        {
            const string CountAdmin = "SELECT COUNT(*) FROM Admins WHERE Name = @Name";
            bool exist = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("Name", name);
                            cmd.CommandText = CountAdmin;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetInt32(0) > 0)
                                    {
                                        exist = true;
                                    }
                                }
                            }
                        }
                    }
                }
                return exist;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return exist;
        }

        public bool IsAdminPasswordCorrect(string connectionString, string name, string password)
        {
            const string SelectPassword = "SELECT Password FROM Admins WHERE Name = @Name";
            bool isCorrect = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Parameters.AddWithValue("Name", name);
                            cmd.CommandText = SelectPassword;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetString(0) == password)
                                    {
                                        isCorrect = true;
                                    }
                                }
                            }
                        }
                    }
                }
                return isCorrect;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return isCorrect;
        }

    }
}
