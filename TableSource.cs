using System;
using Foundation;
using UIKit;
//using SQLitePCL;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Globalization;
using Mono.Data.Sqlite;
//using ZXing;
namespace SCCiPhone
{
	public class ModelHandles : UIViewController
	{
		
	}
	public class TableSource : UITableViewSource 
	{
        public event EventHandler ItemSelected;
		protected string[] TableItems;
		protected string cellId = "TableCell";
		protected string[] TableSubtitles;
		protected UITableView tbv;
		public bool sm = false;
		public bool delete = true;
        public string[] cellIds;
        int TableItemsLength;
        public TableSource(string[] items, string[] subtitles, UITableView maintbl, string[] _cellIds)
		{
			tbv = maintbl;
			TableItems = items;
			TableSubtitles = subtitles;
            cellIds = _cellIds;
            TableItemsLength = TableItems.Length;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
            return TableItemsLength;
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
            UITableViewCell cell = null;
			if (cell == null)
			{
				if (sm)
				{

					cell = new UITableViewCell(UITableViewCellStyle.Default, cellIds[indexPath.Row]);
                    cell.BackgroundColor = UIColor.Clear;
                    cell.TextLabel.TextColor = UIColor.White;
                    cell.DetailTextLabel.TextColor = new UIColor(red: 0.49f, green: 0.56f, blue: 0.62f, alpha: 1.0f);

				}
				else
				{
					cell = new UITableViewCell(UITableViewCellStyle.Subtitle, cellIds[indexPath.Row]);
                    cell.BackgroundColor = UIColor.Clear;
                    cell.TextLabel.TextColor = UIColor.White;
                    cell.DetailTextLabel.TextColor = new UIColor(red: 0.49f, green: 0.56f, blue: 0.62f, alpha: 1.0f);
				}
			}
			if (sm)
			{
				cell.TextLabel.Text = TableItems[indexPath.Row];
			}
			else
			{
				cell.TextLabel.Text = TableItems[indexPath.Row];
				cell.DetailTextLabel.Text = TableSubtitles[indexPath.Row];
			}


			return cell;
		}
        private string GetCellId(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            var cell = tableView.CellAt(indexPath);
            NSString cellId = cell.ReuseIdentifier;
            return cellId.ToString();
        }
		private void deleteid(SqliteConnection connection, int id)
		{
			string command = "DELETE FROM m_scc WHERE _id=" + id + ";";
			var lookup = connection.CreateCommand();
			lookup.CommandText = command;
			lookup.ExecuteNonQuery();
		}
		private SqliteDataReader lookupid(SqliteConnection connection, int id)
		{
			string command = "SELECT * FROM m_scc WHERE _id=" + id + ";";
			var lookup = connection.CreateCommand();
			lookup.CommandText = command;
			var r = lookup.ExecuteReader();
			r.Read();
			return r;
		}
		private void subtract(SqliteConnection connection)
		{
		//	string command = "DELETE FROM SQLITE_SEQUENCE WHERE name = 'm_scc';";
			//
		//	var update = connection.CreateCommand();
		//	update.CommandText = command;
		//	update.ExecuteNonQuery();
		}
		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
		{

			switch (editingStyle)
			{
				case UITableViewCellEditingStyle.Delete:
					if (delete)
					{
						// remove the item from the underlying data source
						//tableItems.RemoveAt(indexPath.Row);
						// delete the row from the table
						var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
						var filename = Path.Combine(documents, "sccMain.sqlite");
						//var filename = Path.Combine("/Users/liujack/Desktop/Dididi/sccMain.sqlite");
						var m_dbConnection = new SqliteConnection("Data Source= " + filename + ";");
						m_dbConnection.Open();
						var flip = m_dbConnection.CreateCommand();
						flip.CommandText = "SELECT * FROM m_scc ORDER BY _id DESC LIMIT 1";
						var r = flip.ExecuteReader();
						r.Read();
						int a;
                        Console.WriteLine(GetCellId(tableView, indexPath));
                        deleteid(m_dbConnection, Int32.Parse(GetCellId(tableView,indexPath)));
						for (a = Int32.Parse(GetCellId(tableView, indexPath)) + 1; a <= Int32.Parse(r["_id"].ToString()); a = a + 1)
						{
							var sub = a - 1;
							string command = "UPDATE m_scc SET _id=" + sub + " WHERE _id=" + a + ";";
							var lookup = m_dbConnection.CreateCommand();
							lookup.CommandText = command;
							lookup.ExecuteNonQuery();
						}
                        tableView.DequeueReusableCell(GetCellId(tableView, indexPath));


						subtract(m_dbConnection);
						Refresh();
                        TableItemsLength -= 1;
						tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
						m_dbConnection.Close();
						break;
					}
					else
					{
						break;
					}
					case UITableViewCellEditingStyle.None:
					Console.WriteLine("CommitEditingStyle:None called");
					break;
			

			}
		}

