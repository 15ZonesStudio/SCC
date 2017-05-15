using System;
using System.IO;
using Mono.Data.Sqlite;
namespace SCCiPhone
{
	public class ConnectionHandles
	{
		public ConnectionHandles()
		{
		}
		public SqliteConnection CreateConnection()
		{
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var filename = Path.Combine(documents, "sccMain.sqlite");
			var m_dbConnection = new SqliteConnection("Data Source= " + filename + ";");
			return m_dbConnection;
		}
		public SqliteConnection CreateEmptyDatabase()
		{
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var filename = Path.Combine(documents, "sccMain.sqlite");
			//var filename = Path.Combine("/Users/liujack/Desktop/Dididi/sccMain.sqlite");
			File.WriteAllText(filename, "");
			var m_dbConnection = new SqliteConnection("Data Source= " + filename + ";");
			m_dbConnection.Open();
			string createtable = "CREATE TABLE [m_scc] ([_id] INTEGER PRIMARY KEY AUTOINCREMENT, [store] ntext, [amount] float, [month] int, [day] int, [year] int);";
			var definetable = m_dbConnection.CreateCommand();
			definetable.CommandText = createtable;
			definetable.ExecuteNonQuery();
            string createbudgert = "CREATE TABLE [s_scc] ([key] ntext, [value] float);";
			var creatsetting = m_dbConnection.CreateCommand();
			creatsetting.CommandText = createbudgert;
			creatsetting.ExecuteNonQuery();
			return m_dbConnection;
		}
        public float lookupsettings(SqliteConnection connection, string key)
        {
            try
            {
                string command = "SELECT * FROM s_scc WHERE key=@key;";
                var lookup = connection.CreateCommand();
                lookup.CommandText = command;
                lookup.Prepare();
                lookup.Parameters.AddWithValue("@key", key);
                var r = lookup.ExecuteReader();
                r.Read();
                return float.Parse(r["value"].ToString());
            }
            catch { return 0; }
        }
		public SqliteDataReader lookupid(SqliteConnection connection, int id)
		{
			string command = "SELECT * FROM m_scc WHERE _id=@id;";
			var lookup = connection.CreateCommand();
			lookup.CommandText = command;
			lookup.Prepare();
			lookup.Parameters.AddWithValue("@id", id);
			var r = lookup.ExecuteReader();
			r.Read();
			return r;
		}
		public void NewTransaction(SqliteConnection connection, int month, int day, int year, string shop, float amount)
		{
			var insert = connection.CreateCommand();
			string command = "INSERT INTO [m_scc] ([store], [amount], [month], [day], [year]) VALUES (@shop, @amount, @month, @day, @year);";
			insert.CommandText = command;
            insert.Prepare();
            insert.Parameters.AddWithValue("@shop", shop);
            insert.Parameters.AddWithValue("@amount", amount);
            insert.Parameters.AddWithValue("@month", month);
            insert.Parameters.AddWithValue("@day", day);
            insert.Parameters.AddWithValue("@year", year);
			insert.ExecuteNonQuery();
		}
		public void NewSettings(SqliteConnection connection, string key, float num)
		{
            try
            {
                var insert = connection.CreateCommand();
                string command = "INSERT INTO [s_scc] ([key], [value]) VALUES (@key,@value);";
                insert.CommandText = command;
                insert.Prepare();
                insert.Parameters.AddWithValue("@key", key);
                insert.Parameters.AddWithValue("@value", num);
                insert.ExecuteNonQuery();
            }
            catch
            {
                
            }
		}

	}
}
