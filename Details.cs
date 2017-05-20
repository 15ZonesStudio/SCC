using Foundation;
using System;
using UIKit;
using Mono.Data.Sqlite;
namespace SCCiPhone
{
    public partial class Details : UIViewController
    {
		public string id;
        public Details (IntPtr handle) : base (handle)
        {
        }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			ConnectionHandles _connection = new ConnectionHandles();
			SqliteConnection m_dbConnection = _connection.CreateConnection();
			m_dbConnection.Open();
			Console.WriteLine("S"+id);
			var r = _connection.lookupid(m_dbConnection, Int32.Parse(id));
			Console.WriteLine(r["store"].ToString());
			store.Text = r["store"].ToString();
			day.Text = r["day"].ToString();
			amount.Text = float.Parse(r["amount"].ToString()).ToString();
			month.Text = r["month"].ToString();
			year.Text = r["year"].ToString();
		}
    }
}
