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
    public partial class sm : UIViewController
    {
		float amountcount = 0;
		int place = 0;
		int times = 0;
		float nodisc = 0;
		Dictionary<int,float> amounts = new Dictionary<int, float> ();
		Dictionary<int, string> names = new Dictionary<int, string>();
        public sm (IntPtr handle) : base (handle)
        {
			
        }

		partial void UIButton2665_TouchUpInside(UIButton sender)
		{
			int quant = Int32.Parse(qtext.Text);
			float newamount = float.Parse(amountnew.Text);
			int discount = 0;
			float dollardisc = 0;
			try
			{
				discount = Int32.Parse(disc.Text);
			}
			catch
			{
				
			}
			float quantamount = quant * newamount;
			nodisc += quantamount;
			float discounted = 100 - discount;
			discounted -= dollardisc;
			float final = quantamount * discounted / 100;
			amountcount += final;
			amount.Text = amountcount.ToString();
			amounts.Add(place, quantamount);
			names.Add(place, quant.ToString() + " of "+ des.Text);
			place += 1;
			amountnew.Text = "";
			des.Text = "";
			disc.Text = "";
			qtext.Text = "1";


		}

		partial void UIButton2669_TouchUpInside(UIButton sender)
		{
			
			int discount = 0;
			try
			{
				discount = Int32.Parse(dk.Text);
			}
			catch
			{

			}
			int discounted = 100 - discount;
			float final = amountcount * discounted / 100;
			amountcount = final;
			amount.Text = amountcount.ToString();
			dk.Text = "";
			
		}

		partial void UIButton2727_TouchUpInside(UIButton sender)
		{
			int maxkey = place-1;

			string[] data = new string[maxkey+1];
			for (int i = place-1; i > -1; i--)
				{
				
					string amounted = amounts[i].ToString();
					string thing = names[i];
					data[i] = thing + " | $" + amounted;
				}
					

			var controller = Storyboard.InstantiateViewController("smgo") as smgo;
			controller.maxkey = maxkey;
			controller.data = data;
			controller.sum = amountcount;
			controller.nodiscount = nodisc;
			PresentViewController(controller, true, null);
		}
		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);
			this.View.EndEditing(true);
		}
	}
}