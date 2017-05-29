using Foundation;
using System;
using UIKit;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.iOS;
using CoreGraphics;
namespace SCCiPhone
{
    public partial class MonthlyUsageChart : UIViewController
    {
        public int _frame;
        public MonthlyUsageChart (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Console.WriteLine('d');
            var model = new PlotModel { Title = "Monthly Spending" };
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
			XAxis.ActualLabels.Add("Jan");
			XAxis.ActualLabels.Add("Feb");
            XAxis.ActualLabels.Add("Mar");
            XAxis.ActualLabels.Add("Apr");
            XAxis.ActualLabels.Add("May");
            XAxis.ActualLabels.Add("Jun");
            XAxis.ActualLabels.Add("Jul");
            XAxis.ActualLabels.Add("Aug");
            XAxis.ActualLabels.Add("Sep");
            XAxis.ActualLabels.Add("Oct");
            XAxis.ActualLabels.Add("Nov");
            XAxis.ActualLabels.Add("Dec");

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
            for (int i = 1; i < 13; i++)
            {
                float total = 0;
				string command = "SELECT * FROM m_scc WHERE month=@month;";
				var lookup = m_dbConnection.CreateCommand();
				lookup.CommandText = command;
				lookup.Prepare();
				lookup.Parameters.AddWithValue("@month", i);
				var r = lookup.ExecuteReader();
                while (r.Read())
                {
                    total += float.Parse(r["amount"].ToString());
                }

                if (total == 0)
                {
                    series.Items.Add(new ColumnItem(0));
                }
                else
                {
                    if (total > maxAmount)
                    {
                        maxAmount = total;
                    }
                    series.Items.Add(new ColumnItem(total));
                }

				
            }
            YAxis.Maximum = maxAmount;
            model.Axes.Add(YAxis);
            model.Axes.Add(XAxis);
            model.Series.Add(series);
            Console.WriteLine("Frame:" + _frame + "| Deft:" + this.View.Frame.Height);
            var plotView = new PlotView
            {
                Model = model,
                Frame = new CGRect(this.View.Frame.X, this.View.Frame.Y, this.View.Frame.Width, _frame - _frame/2-50),
			};

			this.View.AddSubview(plotView);
        }
    }
}