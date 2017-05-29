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
    [Register ("GraphMenu")]
    partial class GraphMenu
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView MonthlyGraphContainer { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView StorelyGraphContainer { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (MonthlyGraphContainer != null) {
                MonthlyGraphContainer.Dispose ();
                MonthlyGraphContainer = null;
            }

            if (StorelyGraphContainer != null) {
                StorelyGraphContainer.Dispose ();
                StorelyGraphContainer = null;
            }
        }
    }
}