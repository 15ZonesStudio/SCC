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
    [Register ("NewTransaction")]
    partial class NewTransaction
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel amount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField AmountInput { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel day { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField DayInput { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel month { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField MonthInput { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel store { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField StoreInput { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton Submit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel year { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField YearInput { get; set; }

        [Action ("AmountChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void AmountChanged (UIKit.UITextField sender);

        [Action ("DayChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void DayChanged (UIKit.UITextField sender);

        [Action ("MonthChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void MonthChanged (UIKit.UITextField sender);

        [Action ("StoreChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void StoreChanged (UIKit.UITextField sender);

        [Action ("Submit_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Submit_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton2841_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton2841_TouchUpInside (UIKit.UIButton sender);

        [Action ("YearChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void YearChanged (UIKit.UITextField sender);

        void ReleaseDesignerOutlets ()
        {
            if (amount != null) {
                amount.Dispose ();
                amount = null;
            }

            if (AmountInput != null) {
                AmountInput.Dispose ();
                AmountInput = null;
            }

            if (day != null) {
                day.Dispose ();
                day = null;
            }

            if (DayInput != null) {
                DayInput.Dispose ();
                DayInput = null;
            }

            if (month != null) {
                month.Dispose ();
                month = null;
            }

            if (MonthInput != null) {
                MonthInput.Dispose ();
                MonthInput = null;
            }

            if (store != null) {
                store.Dispose ();
                store = null;
            }

            if (StoreInput != null) {
                StoreInput.Dispose ();
                StoreInput = null;
            }

            if (Submit != null) {
                Submit.Dispose ();
                Submit = null;
            }

            if (year != null) {
                year.Dispose ();
                year = null;
            }

            if (YearInput != null) {
                YearInput.Dispose ();
                YearInput = null;
            }
        }
    }
}