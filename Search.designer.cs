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
    [Register ("Search")]
    partial class Search
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider amountSlider { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Current { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField month { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISegmentedControl SegmentC { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField store { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField yr { get; set; }

        [Action ("Moved:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Moved (UIKit.UISlider sender);

        [Action ("SegmentChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SegmentChanged (UIKit.UISegmentedControl sender);

        [Action ("UIButton5738_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton5738_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton6051_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton6051_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (amountSlider != null) {
                amountSlider.Dispose ();
                amountSlider = null;
            }

            if (Current != null) {
                Current.Dispose ();
                Current = null;
            }

            if (month != null) {
                month.Dispose ();
                month = null;
            }

            if (SegmentC != null) {
                SegmentC.Dispose ();
                SegmentC = null;
            }

            if (store != null) {
                store.Dispose ();
                store = null;
            }

            if (tableView != null) {
                tableView.Dispose ();
                tableView = null;
            }

            if (yr != null) {
                yr.Dispose ();
                yr = null;
            }
        }
    }
}