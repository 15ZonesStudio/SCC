using System;
using Foundation;
using UIKit;
using SQLitePCL;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Globalization;
using Mono.Data.Sqlite;

namespace SCCiPhone
{
	
    public partial class smgo : UIViewController
    {
		public int maxkey;
		public string[] data;
		public string[] sub = {""};
		public float sum;
		public float nodiscount;
        public smgo (IntPtr handle) : base (handle)
        {
			data = new string[maxkey];
        }
		public override void ViewDidLoad()
		{
			Console.Write(data[0].ToString());
			base.ViewDidLoad();
	//		TableSource table = new TableSource(data, sub, tableview);
		//	table.sm = true;
		//	table.delete = false;
		//	tableview.Source = table;
			tot.Text = "Subtotal: $" + nodiscount.ToString();
			final.Text = "Total (w/ discount): $" + sum.ToString();
		}




		partial void UIButton2837_TouchUpInside(UIButton sender)
		{
		//	var controller = Storyboard.InstantiateViewController("NewTrans") as NewTrans;
//			controller.sm = true;
//			controller.amountsm = sum;
		//	PresentViewController(controller, true, null);
		}
	}

}