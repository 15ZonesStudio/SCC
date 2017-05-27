﻿using System;
using UIKit;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace SCCiPhone
{
	public class ShoppingTavl : UITableViewSource
	{
		protected string[] TableItems;
		protected string cellId = "TableCell";
		protected string[] TableSubtitles;
		protected UITableView tbv;
		Dictionary<int, float> amounts = new Dictionary<int, float>();
		Dictionary<int, string> names = new Dictionary<int, string>();
        public int Index;
        int length;
        public event EventHandler DeletedTrans;
        int ID;
		public ShoppingTavl(string[] items, string[] subtitles, UITableView maintbl, Dictionary<int, float> _amounts, Dictionary<int, string> _names, int _ID)
		{
			tbv = maintbl;
			TableItems = items;
			TableSubtitles = subtitles;
            amounts = _amounts;
            names = _names;
            ID = _ID;
            length = _amounts.Keys.Max();
		}
		void Refresh()
		{
            tbv.ReloadData();
			string[] data = new string[ID + 1];
			string[] amount = new string[ID + 1];
			for (int i = ID - 1; i > 0; i--)
			{

				string amounted = amounts[i].ToString();
				string thing = names[i];
				data[i] = thing;
				amount[i] = "$" + amounted;
			}


			tbv.Source = new ShoppingTavl(data, amount, tbv, amounts, names, ID);

			tbv.ReloadData();
		}
		public override nint RowsInSection(UITableView tableview, nint section)
		{
            return length;
		}
		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
            UITableViewCell cell = tableView.DequeueReusableCell(cellId);
			cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellId);
			cell.TextLabel.Text = TableItems[indexPath.Row];
			cell.DetailTextLabel.Text = TableSubtitles[indexPath.Row];
			return cell;
		}
		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			tableView.DeselectRow(indexPath, true);

		}
        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
        { 
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    Refresh();
					DeletedTrans(this, EventArgs.Empty);
					Index = indexPath.Row;
                    tableView.DequeueReusableCell("TableCell");
                    length -= 1;
                    tableView.DeleteRows(new Foundation.NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                    Refresh();
                    break;
				case UITableViewCellEditingStyle.None:
					Console.WriteLine("SCCSTATUS: CommitEditingStyle: None called");
					break;
            }
        }

	}
}
	

