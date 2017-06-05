﻿using Foundation;
using System;
using UIKit;
//using SQLitePCL;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Globalization;
using CoreGraphics;
using Mono.Data.Sqlite;
using OxyPlot;
using OxyPlot.Xamarin.iOS;
using OxyPlot.Axes;
using OxyPlot.Series;
//using ZXing;
namespace SCCiPhone
{
    public partial class HomeView : UIViewController
    {
		TableSource Source = null;
        public HomeView (IntPtr handle) : base (handle)
        {
			
        }
        string _budgetgoal = "";

		public override void ViewDidLoad()
		{
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("BackgroundGradiant.png"));
			ConnectionHandles _connection = new ConnectionHandles();
			SqliteConnection m_dbConnection = _connection.CreateConnection();
			m_dbConnection.Open();
			var lookup = m_dbConnection.CreateCommand();
			lookup.CommandText = "SELECT * FROM m_scc WHERE month like @month AND year like @year;";
			lookup.Prepare();
			lookup.Parameters.AddWithValue("@month", DateTime.Now.Month.ToString());
			lookup.Parameters.AddWithValue("@year", DateTime.Now.Year.ToString());
            SqliteDataReader reader;
            try
            {
                reader = lookup.ExecuteReader();
            }
            catch
            {
                m_dbConnection = _connection.CreateEmptyDatabase();
				lookup = m_dbConnection.CreateCommand();
				lookup.CommandText = "SELECT * FROM m_scc WHERE month like @month AND year like @year;";
				lookup.Prepare();
				lookup.Parameters.AddWithValue("@month", DateTime.Now.Month.ToString());
				lookup.Parameters.AddWithValue("@year", DateTime.Now.Year.ToString());
                reader = lookup.ExecuteReader();
            }
			float tally = 0;
			while (reader.Read())
			{
				Console.WriteLine(reader["amount"].ToString());
				tally += float.Parse(reader["amount"].ToString());
			}
			tot.Text = "$"+tally.ToString();
			float budgetgoal = _connection.lookupsettings(m_dbConnection, "budget");
            if (budgetgoal == 0)
            {
				Console.WriteLine("SCCSTATUS: Budget Is 0!");
            }
            else
            {
                BudgetBar.Progress = tally/budgetgoal;
                if (tally / budgetgoal >= 1)
                {
                    this.View.BackgroundColor = UIColor.Red;
                    recent.BackgroundColor = UIColor.Red;
                }
            }
            amt.Text = "Budget: $" + budgetgoal;
			var id = m_dbConnection.CreateCommand();
			id.CommandText = "SELECT * FROM m_scc ORDER BY _id DESC LIMIT 1";
			var es = id.ExecuteReader();
			es.Read();
            try
            {
                string[] LableTitle = new string[Int32.Parse(es["_id"].ToString())];
                string[] LableDescription = new string[Int32.Parse(es["_id"].ToString())];
                string[] Ids = new string[Int32.Parse(es["_id"].ToString())];


                var flip = m_dbConnection.CreateCommand();
                flip.CommandText = "SELECT * FROM m_scc ORDER BY _id DESC";
                var r = flip.ExecuteReader();
                int times = 1;
                try
                {
                    while (times < 51)
                    {
                        r.Read();
                        float amount = float.Parse(r["amount"].ToString());
                        int day = Int32.Parse(r["day"].ToString());
                        int month = Int32.Parse(r["month"].ToString());
                        int year = Int32.Parse(r["year"].ToString());
                        string store = r["store"].ToString();
                        string label = store + " | $" + amount.ToString();
                        string subLabel = month.ToString() + "/" + day.ToString() + "/" + year.ToString();
                        LableTitle[times - 1] = label;
                        LableDescription[times - 1] = subLabel;
                        Ids[times - 1] = r["_id"].ToString();
                        times++;
                    }
                }

                catch { }
                m_dbConnection.Close();
                Source = new TableSource(LableTitle, LableDescription, recent, Ids);
            
            CGAffineTransform transform = CGAffineTransform.MakeScale(1.0f, 0.25f);
			Line.Transform = transform;
            Lineo.Transform = transform;
			recent.Source = Source;
            Console.WriteLine(Source.SelectedItem);
            Source.ItemSelected += (object sender, EventArgs e) => LaunchDetail(Source.SelectedItem);
			Source.ItemDeleted += (object sender, EventArgs e) => RefreshViewColor();
            Console.WriteLine(Source.SelectedItem);
            Console.WriteLine("hey");
			}
			catch { }

		}
        void RefreshViewColor()
        {
			ConnectionHandles _connection = new ConnectionHandles();
			SqliteConnection m_dbConnection = _connection.CreateConnection();
			m_dbConnection.Open();
			var lookup = m_dbConnection.CreateCommand();
			lookup.CommandText = "SELECT * FROM m_scc WHERE month like @month AND year like @year;";
			lookup.Prepare();
			lookup.Parameters.AddWithValue("@month", DateTime.Now.Month.ToString());
			lookup.Parameters.AddWithValue("@year", DateTime.Now.Year.ToString());
			SqliteDataReader reader;
			try
			{
				reader = lookup.ExecuteReader();
			}
			catch
			{
				m_dbConnection = _connection.CreateEmptyDatabase();
				lookup = m_dbConnection.CreateCommand();
				lookup.CommandText = "SELECT * FROM m_scc WHERE month like @month AND year like @year;";
				lookup.Prepare();
				lookup.Parameters.AddWithValue("@month", DateTime.Now.Month.ToString());
				lookup.Parameters.AddWithValue("@year", DateTime.Now.Year.ToString());
				reader = lookup.ExecuteReader();
			}
			float tally = 0;
			while (reader.Read())
			{
				Console.WriteLine(reader["amount"].ToString());
				tally += float.Parse(reader["amount"].ToString());
			}
            float budgetgoal = _connection.lookupsettings(m_dbConnection, "budget");
			if (budgetgoal == 0)
			{
				Console.WriteLine("SCCSTATUS: Budget Is 0!");
				
			}
			else
			{
				BudgetBar.Progress = tally / budgetgoal;
				if (tally / budgetgoal >= 1)
				{
					this.View.BackgroundColor = UIColor.Red;
					recent.BackgroundColor = UIColor.Red;
				}
                else
                {
					this.View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("BackgroundGradiant.png"));
					recent.BackgroundColor = UIColor.Clear;
                }
			}
        }
		void LaunchDetail(string _id)
		{
			
			Console.WriteLine("s"+Source.SelectedItem);
			var controller = Storyboard.InstantiateViewController("Details") as Details;
			Console.WriteLine("d"+Source.SelectedItem);
            controller.id = _id;
            PresentViewController(controller, true, null);

		}
    }
}