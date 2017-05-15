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
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations

		public override UIWindow Window
		{
			get;
			set;
		}
		private void CreateEmptyDatabase()
		{
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var filename = Path.Combine(documents, "sccMain.sqlite");
			//var filename = Path.Combine("/Users/liujack/Desktop/Dididi/sccMain.sqlite");
			File.WriteAllText(filename, "");
			var m_dbConnection = new SqliteConnection("Data Source= " + filename + ";");
			m_dbConnection.Open();
			string createtable = "CREATE TABLE [m_scc] ([_id] INTEGER PRIMARY KEY, [store] ntext, [amount] float, [month] int, [day] int, [year] int);";
			var definetable = m_dbConnection.CreateCommand();
			definetable.CommandText = createtable;
			definetable.ExecuteNonQuery();
            string createbudget = "CREATE TABLE [s_scc] ([key] ntext, [value] float);";
			var creatsetting = m_dbConnection.CreateCommand();
			creatsetting.CommandText = createbudget;
			creatsetting.ExecuteNonQuery();
			m_dbConnection.Close();
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
		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
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
				string[] di = { "" };
				string[] du = { "" };
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
                ConnectionHandles _connection = new ConnectionHandles();
				float budgetgoal = _connection.lookupsettings(m_dbConnection, "budget");
				Console.WriteLine("bi" + budgetgoal.ToString());
				if (budgetgoal == 0)
				{
					Console.WriteLine("SCCSTATUS: Budget Is 0!");
					UIAlertView messagebox = new UIAlertView("Set your budget!", "Set your budget goal at \"Set Budget\"!", null, "Ok", null);
					messagebox.Show();
				}
				else
				{
					
				}
				// Perform any additional setup after loading the view, typically from a nib.
				m_dbConnection.Close();
			}
			catch
			{
				Console.WriteLine("SCCSTATUS: No Transaction Found");
				UIAlertView messagebox = new UIAlertView("Nothing out here....", "Go out, shop, and create a transaction!", null, "Ok", null);
				messagebox.Show();
			}
            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method



            return true;
		}

		public override void OnResignActivation(UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground(UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground(UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated(UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate(UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}
	}
}

