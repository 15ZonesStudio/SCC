// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace SCCiPhone
{
    [Register ("HomeView")]
    partial class HomeView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel amt { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIProgressView BudgetBar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView recent { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SearchBut { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel tot { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (amt != null) {
                amt.Dispose ();
                amt = null;
            }

            if (BudgetBar != null) {
                BudgetBar.Dispose ();
                BudgetBar = null;
            }

            if (recent != null) {
                recent.Dispose ();
                recent = null;
            }

            if (SearchBut != null) {
                SearchBut.Dispose ();
                SearchBut = null;
            }

            if (tot != null) {
                tot.Dispose ();
                tot = null;
            }
        }
    }
}