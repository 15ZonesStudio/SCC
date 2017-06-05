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
using Mono.Data.Sqlite;
//using ZXing;
namespace SCCiPhone
{
    public partial class AllTransaction : UIViewController
    {
        public AllTransaction (IntPtr handle) : base (handle)
        {
        }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
            View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("BackgroundGradiant.png"));

                ConnectionHandles _connection = new ConnectionHandles();
                var filename = "";
                var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                filename = Path.Combine(documents, "sccMain.sqlite");
                var m_dbConnection = new SqliteConnection("Data Source= " + filename + ";");
                m_dbConnection.Open();
                //	_connection.NewTransaction(m_dbConnection, 12, 23, 34, "fs", 23);

                var flip = m_dbConnection.CreateCommand();
                flip.CommandText = "SELECT * FROM m_scc ORDER BY _id DESC LIMIT 1";
                var r = flip.ExecuteReader();
                r.Read();
                string[] data = new string[Int32.Parse(r["_id"].ToString())];
                string[] SubData = new string[Int32.Parse(r["_id"].ToString())];
                string[] Ids = new string[Int32.Parse(r["_id"].ToString())];
                float total = 0;
            Console.WriteLine("total: "+ Int32.Parse(r["_id"].ToString()));
            int a = Int32.Parse(r["_id"].ToString());
            Console.WriteLine(1+a);
            for (a = Int32.Parse(r["_id"].ToString()); a < 0; a--)
            {
				SqliteDataReader s = _connection.lookupid(m_dbConnection, a);
				float amount = float.Parse(s["amount"].ToString());
				int day = Int32.Parse(s["day"].ToString());
				int month = Int32.Parse(s["month"].ToString());
				int year = Int32.Parse(s["year"].ToString());
				string store = s["store"].ToString();
				string label = store + " | $" + amount.ToString();
				string subLabel = month.ToString() + "/" + day.ToString() + "/" + year.ToString();
				SubData[a] = subLabel;
				data[a] = label;
				Ids[a] = a.ToString();
				Console.WriteLine(SubData[a]);
				total += float.Parse(s["amount"].ToString());
            }

                Tot.Text = "Total of all transactions: $" + total.ToString();
                tableView.Frame = new CoreGraphics.CGRect(5, 30, View.Bounds.Width, View.Bounds.Height - 30);
                tableView.BackgroundColor = UIColor.Clear;
                tableView.Source = new TableSource(data, SubData, tableView, Ids);
                m_dbConnection.Close();
            

                tableView.Frame = new CoreGraphics.CGRect(5, 30, View.Bounds.Width, View.Bounds.Height - 30);
                tableView.BackgroundColor = UIColor.Clear;

		}
    }
}