using Foundation;
using System;
using UIKit;

namespace SCCiPhone
{
    public partial class Erase_Database : UIViewController
    {
        public Erase_Database (IntPtr handle) : base (handle)
        {
        }

        partial void UIButton7916_TouchUpInside(UIButton sender)
        {
			// Create a new Alert Controller
			UIAlertController actionSheetAlert = UIAlertController.Create("Are you sure?", "Please confirm that you are sure to erase all transacitons.", UIAlertControllerStyle.ActionSheet);

			// Add Actions
			actionSheetAlert.AddAction(UIAlertAction.Create("Yes", UIAlertActionStyle.Default, (action) => eraseData()));

			actionSheetAlert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, (action) => Console.WriteLine("Cancel button pressed.")));

			// Required for iPad - You must specify a source for the Action Sheet since it is
			// displayed as a popover
			UIPopoverPresentationController presentationPopover = actionSheetAlert.PopoverPresentationController;
			if (presentationPopover != null)
			{
				presentationPopover.SourceView = this.View;
				presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
			}

			// Display the alert
			this.PresentViewController(actionSheetAlert, true, null);
        }
        void eraseData()
        {
            var _connection = new ConnectionHandles();
            var connection = _connection.CreateEmptyDatabase();
            connection.Close();
			var controller = Storyboard.InstantiateViewController("HomeView") as HomeView;
			PresentViewController(controller, true, null);
        }
    }
}