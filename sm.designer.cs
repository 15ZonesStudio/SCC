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
    [Register ("sm")]
    partial class sm
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel amount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField amountnew { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField des { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField disc { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField dk { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField offv { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField qtext { get; set; }

        [Action ("UIButton2665_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton2665_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton2669_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton2669_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton2727_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton2727_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (amount != null) {
                amount.Dispose ();
                amount = null;
            }

            if (amountnew != null) {
                amountnew.Dispose ();
                amountnew = null;
            }

            if (des != null) {
                des.Dispose ();
                des = null;
            }

            if (disc != null) {
                disc.Dispose ();
                disc = null;
            }

            if (dk != null) {
                dk.Dispose ();
                dk = null;
            }

            if (offv != null) {
                offv.Dispose ();
                offv = null;
            }

            if (qtext != null) {
                qtext.Dispose ();
                qtext = null;
            }
        }
    }
}