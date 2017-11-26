//
//  Date_NTViews.swift
//  SCC
//
//  Created by Jack Liu on 10/28/17.
//  Copyright © 2017 Shabang Systems, LLC. All rights reserved.
//

import Foundation
import UIKit
class Date_NTView : UIViewController
{
    override func viewDidLoad() {
        super.viewDidLoad()
        NotificationCenter.default.addObserver(self, selector: #selector(Date_NTView.goPrevious), name: NSNotification.Name("deletePressed"), object: nil)
        self.hideKeyboard()
    }
    
    var isHangingDelete = false
    @IBOutlet weak var input: UITextField!
    @IBAction func storeData(_ sender: Any) {
        let defaults = UserDefaults.standard
        if input.text == nil || input.text! == "" || input.text!.count>10
        {
            input.borderStyle = .line
            input.layer.borderColor = UIColor.red.cgColor
            input.layer.borderWidth = 2.0
            
        }
        else
        {
            let inputText = input.text!
            defaults.set(inputText[(-1 ..< 2)], forKey: "NTViewsDate_M")
            defaults.set(inputText[(3 ..< 5)], forKey: "NTViewsDate_D")
            defaults.set(inputText[(6 ..< 10)], forKey: "NTViewsDate_Y")
        }
    }
    @IBAction func FitDate(_ sender: Any) {
        if !isHangingDelete
        {
            if let inputText = input.text
            {
                let count = inputText.count
                if count == 2{
                    input.text = inputText+"/"
                }
                else if count == 5{
                    input.text = inputText+"/"
                }
                else if count >= 11{
                    print("do!")
                    input.text = inputText[(0 ..< 10)]
                }
            }
        }
        else
        {
            isHangingDelete = false
        }
    }
    override func shouldPerformSegue(withIdentifier identifier: String, sender: Any?) -> Bool {
        if identifier == "NTViewsDateToAmount"{
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
    @objc func goPrevious(){
        let inputText = input.text!
        input.text = inputText[(-1 ..< inputText.length)]
        isHangingDelete = true
    }
    @IBAction func fillDate(_ sender: Any) {
        let date = Date()
        let calendar = Calendar.current
        
        let year = calendar.component(.year, from: date)
        let month = calendar.component(.month, from: date)
        let day = calendar.component(.day, from: date)

        input.text = String(month)+"/"+String(day)+"/"+String(year)
    }
    
}
