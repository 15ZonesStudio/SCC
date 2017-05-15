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
    [Register ("NewTrans")]
    partial class NewTrans
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel am { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField amount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel da { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField day { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lab { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel m { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField month { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel st { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField store { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton submit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField year { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel yr { get; set; }

        [Action ("amountchg:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void amountchg (UIKit.UITextField sender);

        [Action ("dchg:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void dchg (UIKit.UITextField sender);

        [Action ("monthc:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void monthc (UIKit.UITextField sender);

        [Action ("storechg:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void storechg (UIKit.UITextField sender);

        [Action ("UIButton2841_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton2841_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton830_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton830_TouchUpInside (UIKit.UIButton sender);

        [Action ("yearchg:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void yearchg (UIKit.UITextField sender);

        void ReleaseDesignerOutlets ()
        {
            if (am != null) {
                am.Dispose ();
                am = null;
            }

            if (amount != null) {
                amount.Dispose ();
                amount = null;
            }

            if (da != null) {
                da.Dispose ();
                da = null;
            }

            if (day != null) {
                day.Dispose ();
                day = null;
            }

            if (lab != null) {
                lab.Dispose ();
                lab = null;
            }

            if (m != null) {
                m.Dispose ();
                m = null;
            }

            if (month != null) {
                month.Dispose ();
                month = null;
            }

            if (st != null) {
                st.Dispose ();
                st = null;
            }

            if (store != null) {
                store.Dispose ();
                store = null;
            }

            if (submit != null) {
                submit.Dispose ();
                submit = null;
            }

            if (year != null) {
                year.Dispose ();
                year = null;
            }

            if (yr != null) {
                yr.Dispose ();
                yr = null;
            }
        }
    }
}