		public void Refresh()
		{
            try
            {
                ConnectionHandles _connection = new ConnectionHandles();
                var filename = "";
                var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                filename = Path.Combine(documents, "sccMain.sqlite");
                var m_dbConnection = new SqliteConnection("Data Source= " + filename + ";");
                m_dbConnection.Open();
                //  _connection.NewTransaction(m_dbConnection, 12, 23, 34, "fs", 23);

                var flip = m_dbConnection.CreateCommand();
                flip.CommandText = "SELECT * FROM m_scc ORDER BY _id DESC LIMIT 1";
                var r = flip.ExecuteReader();
                r.Read();
                string[] data = new string[Int32.Parse(r["_id"].ToString()) + 1];
                string[] SubData = new string[Int32.Parse(r["_id"].ToString()) + 1];
                string[] Ids = new string[Int32.Parse(r["_id"].ToString()) + 1];

                int a;
                float total = 0;
                for (a = 1; a < Int32.Parse(r["_id"].ToString()); a = a + 1)
                {
                    SqliteDataReader s = _connection.lookupid(m_dbConnection, a);
                    float amount = float.Parse(s["amount"].ToString());
                    int day = Int32.Parse(s["day"].ToString());
                    int month = Int32.Parse(s["month"].ToString());
                    int year = Int32.Parse(s["year"].ToString());
                    string store = s["store"].ToString();
                    string label = store + " | $" + amount.ToString();
                    string subLabel = month.ToString() + "/" + day.ToString() + "/" + year.ToString();
                    SubData[a] = subLabel;
                    data[a] = label;
                    Ids[a] = a.ToString();
                    total += float.Parse(s["amount"].ToString());
                }
                a = Int32.Parse(r["_id"].ToString());
                SqliteDataReader ss = _connection.lookupid(m_dbConnection, a);
                float amounts = float.Parse(ss["amount"].ToString());
                int days = Int32.Parse(ss["day"].ToString());
                int months = Int32.Parse(ss["month"].ToString());
                int years = Int32.Parse(ss["year"].ToString());
                string stores = ss["store"].ToString();
                string labels = stores + " | $" + amounts.ToString();
                string subLabels = months.ToString() + "/" + days.ToString() + "/" + years.ToString();
                SubData[a] = subLabels;
                data[a] = labels;
                Ids[a] = a.ToString();
                m_dbConnection.Close();
            }
            catch
            {
                Console.WriteLine("SCCSTATUS: No Transaction Found");   
            }
		}
		public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
		{   // Optional - default text is 'Delete'
			return "Remove";
		}

        string selectedRow;
		public string SelectedItem;
        
		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			tableView.DeselectRow(indexPath, true);
            SelectedItem = GetCellId(tableView, indexPath);
			Console.WriteLine("a"+selectedRow);

            try
            {
                ItemSelected(this, EventArgs.Empty);
            }
            catch
            {}
          //  mc.LaunchDetail(selectedRow);
        //	RowDeselected(tableView, indexPath);
        //	var ViewControl = Storyboard.InstantiateViewController("sm") as sm;
        //	PresentViewController(ViewControl, true, null);
        }


	}

}
