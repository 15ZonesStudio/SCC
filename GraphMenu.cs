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
    public partial class GraphMenu : UIViewController
    {
        public GraphMenu (IntPtr handle) : base (handle)
        {

        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            MonthlyGraphContainer.Frame = new CGRect(View.Frame.X, View.Frame.Y + 50, View.Frame.Width, View.Frame.Height / 2);
            StorelyGraphContainer.Frame = new CGRect(View.Frame.X, View.Frame.Y + View.Frame.Height / 2, View.Frame.Width, View.Frame.Height / 2-50);
        }
		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{
            Console.WriteLine("mos"+segue.Identifier);
			switch (segue.Identifier)
			{
				case "MonthlyUsageChart":
                    Console.WriteLine("bea");
					var topSelectorViewController = (MonthlyUsageChart)segue.DestinationViewController;
                    topSelectorViewController._frame = Int32.Parse(View.Frame.Height.ToString());
					break;
				case "StorewiseUsageChart":
					var bottomViewController = (StorewiseUsageChart)segue.DestinationViewController;
                    bottomViewController._frame = Int32.Parse(View.Frame.Height.ToString());
					break;
				default:
					//Another segue
					break;
			}
		}
    }
}