//
//  BudgetGoalVC.swift
//  SCC
//
//  Created by Jack Liu on 11/24/17.
//  Copyright © 2017 Shabang Systems, LLC. All rights reserved.
//

import Foundation
import UIKit

class BudgetGoalVC: UIViewController {
    // Outlets
    @IBOutlet weak var budgetText: UITextField!
    @IBOutlet weak var budgetStepper: UIStepper!
    @IBOutlet weak var yearlyIncome: UILabel!
    @IBOutlet weak var lifetimeIncome: UILabel!
    @IBOutlet weak var averageIncome: UILabel!
    
    override func viewDidLoad() {
        let userDefaults = UserDefaults.standard
        let budget = userDefaults.float(forKey: "budget")
        budgetText.text = String(budget)
        conformLabels()
        hideKeyboard()
    }
    
    func conformLabels()
    {
        var budget = Double()
        if let b = Double(budgetText.text!)
        {
            budget = b
        }
        else
        {
            budget = 0
        }
        let yearly = budget*12
        let lifetime = yearly*70
        let average = (yearly/9733)*100
        yearlyIncome.text = "$"+String(yearly)
        lifetimeIncome.text = "$"+String(lifetime)
        averageIncome.text = String(round(average))+"%"
        budgetStepper.value = budget
    }
    
    @IBAction func stepperTriggered(_ sender: UIStepper) {
        budgetText.text = Int(sender.value).description
        conformLabels()
    }
    
    @IBAction func budgetEdited(_ sender: UITextField) {
        if let text = sender.text
        {
            if let value = Double(text){budgetStepper.value = value; conformLabels()}
            else{budgetStepper.value = 0; conformLabels(); sender.text = "0"}
        }
        else
        {
            // Handle Situation
        }
    }
    @IBAction func saveBudget(_ sender: Any) {
        var budget = Double()
        if let b = Double(budgetText.text!)
        {
            budget = b
        }
        else
        {
            budget = 0
        }
        UserDefaults.standard.set(Float(budget), forKey: "budget")
    }
}

