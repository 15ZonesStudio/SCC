using System;
using Foundation;
using UIKit;
using SQLitePCL;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Globalization;
using Mono.Data.Sqlite;
using ZXing;

namespace SCCiPhone
{
    public partial class Erase : UIViewController
    {
		private void CreateEmptyDatabase()
		{
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var filename = Path.Combine(documents, "sccMain.sqlite");
			//var filename = Path.Combine("/Users/liujack/Desktop/Dididi/sccMain.sqlite");
			File.WriteAllText(filename, "");
			var m_dbConnection = new SqliteConnection("Data Source= " + filename + ";");
			m_dbConnection.Open();
			string createtable = "CREATE TABLE [m_scc] ([_id] INTEGER PRIMARY KEY AUTOINCREMENT, [store] ntext, [amount] int, [month] int, [day] int, [year] int);";
			var definetable = m_dbConnection.CreateCommand();
			definetable.CommandText = createtable;
			definetable.ExecuteNonQuery();
			m_dbConnection.Close();
		}

		partial void UIButton2128_TouchUpInside(UIButton sender)
		{
			CreateEmptyDatabase();
		}

		public Erase (IntPtr handle) : base (handle)
        {
			
        }






	}
}