using Foundation;
using System;
using UIKit;
using Mono.Data.Sqlite;
namespace SCCiPhone
{
    public partial class SetBudget : UIViewController
    {
        public SetBudget (IntPtr handle) : base (handle)
        {
        }

        partial void UIButton4554_TouchUpInside(UIButton sender)
        {
			ConnectionHandles _connection = new ConnectionHandles();
			SqliteConnection m_dbConnection = _connection.CreateConnection();
			m_dbConnection.Open();
            if (_connection.lookupsettings(m_dbConnection, "budget") == 0)
            {
                _connection.NewSettings(m_dbConnection, "budget", float.Parse(bud.Text));
                m_dbConnection.Close();
            }
            else
            {
                string edit = "UPDATE s_scc SET value=@value WHERE key='budget'";
                var command = m_dbConnection.CreateCommand();
                command.CommandText = edit;
                command.Prepare();
                command.Parameters.AddWithValue("@value",float.Parse(bud.Text));
                command.ExecuteNonQuery();
                Console.WriteLine(command.CommandText);
                m_dbConnection.Close();
            }

        }
    }
}