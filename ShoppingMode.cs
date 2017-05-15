using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
namespace SCCiPhone
{
   
    public partial class ShoppingMode : UIViewController
    {
		Dictionary<int, float> amounts = new Dictionary<int, float>();
		Dictionary<int, string> names = new Dictionary<int, string>();
        int ID = 0;
        public ShoppingMode (IntPtr handle) : base (handle)
        {
        }
        void Refresh()
        {
			string[] data = new string[ID + 1];
            string[] amount = new string[ID + 1];
			for (int i = ID - 1; i > -1; i--)
			{

				string amounted = amounts[i].ToString();
				string thing = names[i];
				data[i] = thing;
                amount[i] = "$" + amounted;
			}


            Recipt.Source = new ShoppingTavl(data, amount, Recipt);
        }
        partial void UIButton6081_TouchUpInside(UIButton sender)
        {
            float amountL = float.Parse(amount.Text);
            string itemL = item.Text;
            float quantity = float.Parse(quant.Text);
            float discountL = float.Parse(discont.Text);
            names.Add(ID,quantity.ToString()+" of "+item);
            if (discountL.ToString() != "" & discountL.ToString() != "0")
            {
                discountL = 0;
            }
            amounts.Add(ID, (amountL*quantity) - discountL * amountL);
            Refresh();
        }
    }
}