using System;
using UIKit;
using SQLitePCL;
using System.IO;
using Foundation;
using Mono.Data.Sqlite;
using MonoTouch;
namespace SCCiPhone

{
    public partial class NewTrans : UIViewController
    {
		private bool sm = false;
		private float amountsm;

		public bool Sm
		{
			get
			{
				return sm;
			}

			set
			{
				sm = value;
			}
		}

		public float Amountsm
		{
			get
			{
				return amountsm;
			}

			set
			{
				amountsm = value;
			}
		}

		public bool Sm1
		{
			get
			{
				return sm;
			}

			set
			{
				sm = value;
			}
		}

		public float Amountsm1
		{
			get
			{
				return amountsm;
			}

			set
			{
				amountsm = value;
			}
		}

		public NewTrans (IntPtr handle) : base (handle)
        {
        }
		private void insertvar(SqliteConnection connection, int month, int day, int year, string shop, float amount)
		{

			var insert = connection.CreateCommand();
			string command = "INSERT INTO [m_scc] ([store], [amount], [month], [day], [year]) VALUES (@shop, @amount, @month, @day, @year);";
			insert.CommandText = command;
			insert.Prepare();
			insert.Parameters.AddWithValue("@shop", shop);
			insert.Parameters.AddWithValue("@day", day);
			insert.Parameters.AddWithValue("@amount", amount);
			insert.Parameters.AddWithValue("@year", year);
			insert.Parameters.AddWithValue("@month", month);
			insert.ExecuteNonQuery();

		}
		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);
			this.View.EndEditing(true);
		}

	//	partial void UIButton3305_TouchUpInside(UIButton sender)
	//	{
	//		throw new NotImplementedException();
	//	}

		partial void UIButton2841_TouchUpInside(UIButton sender)
		{
			day.Text = DateTime.Now.Day.ToString();
			month.Text = DateTime.Now.Month.ToString();
			year.Text = DateTime.Now.Year.ToString();
			da.Text = day.Text;
			m.Text = month.Text;
			yr.Text = year.Text;
		}



		public override void ViewDidLoad()
		{
			
			base.ViewDidLoad();
			if (sm)
			{
				day.Text = DateTime.Now.Day.ToString();
				month.Text = DateTime.Now.Month.ToString();
				year.Text = DateTime.Now.Year.ToString();
				amount.Text = amountsm.ToString();
				da.Text = day.Text;
				m.Text = month.Text;
				yr.Text = year.Text;
				am.Text = amount.Text;
			}
		}


		partial void UIButton830_TouchUpInside(UIButton sender)
		{
int days = 0;
int months = 0;
int years = 0;
float amounts = 0;
			try
			{
				days = Int32.Parse(day.Text);
			}
			catch
			{
				Console.WriteLine("SCCSTATUS: Int Recived (day.Text) is not parsable to int");
				UIAlertView _error = new UIAlertView("SCC", "Please enter a correctly formatted date", null, "Ok", null);
_error.Show();
				return;

			}
			try
			{
				months = Int32.Parse(month.Text);
			}
			catch
			{
				Console.WriteLine("SCCSTATUS: Int Recived (month.Text) is not parsable to int");
				UIAlertView _error = new UIAlertView("SCC", "Please enter a correctly formatted date", null, "Ok", null);
_error.Show();
				return;

			}
			try
			{
				amounts = float.Parse(amount.Text);
			}
			catch
			{
				Console.WriteLine("SCCSTATUS: Int Recived (amount.Text) is not parsable to int");
				UIAlertView _error = new UIAlertView("SCC", "Please enter a correctly formatted date", null, "Ok", null);
_error.Show();
				return;

			}
			try
			{
				years = Int32.Parse(year.Text);
			}
			catch
			{
				Console.WriteLine("SCCSTATUS: Int Recived (year.Text) is not parsable to int");
				UIAlertView _error = new UIAlertView("SCC", "Please enter a correctly formatted date", null, "Ok", null);
_error.Show();
				return;

			}


			string stores = store.Text;
var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
var filename = Path.Combine(documents, "sccMain.sqlite");
//var filename = Path.Combine("/Users/liujack/Desktop/Dididi/sccMain.sqlite");
var m_dbConnection = new SqliteConnection("Data Source= " + filename + ";");
m_dbConnection.Open();
			insertvar(m_dbConnection, months, days, years, stores, amounts);
m_dbConnection.Close();
		}


		partial void dchg(UITextField sender)
		{
			da.Text = day.Text;
		}

		partial void monthc(UITextField sender)
		{
			m.Text = month.Text;
		}

		partial void yearchg(UITextField sender)
		{
			yr.Text = year.Text;
		}

		partial void storechg(UITextField sender)
		{
			st.Text = store.Text;
		}

		partial void amountchg(UITextField sender)
		{
			am.Text = amount.Text;
		}
	}
}