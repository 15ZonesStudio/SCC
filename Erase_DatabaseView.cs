using Foundation;
using System;
using UIKit;

namespace SCCiPhone
{
    public partial class Erase_DatabaseView : UIViewController
    {
        public Erase_DatabaseView(IntPtr handle) : base(handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("BackgroundGradiant.png"));
        }
        partial void UIButton7916_TouchUpInside(UIButton sender)
        {
            // Create a new Alert Controller
            UIAlertController actionSheetAlert = UIAlertController.Create("Are You Sure?", "There is no way to revert this action.", UIAlertControllerStyle.ActionSheet);

            // Add Actions
            actionSheetAlert.AddAction(UIAlertAction.Create("Yes, absolutely", UIAlertActionStyle.Default, (action) => EraseAndClose()));

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
        void EraseAndClose()
        { new ConnectionHandles().CreateEmptyDatabase().Close(); var controller = Storyboard.InstantiateViewController("HomeView") as HomeView; this.PresentViewController(controller, true, null);}
    }
}