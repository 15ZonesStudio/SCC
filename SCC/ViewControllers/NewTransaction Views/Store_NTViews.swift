//
//  Store_NTViews.swift
//  SCC
//
//  Created by Jack Liu on 10/28/17.
//  Copyright © 2017 Shabang Systems, LLC. All rights reserved.
//

import Foundation
import UIKit
class Store_NTView : UIViewController
{
    
    @IBOutlet weak var input: UITextField!
    @IBAction func storeData(_ sender: Any) {
        let defaults = UserDefaults.standard
        if input.text == nil || input.text == ""
        {
            input.borderStyle = .line
            input.layer.borderColor = UIColor.red.cgColor
            input.layer.borderWidth = 2.0
            
        }
        else
        {
            defaults.set(input.text!, forKey: "NTViewsStore")
        }
    }
    override func shouldPerformSegue(withIdentifier identifier: String, sender: Any?) -> Bool {
        if identifier == "NTViewsStoreToDate"{
            if input.text == nil || input.text == ""
            {
                print("SCCWARNING: Store to Date Seague not performing...")
                return false
            }
            else
            {
                print("SCCINFO: Performing Store to Date Seague...")
                return true
            }
        }
        return true
    }
    
}
