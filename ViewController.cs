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
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
		private void CreateEmptyDatabase()
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
			m_dbConnection.Close();
		}
		private void insertvar(SqliteConnection connection, int month, int day, int year, string shop, float amount)
		{

			var insert = connection.CreateCommand();
			string command = "INSERT INTO [m_scc] ([store], [amount], [month], [day], [year]) VALUES ('" + shop + "', " + amount + ", " + month + ", " + day + ", " + year + ");";
			insert.CommandText = command;
			insert.ExecuteNonQuery();

		}
		private SqliteDataReader lookupid(SqliteConnection connection, int id)
		{
			string command = "SELECT * FROM m_scc WHERE _id=" + id + ";";
			var lookup = connection.CreateCommand();
			lookup.CommandText = command;
			var r = lookup.ExecuteReader();
			r.Read();
			return r;
		}
		private SqliteDataReader lookupmonth(SqliteConnection connection, int month)
		{
			string command = "SELECT * FROM m_scc WHERE month=" + month + ";";
			var lookup = connection.CreateCommand();
			lookup.CommandText = command;
			var r = lookup.ExecuteReader();
			r.Read();
			return r;
		}
		private SqliteDataReader lookupstore(SqliteConnection connection, string store)
		{
			string command = "SELECT * FROM m_scc WHERE store=" + store + ";";
			var lookup = connection.CreateCommand();
			lookup.CommandText = command;
			var r = lookup.ExecuteReader();
			r.Read();
			return r;
		}
		void HandleValueChanged(object sender, EventArgs e)
		{
			Refresh();
		}
		private void Refresh()
		{
		try
			{
				var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				var filename = Path.Combine(documents, "sccMain.sqlite");
				//var filename = Path.Combine("/Users/liujack/Desktop/Dididi/sccMain.sqlite");
				var m_dbConnection = new SqliteConnection("Data Source= " + filename + ";");
				m_dbConnection.Open();
				var flip = m_dbConnection.CreateCommand();
				flip.CommandText = "SELECT * FROM m_scc ORDER BY _id DESC LIMIT 1";
				var r = flip.ExecuteReader();
				r.Read();
				string[] data = new string[Int32.Parse(r["_id"].ToString()) + 1];
				string[] SubData = new string[Int32.Parse(r["_id"].ToString()) + 1];
				int[] ides = new int[Int32.Parse(r["_id"].ToString()) + 1];
				tableView.Frame = new CoreGraphics.CGRect(0, 30, View.Bounds.Width, View.Bounds.Height - 30);
				string[] di = { "" };
				string[] du = { "" };
				int[] da = { };
				int length = Int32.Parse(r["_id"].ToString()) + 1;
			//	tableView.Source = new TableSource(di,du, tableView);
				int a;
				for (a = 1; a < Int32.Parse(r["_id"].ToString()); a = a + 1)
				{
					SqliteDataReader s = lookupid(m_dbConnection, a);
					float amount = float.Parse(s["amount"].ToString());
					int day = Int32.Parse(s["day"].ToString());
					int month = Int32.Parse(s["month"].ToString());
					int year = Int32.Parse(s["year"].ToString());
					int id = Int32.Parse(s["_id"].ToString());
					string store = s["store"].ToString();
					string label = store + " | $" + amount.ToString();
					string subLabel = month.ToString() + "/" + day.ToString() + "/" + year.ToString();
					SubData[a] = subLabel;
					data[a] = label;
					ides[a] = id;
				}
				a = Int32.Parse(r["_id"].ToString());
				SqliteDataReader ss = lookupid(m_dbConnection, a);
				float amounts = float.Parse(ss["amount"].ToString());
				int days = Int32.Parse(ss["day"].ToString());
				int months = Int32.Parse(ss["month"].ToString());
				int years = Int32.Parse(ss["year"].ToString());
				string stores = ss["store"].ToString();
				string labels = stores + " | $" + amounts.ToString();
				string subLabels = months.ToString() + "/" + days.ToString() + "/" + years.ToString();
				SubData[a] = subLabels;
				data[a] = labels;
		
			//	tableView.Source = new TableSource(data, SubData, tableView);
				// Perform any additional setup after loading the view, typically from a nib.
				m_dbConnection.Close();
			}
			catch
			{
				Console.WriteLine("SCCSTATUS: No Transaction Found");
				CreateEmptyDatabase();
				tot.Hidden = true;

			}
			tableView.RefreshControl.EndRefreshing();

		
		}

		public override void ViewDidLoad()
		{
			
			base.ViewDidLoad();
			tableView.RefreshControl = new UIRefreshControl();
			tableView.RefreshControl.ValueChanged += HandleValueChanged;

			try
			{
				
				//var filename = Path.Combine("/Users/liujack/Desktop/Dididi/sccMain.sqlite");
				var filename = "";
				var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				filename = Path.Combine(documents, "sccMain.sqlite");
				var m_dbConnection = new SqliteConnection("Data Source= " + filename + ";");

				tot.Hidden = false;
				m_dbConnection.Open();
				var flip = m_dbConnection.CreateCommand();
				flip.CommandText = "SELECT * FROM m_scc ORDER BY _id DESC LIMIT 1";
				var r = flip.ExecuteReader();
				r.Read();
				string[] data = new string[Int32.Parse(r["_id"].ToString()) + 1];
				string[] SubData = new string[Int32.Parse(r["_id"].ToString()) + 1];
				int a;
				for (a = 1; a < Int32.Parse(r["_id"].ToString()); a = a + 1)
				{
					SqliteDataReader s = lookupid(m_dbConnection, a);
					float amount = float.Parse(s["amount"].ToString());
					int day = Int32.Parse(s["day"].ToString());
					int month = Int32.Parse(s["month"].ToString());
					int year = Int32.Parse(s["year"].ToString());
					string store = s["store"].ToString();
					string label = store + " | $" + amount.ToString();
					string subLabel = month.ToString() + "/" + day.ToString() + "/" + year.ToString();
					SubData[a] = subLabel;
					data[a] = label;
				}
				a = Int32.Parse(r["_id"].ToString());
				SqliteDataReader ss = lookupid(m_dbConnection, a);
				float amounts = float.Parse(ss["amount"].ToString());
				int days = Int32.Parse(ss["day"].ToString());
				int months = Int32.Parse(ss["month"].ToString());
				int years = Int32.Parse(ss["year"].ToString());
				string stores = ss["store"].ToString();
				string labels = stores + " | $" + amounts.ToString();
				string subLabels = months.ToString() + "/" + days.ToString() + "/" + years.ToString();
				SubData[a] = subLabels;
				data[a] = labels;
		//		tableView.Source = new TableSource(data, SubData, tableView);

				// Perform any additional setup after loading the view, typically from a nib.
				m_dbConnection.Close();
			}
			catch
			{
				
				CreateEmptyDatabase();
				tot.Hidden = true;
			}

		}




		partial void UIButton134_TouchUpInside(UIButton sender)
		{
			
		}

		public override void DidReceiveMemoryWarning()
		{
			
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}



		//  Junkyard/Useful Commands
		//	CreateEmptyDatabase();
		//	var filename = Path.Combine("/Users/liujack/Desktop/Dididi/sccMain.sqlite");
		//	File.WriteAllText(filename, "");
		//	var m_dbConnection = new SqliteConnection("Data Source= " + filename + ";");
		//	m_dbConnection.Open();
		//	insertvar(m_dbConnection, 12, 1, 2016, "Costco", 302);
		//	var r = lookupid(m_dbConnection, 1);
		//	but.SetTitle(r["store"].ToString(), UIControlState.Normal);





	}
}
