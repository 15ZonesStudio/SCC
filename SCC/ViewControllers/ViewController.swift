//
//  ViewController.swift
//  SCC
//
//  Created by Jack Liu on 9/4/17.
//  Copyright © 2017 Shabang Systems, LLC. All rights reserved.
//

import UIKit
import MaterialComponents

import UIColor_Hex_Swift
import CoreData

import SideMenu

extension UIApplication {
    var statusBarView: UIView? {
        return value(forKey: "statusBar") as? UIView
    }
}


class ViewController: UIViewController {
    
    // Outlets
    @IBOutlet weak var currentMonthSpending: UILabel!
    @IBOutlet weak var currentMonth: UILabel!
    @IBOutlet weak var budgetBar: UIProgressView!
    @IBOutlet weak var budgetPercent: UILabel!
    @objc let managedObjectContext = (UIApplication.shared.delegate as! AppDelegate).persistentContainer.viewContext
    
    @objc func setBudgetArea() {
        // Testing by setting the budget to 10,000 bucks
        let userDefaults = UserDefaults.standard
        
        // Get current month
        let date = Date()
        let formatter = DateFormatter()
        formatter.dateFormat = "MM"
        let mresult = formatter.string(from: date)
        
        formatter.dateFormat = "yyyy"
        let yresult = formatter.string(from: date)
        
        // Look that up in the DB
        let databaseModuleInstance = DatabaseModule()
        let fetchedData = databaseModuleInstance.GetDataWithParams(Month: mresult, Year: yresult, Store: nil, Amount: nil)
        
        var total: Float = 0
        for e in fetchedData!
        {
            total += e.amount
        }
        
        if currentMonthSpending != nil
        {
            let totalNSNum: NSNumber = total as NSNumber
            let numberFormatter = NumberFormatter()
            numberFormatter.numberStyle = .currency
            currentMonthSpending.text = numberFormatter.string(from: totalNSNum)
        }
        
        if currentMonth != nil
        {
            currentMonth.text = "Total in "+Date().monthFull+":"
        }
        
        // Look up the user's budget
        let budget = userDefaults.float(forKey: "budget")
        let percentage:Float = total/budget
        
        // Set the budget bar
        if budgetBar != nil
        {
            budgetBar.setProgress(percentage, animated: true)
        }
        
        let budgetNSNum: NSNumber = budget as NSNumber
        let numberFormatter = NumberFormatter()
        numberFormatter.numberStyle = .currency
        
        if budgetPercent != nil
        {
            budgetPercent.text = String(round(100*percentage))+"% of "+numberFormatter.string(from: budgetNSNum)!
        }
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        // Handle side menu
        SideMenuManager.menuPresentMode = .menuSlideIn
        SideMenuManager.menuPushStyle = .replace
        
        // Set the budget area
        setBudgetArea()
        
        // Listen for any DB events and reset the budget area
        let notificationCenter = NotificationCenter.default
        notificationCenter.addObserver(self, selector: #selector(setBudgetArea), name: NSNotification.Name.NSManagedObjectContextDidSave, object: managedObjectContext)
    }



}

