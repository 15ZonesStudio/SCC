using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.Linq;
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


            ShoppingTavl source = new ShoppingTavl(data, amount, Recipt, amounts, names, ID);
            Recipt.Source = source;
            source.DeletedTrans += (object sender, EventArgs e) => removeAtRow(source.Index);
            Recipt.ReloadData();
        }
        void removeAtRow(int row)
        {
			var temp1 = amounts.ToDictionary(entry => entry.Key,
											   entry => entry.Value);
			var temp2 = names.ToDictionary(entry => entry.Key,
											   entry => entry.Value);
            for (int i = row+1; i <= temp1.Keys.Max(); i++)
            {
                
                    amounts.Remove(i-1);
                    amounts.Add(i - 1, temp1[i]);
                
            }
			for (int i = row + 1; i <= temp2.Keys.Max(); i++)
			{
                
				names.Remove(i-1);
				names.Add(i - 1, temp2[i]);
				
			}
			
			Recipt.ReloadData();
		
        }
        partial void UIButton6081_TouchUpInside(UIButton sender)
        {
            float amountL = float.Parse(amount.Text);
            string itemL = item.Text;
            float quantity = float.Parse(quant.Text);
            float discountL = 0;
            names.Add(ID, quantity.ToString() + " of " + item.Text);
            if (discont.Text != "" & discont.Text != "0")
            {
                discountL = float.Parse(discont.Text);
            }
            amounts.Add(ID, (amountL * quantity) - (discountL/100) * amountL);
            ID += 1;
            Refresh();
            
        }
    }
}