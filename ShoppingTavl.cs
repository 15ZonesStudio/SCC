using System;
using UIKit;
namespace SCCiPhone
{
	public class ShoppingTavl : UITableViewSource
	{
		protected string[] TableItems;
		protected string cellId = "TableCell";
		protected string[] TableSubtitles;
		protected UITableView tbv;
		public ShoppingTavl(string[] items, string[] subtitles, UITableView maintbl)
		{
			tbv = maintbl;
			TableItems = items;
			TableSubtitles = subtitles;
		}
		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return TableItems.Length;
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

	}
}
	

