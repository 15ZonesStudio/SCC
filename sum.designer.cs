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
    [Register ("sum")]
    partial class sum
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lab1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel labdore { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField month { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField store { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel totals { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField yr { get; set; }

        [Action ("UIButton1651_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton1651_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (lab1 != null) {
                lab1.Dispose ();
                lab1 = null;
            }

            if (labdore != null) {
                labdore.Dispose ();
                labdore = null;
            }

            if (month != null) {
                month.Dispose ();
                month = null;
            }

            if (store != null) {
                store.Dispose ();
                store = null;
            }

            if (tableView != null) {
                tableView.Dispose ();
                tableView = null;
            }

            if (totals != null) {
                totals.Dispose ();
                totals = null;
            }

            if (yr != null) {
                yr.Dispose ();
                yr = null;
            }
        }
    }
}