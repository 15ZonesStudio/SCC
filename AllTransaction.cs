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
    public partial class AllTransaction : UIViewController
    {
        public AllTransaction (IntPtr handle) : base (handle)
        {
        }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
            try
            {
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
                string[] data = new string[Int32.Parse(r["_id"].ToString()) + 1];
                string[] SubData = new string[Int32.Parse(r["_id"].ToString()) + 1];
                string[] Ids = new string[Int32.Parse(r["_id"].ToString()) + 1];

                int a;
                float total = 0;
                for (a = 1; a < Int32.Parse(r["_id"].ToString()); a = a + 1)
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
                    total += float.Parse(s["amount"].ToString());
                }
                Tot.Text = "Total of all transactions: $" + total.ToString();
                a = Int32.Parse(r["_id"].ToString());
                SqliteDataReader ss = _connection.lookupid(m_dbConnection, a);
                float amounts = float.Parse(ss["amount"].ToString());
                int days = Int32.Parse(ss["day"].ToString());
                int months = Int32.Parse(ss["month"].ToString());
                int years = Int32.Parse(ss["year"].ToString());
                string stores = ss["store"].ToString();
                string labels = stores + " | $" + amounts.ToString();
                string subLabels = months.ToString() + "/" + days.ToString() + "/" + years.ToString();
                SubData[a] = subLabels;
                data[a] = labels;
                Ids[a] = a.ToString();
                tableView.Frame = new CoreGraphics.CGRect(5, 30, View.Bounds.Width, View.Bounds.Height - 30);
                tableView.Source = new TableSource(data, SubData, tableView, Ids);
                m_dbConnection.Close();
            }
            catch
            {
                tableView.Frame = new CoreGraphics.CGRect(5, 30, View.Bounds.Width, View.Bounds.Height - 30);
            }
		}
    }
}