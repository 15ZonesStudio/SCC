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
            //ser
            View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("BackgroundGradiant.png"));
			base.ViewDidLoad();
			ConnectionHandles _connection = new ConnectionHandles();
			SqliteConnection m_dbConnection = _connection.CreateConnection();
			m_dbConnection.Open();
			Console.WriteLine("S"+id);
			var r = _connection.lookupid(m_dbConnection, Int32.Parse(id));
			Console.WriteLine(r["store"].ToString());
			store.Text = r["store"].ToString();
            amount.Text = r["amount"].ToString();
            day.Text = r["month"].ToString() + "/" + r["day"].ToString() + "/" + r["year"].ToString();
		}
    }
}