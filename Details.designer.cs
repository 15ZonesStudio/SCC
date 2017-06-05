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
    [Register ("Details")]
    partial class Details
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel amount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel day { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel store { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (amount != null) {
                amount.Dispose ();
                amount = null;
            }

            if (day != null) {
                day.Dispose ();
                day = null;
            }

            if (store != null) {
                store.Dispose ();
                store = null;
            }
        }
    }
}