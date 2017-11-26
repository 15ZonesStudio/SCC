//
//  Amount_NTViews.swift
//  SCC
//
//  Created by Jack Liu on 10/28/17.
//  Copyright © 2017 Shabang Systems, LLC. All rights reserved.
//

import Foundation
import UIKit
class Amount_NTView : UIViewController
{
    override func viewDidLoad() {
        self.hideKeyboard()
    }
    
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
            let inputText = input.text!
            defaults.set(inputText[(1 ..< inputText.length+1)], forKey: "NTViewsAmount")
        }
        storeData()
    }
    func storeData()
    {
        DatabaseModule().NewTransaction(Day: Int32(UserDefaults.standard.string(forKey: "NTViewsDate_D")!)!, Month: Int32(UserDefaults.standard.string(forKey: "NTViewsDate_M")!)!, Year: Int32(UserDefaults.standard.string(forKey: "NTViewsDate_Y")!)!, Amount: Float(UserDefaults.standard.string(forKey: "NTViewsAmount")!)!, Store: UserDefaults.standard.string(forKey: "NTViewsStore")!)
    }
    @IBAction func addDollarSign(_ sender: Any) {
        if input.text != nil || input.text != ""
        {
            if !input.text!.contains("$")
            {
                input.text = "$"+input.text!
            }
            else if input.text! == "$"
            {
                input.text = ""
            }
        }
    }
}

