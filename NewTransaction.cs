using Foundation;
using System;
using UIKit;
//using Tesseract;
//using Tesseract.iOS;
using AVKit;
using AVFoundation;
using System.Threading;
using System.Threading.Tasks;
namespace SCCiPhone
{
    public partial class NewTransaction : UIViewController
    {
		
        public NewTransaction (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("BackgroundGradiant.png"));
        }
		partial void DayChanged(UITextField sender)
		{
			day.Text = DayInput.Text;
		}

		partial void MonthChanged(UITextField sender)
		{
			month.Text = MonthInput.Text;
		}

		partial void YearChanged(UITextField sender)
		{
			year.Text = YearInput.Text;
		}

		partial void StoreChanged(UITextField sender)
		{
			store.Text = StoreInput.Text;
		}

		partial void AmountChanged(UITextField sender)
		{
			amount.Text = AmountInput.Text;
		}

		partial void UIButton2841_TouchUpInside(UIButton sender)
		{
			day.Text = DateTime.Now.Day.ToString();
			month.Text = DateTime.Now.Month.ToString();
			year.Text = DateTime.Now.Year.ToString();
			DayInput.Text = day.Text;
			MonthInput.Text = month.Text;
			YearInput.Text = year.Text;
		}
        
		
		
		partial void Submit_TouchUpInside(UIButton sender)
		{
			ConnectionHandles _Connection = new ConnectionHandles();
			var connection = _Connection.CreateConnection();
			connection.Open();
			_Connection.NewTransaction(connection, Int32.Parse(MonthInput.Text), Int32.Parse(DayInput.Text), Int32.Parse(YearInput.Text), StoreInput.Text, float.Parse(AmountInput.Text));
			connection.Close();
		}
		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);
			View.EndEditing(true);
		}
	}
}