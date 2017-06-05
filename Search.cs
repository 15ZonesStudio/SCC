using Foundation;
using System;
using UIKit;
using System.IO;
using Mono.Data.Sqlite;


namespace SCCiPhone
{
    public partial class Search : UIViewController
    {
        bool moved = false;
        string segmentID;
        event EventHandler setAmount;
        string amountSet;
        TableSource table;

        public override void ViewDidLoad()
        {
            View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("BackgroundGradiant.png"));
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "sccMain.sqlite");
            var m_dbConnection = new SqliteConnection("Data Source= " + filename + ";");
            m_dbConnection.Open();
            var flip = m_dbConnection.CreateCommand();
            flip.CommandText = "SELECT * FROM m_scc ORDER BY amount DESC";
            var r = flip.ExecuteReader();
            r.Read();
            amountSlider.MaxValue = float.Parse(r["amount"].ToString());
            var selectedSegmentId = SegmentC.SelectedSegment;
            segmentID = selectedSegmentId.ToString();
            m_dbConnection.Close();

        }

        partial void SegmentChanged(UISegmentedControl sender)
        {
            var selectedSegmentId = (sender as UISegmentedControl).SelectedSegment;
            segmentID = selectedSegmentId.ToString();
        }


               
        partial void Moved(UISlider sender)
        {
            
            moved = true;

            Current.Text = amountSlider.Value.ToString("c2");
        }

        public Search (IntPtr handle) : base (handle)
        {
            
        }
		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);
			View.EndEditing(true);
		}
        partial void UIButton6051_TouchUpInside(UIButton sender)
        {
			store.Text = "";
			yr.Text = "";
			month.Text = "";
            Current.Text = "Any";
            amountSlider.SetValue(0, false);
            tot.Text = "";
            tableView.Source = new TableSource(new string[] { "" }, new string[] { "" }, tableView, new string[] { "0" });
            tableView.ReloadData();
        }

        partial void UIButton5738_TouchUpInside(UIButton sender)
        {
            float totastore = 0;
            string stores = store.Text;
            string years = yr.Text;
            string months = month.Text;
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "sccMain.sqlite");
            var m_dbConnection = new SqliteConnection("Data Source= " + filename + ";");
            m_dbConnection.Open();
            var flip = m_dbConnection.CreateCommand();
            flip.CommandText = "SELECT * FROM m_scc ORDER BY amount DESC";
            var r = flip.ExecuteReader();
            r.Read();
            amountSlider.MaxValue = float.Parse(r["amount"].ToString());
            string command = "SELECT * FROM m_scc WHERE ";
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
            if (moved)
            {
                
                if (start)
                {
                    
                    if (segmentID == "0")
                    {
                        Console.WriteLine("superstar");
                        command += "amount <= @amount";
                        start = false;
                    }
                    else if (segmentID == "1")
                    {
                        command += "amount >= @amount";
                        start = false;
                    }

                }
                else
                {
                    if (segmentID == "0")
                    {
                        command += " AND amount <= @amount";
                    
                    }
                    else if (segmentID == "1")
                    {
                        command += " AND amount >= @amount";

                    }
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
                lookup.Parameters.AddWithValue("@store", "%" + stores + "%");
                Console.WriteLine("store!");
            }
            if (years != "")
            {
                lookup.Parameters.AddWithValue("@year", "%" + years + "%");
                Console.WriteLine("year!");
            }
            if (months != "")
            {
                lookup.Parameters.AddWithValue("@month", "%" + months + "%");
                Console.WriteLine("month!");
            }
            if (moved)
            {
                lookup.Parameters.AddWithValue("@amount", amountSlider.Value);
            }

            var reader = lookup.ExecuteReader();
            var lookupw = m_dbConnection.CreateCommand();
            lookupw.CommandText = command;
            lookupw.Prepare();

            if (stores != "")
            {
                lookupw.Parameters.AddWithValue("@store", "%" + stores + "%");
                Console.WriteLine("store!");
            }
            if (years != "")
            {
                lookupw.Parameters.AddWithValue("@year", "%" + years + "%");
                Console.WriteLine("year!");
            }
            if (months != "")
            {
                lookupw.Parameters.AddWithValue("@month", "%" + months + "%");
                Console.WriteLine("month!");
            }
            if (moved)
            {
                Console.WriteLine("s");
                lookupw.Parameters.AddWithValue("@amount", amountSlider.Value);
            }
            Console.WriteLine(lookup.CommandText);
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
            string[] ids = new string[lendata];
            int subsidery = 0;
            float total = 0;
            while (reader.Read())
            {
                Console.WriteLine("read!");
                totastore += float.Parse(reader["amount"].ToString());
                float amount = float.Parse(reader["amount"].ToString());
                int day = Int32.Parse(reader["day"].ToString());
                int month = Int32.Parse(reader["month"].ToString());
                int year = Int32.Parse(reader["year"].ToString());
                ids[subsidery] = reader["_id"].ToString();
                string store = reader["store"].ToString();
                string label = store + " | $" + amount.ToString();
                string subLabel = month.ToString() + "/" + day.ToString() + "/" + year.ToString();
                SubData[subsidery] = subLabel;
                data[subsidery] = label;
                subsidery += 1;
                total += float.Parse(reader["amount"].ToString());
            }
            table = new TableSource(data, SubData, tableView, ids);
            tableView.Source = table;
            tot.Text = "Total of searched transaction: $"+total;
        //    lab1.Text = "You spent a total of $" + totastore.ToString() + " with specified parameters";
            tableView.ReloadData();
            m_dbConnection.Close();
            store.Text = "";
            yr.Text = "";
            month.Text = "";
            moved = false;
            Current.Text = "Any";
            amountSlider.SetValue(0,true);
            table.ItemSelected += (object _sender, EventArgs e) => LaunchDetail(table.SelectedItem);
        }
		public void LaunchDetail(string _id)
		{

		//	Console.WriteLine("s" + table.SelectedItem);
		//	var controller = Storyboard.InstantiateViewController("Details") as Details;
		//	Console.WriteLine("d" + table.SelectedItem);
		//	controller.id = _id;
            //	PresentViewController(controller, true, null);

		}
    }
}