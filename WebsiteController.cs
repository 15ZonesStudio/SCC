using Foundation;
using System;
using UIKit;

namespace SCCiPhone
{
    public partial class WebsiteController : UIViewController
    {
        public WebsiteController (IntPtr handle) : base (handle)
        {
        }
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			spinner.StartAnimating();
			webView.LoadRequest(new NSUrlRequest(new NSUrl("http://scciphone.15ZonesStudio.cf/")));
			spinner.StopAnimating();
			spinner.Hidden = true;
		}
    }
}