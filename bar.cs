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
using OxyPlot.Xamarin.iOS;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
namespace SCCiPhone
{
    public partial class bar : UIViewController
    {
        public bar (IntPtr handle) : base (handle)
        {
			
        }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			var plotModel = new PlotModel { Title = "   " };
			var xaxis = new CategoryAxis { Position = AxisPosition.Bottom };
			var yaxis = new LinearAxis { Position = AxisPosition.Left, MajorStep = 50};
			xaxis.ActualLabels.Add("Jan");
			xaxis.ActualLabels.Add("Feb");
			xaxis.ActualLabels.Add("Mar");
			xaxis.ActualLabels.Add("Apr");
			xaxis.ActualLabels.Add("May");
			xaxis.ActualLabels.Add("Jun");
			xaxis.ActualLabels.Add("Jul");
			xaxis.ActualLabels.Add("Aug");
			xaxis.ActualLabels.Add("Sep");
			xaxis.ActualLabels.Add("Oct");
			xaxis.ActualLabels.Add("Nov");
			xaxis.ActualLabels.Add("Dec");
			plotModel.Axes.Add(yaxis);
			plotModel.Axes.Add(xaxis);
			var series1 = new ColumnSeries();
			for (int i = 1; i <= 12; i++)
			{
				float sum = SubUpMonth(i);
				series1.Items.Add(new ColumnItem(sum));
				if (sum / 10 > yaxis.MajorStep)
				{
					yaxis.MajorStep = sum / 10 + 10;
				}


			}


			plotModel.Series.Add(series1);
			var plotView = new PlotView
			{
				Model = plotModel,
				Frame = this.View.Frame
			};

			CoreGraphics.CGRect ScreenBounds = new CoreGraphics.CGRect(0, 0, this.View.Frame.Size.Width, this.View.Frame.Size.Height - 50);
			plotView.Bounds = ScreenBounds;
			this.View.AddSubview(plotView);


		}
		private float SubUpMonth(int month)
		{
			float sum = 0;
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var filename = Path.Combine(documents, "sccMain.sqlite");
			var m_dbConnection = new SqliteConnection("Data Source= " + filename + ";");
			m_dbConnection.Open();
			string command = "SELECT * FROM m_scc WHERE month=" + month + ";";
			var lookup = m_dbConnection.CreateCommand();
			lookup.CommandText = command;
			var r = lookup.ExecuteReader();
			while (r.Read())
			{
				sum += float.Parse(r["amount"].ToString());

			}
			m_dbConnection.Close();
			Console.WriteLine(sum);
			return sum;
		}


	}
}