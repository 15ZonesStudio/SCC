using Foundation;
using System;
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
			ConnectionHandles _connection = new ConnectionHandles();
			SqliteConnection m_dbConnection = _connection.CreateConnection();
			m_dbConnection.Open();
			var lookup = m_dbConnection.CreateCommand();
			lookup.CommandText = "SELECT * FROM m_scc WHERE month like @month AND year like @year;";
			lookup.Prepare();
			lookup.Parameters.AddWithValue("@month", DateTime.Now.Month.ToString());
			lookup.Parameters.AddWithValue("@year", DateTime.Now.Year.ToString());
			var reader = lookup.ExecuteReader();
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
			string[] LableTitle = new string[50];
			string[] LableDescription = new string[50];
            string[] Ids = new string[50];
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
					LableTitle[times] = label;
					LableDescription[times] = subLabel;
                    Ids[times] = r["_id"].ToString();
					times++;
				}
			}

			catch { }
			m_dbConnection.Close();
			Source = new TableSource(LableTitle, LableDescription, recent,Ids);
			recent.Frame = new CoreGraphics.CGRect(0, 50, View.Bounds.Width, 133);
			recent.Source = Source;
            Console.WriteLine(Source.SelectedItem);
            Source.ItemSelected += (object sender, EventArgs e) => LaunchDetail(Source.SelectedItem);
            Console.WriteLine(Source.SelectedItem);


		}
		public void LaunchDetail(string _id)
		{
			
			Console.WriteLine("s"+Source.SelectedItem);
			var controller = Storyboard.InstantiateViewController("Details") as Details;
			Console.WriteLine("d"+Source.SelectedItem);
            controller.id = _id;
            PresentViewController(controller, true, null);

		}
    }
}