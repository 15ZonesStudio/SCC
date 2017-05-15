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

namespace SCCiPhone
{
	
    public partial class sum : UIViewController
    {
		
		private SqliteDataReader lookupid(SqliteConnection connection, int id)
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

		public override void ViewDidLoad()
		{
			
			base.ViewDidLoad();
			float total = 0;
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var filename = Path.Combine(documents, "sccMain.sqlite");
			//var filename = Path.Combine("/Users/liujack/Desktop/Dididi/sccMain.sqlite");
			var m_dbConnection = new SqliteConnection("Data Source= " + filename + ";");
			m_dbConnection.Open();
			var flip = m_dbConnection.CreateCommand();
			flip.CommandText = "SELECT * FROM m_scc ORDER BY _id DESC LIMIT 1";
			var r = flip.ExecuteReader();
			r.Read();
   			int a;

			for (a = 1; a < Int32.Parse(r["_id"].ToString()); a = a + 1)
			{
				SqliteDataReader s = lookupid(m_dbConnection, a);
				total += float.Parse(s["amount"].ToString());


			}
			a = Int32.Parse(r["_id"].ToString());
 			SqliteDataReader se = lookupid(m_dbConnection, a);

			total += float.Parse(se["amount"].ToString());
			totals.Text = total.ToString();
		}

	

		public sum (IntPtr handle) : base (handle)
        {
        }

	

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);
			this.View.EndEditing(true);
		}


		partial void UIButton1651_TouchUpInside(UIButton sender)
		{
			float totastore = 0;
			string stores = store.Text;
			string years = yr.Text;
			string months = month.Text;
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var filename = Path.Combine(documents, "sccMain.sqlite");
			//var filename = Path.Combine("/Users/liujack/Desktop/Dididi/sccMain.sqlite");
			var m_dbConnection = new SqliteConnection("Data Source= " + filename + ";");
			m_dbConnection.Open();
			string command = "SELECT * FROM m_scc WHERE ";
			// store LIKE @store AND month LIKE @month AND year LIKE @year;
			bool start = true;
			if (stores != "")
			{
				command += "store like @store";
				start = false;
			}

			if (years != "")
			{
				if (start)
				{
					command += "year like @year";
					start = false;
				}
				else
				{
					command += " AND year like @year";
				}
			}
			if (months != "")
			{
				if (start)
				{
					command += "month like @month";
					start = false;

				}
				else
				{
					command += " AND month like @month";
				}
			}
			else
			{
				command += ";";
			}
			Console.WriteLine(command);
			Console.WriteLine(stores);
			var lookup = m_dbConnection.CreateCommand();
			lookup.CommandText = command;
			lookup.Prepare();

			if (stores != "")
			{
				lookup.Parameters.AddWithValue("@store", "%"+stores+"%");
				Console.WriteLine("store!");
			}
			if (years != "")
			{
				lookup.Parameters.AddWithValue("@year", "%"+years+"%");
				Console.WriteLine("year!");
			}
			if (months != "")
			{
				lookup.Parameters.AddWithValue("@month", "%"+months+"%");
				Console.WriteLine("month!");
			}
			var reader = lookup.ExecuteReader();
			var lookupw = m_dbConnection.CreateCommand();
			lookupw.CommandText = command;
			lookupw.Prepare();

			if (stores != "")
			{
				lookupw.Parameters.AddWithValue("@store", "%"+stores+"%");
				Console.WriteLine("store!");
			}
			if (years != "")
			{
				lookupw.Parameters.AddWithValue("@year", "%"+years+"%");
				Console.WriteLine("year!");
			}
			if (months != "")
			{
				lookupw.Parameters.AddWithValue("@month", "%"+months+"%");
				Console.WriteLine("month!");
			}
			var LengthId = lookupw.ExecuteReader();
			int lendata = 0;
			while (LengthId.Read())
			{
				Console.WriteLine("read!");
				lendata += 1;
			}

			Console.WriteLine(lendata.ToString());
			string[] data = new string[lendata];
			string[] SubData = new string[lendata];
			int subsidery = 0;
			while (reader.Read())
			{
				Console.WriteLine("read!");
				totastore += float.Parse(reader["amount"].ToString());
				float amount = float.Parse(reader["amount"].ToString());
				int day = Int32.Parse(reader["day"].ToString());
				int month = Int32.Parse(reader["month"].ToString());
				int year = Int32.Parse(reader["year"].ToString());
				string store = reader["store"].ToString();
				string label = store + " | $" + amount.ToString();
				string subLabel = month.ToString() + "/" + day.ToString() + "/" + year.ToString();
				Console.WriteLine(store+"s");
				Console.WriteLine(label+"a");
				Console.WriteLine(subLabel+"z");
				SubData[subsidery] = subLabel;
				data[subsidery] = label;
				subsidery += 1;;
			}
		//	TableSource table = new TableSource(data, SubData, tableView);
		//	table.delete = false;
		//	tableView.Source = table;
			lab1.Text = "You spent a total of $" + totastore.ToString() + " with specified parameters";
			tableView.ReloadData();
			m_dbConnection.Close();
		}
	}
}