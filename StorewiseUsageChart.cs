using Foundation;
using System;
using UIKit;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.iOS;
using CoreGraphics;
using System.Collections.Generic;
using System.Linq;
namespace SCCiPhone
{
	public partial class StorewiseUsageChart : UIViewController
	{
        public int _frame;
		public StorewiseUsageChart(IntPtr handle) : base(handle)
		{
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			Console.WriteLine('d');
			var model = new PlotModel { Title = "Spending in Most Visited Stores" };
			var XAxis = new CategoryAxis()
			{
				Position = AxisPosition.Bottom,
				MinorTickSize = 0,
				MajorTickSize = 0,
				//MajorGridlineStyle = LineStyle.Solid,
				//MinorGridlineStyle = LineStyle.Solid,
				IsPanEnabled = false,
				IsZoomEnabled = false,
			};
		

			var YAxis = new LinearAxis()
			{
				AxislineStyle = LineStyle.None,
				Position = AxisPosition.Left,
				MinorTickSize = 0,
				MajorTickSize = 0,
				MajorGridlineStyle = LineStyle.Solid,
				MinorGridlineStyle = LineStyle.Solid,
				IsPanEnabled = false,
				IsZoomEnabled = false,

			};
			var series = new ColumnSeries();
			var _connection = new ConnectionHandles();
			var m_dbConnection = _connection.CreateConnection();
			float maxAmount = 0;
			m_dbConnection.Open();
            var flip = m_dbConnection.CreateCommand();
			flip.CommandText = "SELECT * FROM m_scc ORDER BY _id DESC LIMIT 1";
			var s = flip.ExecuteReader();
			s.Read();
            List<string> stores = new List<string>();
            Dictionary<string, float> amounts = new Dictionary<string, float>();
            Dictionary<string, int> times = new Dictionary<string, int>();
            for (int i = Int32.Parse(s["_id"].ToString()); i > 0; i--)
			{
                var r = _connection.lookupid(m_dbConnection, i);
                if (!stores.Contains(r["store"].ToString()))
                {
                    stores.Add(r["store"].ToString());
                    Console.WriteLine(r["amount"].ToString()+r["store"].ToString());
                    amounts.Add(r["store"].ToString(), float.Parse(r["amount"].ToString()));
                    times.Add(r["store"].ToString(), 1);
                }
                else
                {
                    float current = amounts[r["store"].ToString()];
                    amounts[r["store"].ToString()] = float.Parse(r["amount"].ToString())+current;
                    int visits = times[r["store"].ToString()];
                    times[r["store"].ToString()] = visits + 1;
                }

			}
            var top5 = times.OrderByDescending(pair => pair.Value).Take(5);
			foreach (KeyValuePair<string, int> entry in top5)
			{
                series.Items.Add(new ColumnItem(){ Value = amounts[entry.Key], Color = OxyColors.DarkOliveGreen });
                XAxis.ActualLabels.Add(entry.Key);
				
			}
			
			model.Axes.Add(YAxis);
			model.Axes.Add(XAxis);
			model.Series.Add(series);

			var plotView = new PlotView
			{
				Model = model,
				Frame = new CGRect(this.View.Frame.X, this.View.Frame.Y + 50, this.View.Frame.Width, _frame - _frame / 2-100),
			};
            plotView.BackgroundColor = UIColor.Clear;
			this.View.AddSubview(plotView);
            this.View.BackgroundColor = UIColor.Clear;
		}
	}
}