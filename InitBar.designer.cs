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
    [Register ("InitBar")]
    partial class InitBar
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ViewContrainer { get; set; }

        [Action ("ExitBar:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ExitBar (UIKit.UITapGestureRecognizer sender);

        void ReleaseDesignerOutlets ()
        {
            if (ViewContrainer != null) {
                ViewContrainer.Dispose ();
                ViewContrainer = null;
            }
        }
    }
}