//
//  NewTransactionViewController.swift
//  SCC
//
//  Created by Jack Liu on 9/9/17.
//  Copyright © 2017 Shabang Systems, LLC. All rights reserved.
//

import Foundation
import UIKit

class NewTransactionViewController: ViewController
{
    @IBOutlet weak var year: UITextField!
    @IBOutlet weak var month: UITextField!
    @IBOutlet weak var day: UITextField!
    @IBOutlet weak var store: UITextField!
    @IBOutlet weak var amount: UITextField!
    @IBOutlet weak var storePreview: UILabel!
    @IBOutlet weak var amountPreview: UILabel!
    @IBOutlet weak var datePreview: UILabel!
    
    let managedContext = (UIApplication.shared.delegate as! AppDelegate).persistentContainer.viewContext
    override func viewDidLoad() {
        super.viewDidLoad()
    }
    override func touchesBegan(_ touches: Set<UITouch>, with event: UIEvent?) {
        self.hideKeyboard()
    }
    @IBAction func SubmitTransactions(_ sender: Any) {
        let transaction = Transaction(context: self.managedContext)
        transaction.day = Int32(day.text!)!
        transaction.month = Int32(month.text!)!
        transaction.year = Int32(year.text!)!
        transaction.amount = Float(amount.text!)!
        transaction.store = store.text!
        
        do
        {
            try self.managedContext.save()
        }catch
        {
            print("SCCERROR: Could Not Save Context")
        }
    }
    @IBAction func yearChanged(_ sender: Any) {
        var currentMonth = "--"
        if month.text != ""
        {
            currentMonth = month.text!
        }
        var currentDay = "--"
        if day.text != ""
        {
            currentDay = day.text!
        }
        let currentYear = year.text!
        self.datePreview.text = currentMonth+"/"+currentDay+"/"+currentYear
    }
    @IBAction func monthChanged(_ sender: Any) {
        let currentMonth = month.text!
        var currentDay = "--"
        if day.text != ""
        {
            currentDay = day.text!
        }
        var currentYear = "----"
        if year.text != ""
        {
            currentYear = year.text!
        }
        self.datePreview.text = currentMonth+"/"+currentDay+"/"+currentYear
    }
    @IBAction func dayChanged(_ sender: Any) {
        var currentMonth = "--"
        if month.text != ""
        {
            currentMonth = month.text!
        }
        let currentDay = day.text!
        var currentYear = "----"
        if year.text != ""
        {
            currentYear = year.text!
        }
        self.datePreview.text = currentMonth+"/"+currentDay+"/"+currentYear
    }
    @IBAction func amountChanged(_ sender: Any) {
        if Decimal(string: amount.text!) == nil{
            return
        }
        let number = NSDecimalNumber(decimal: Decimal(string: amount.text!)!)
        
        let numberFormatter = NumberFormatter()
        numberFormatter.numberStyle = .currency
        numberFormatter.locale = Locale.current
        
        let result = numberFormatter.string(from: number)
        self.amountPreview.text = result!
    }
    @IBAction func storeChanged(_ sender: Any) {
        self.storePreview.text = store.text!
    }
    
    @IBAction func fillDate(_ sender: Any) {
        let date = Date()
        let calendar = Calendar.current
        
        let year = calendar.component(.year, from: date)
        let month = calendar.component(.month, from: date)
        let day = calendar.component(.day, from: date)
        
        self.day.text = String(day)
        self.month.text = String(month)
        self.year.text = String(year)
        
        var currentMonth = "--"
        if self.month.text != ""
        {
            currentMonth = self.month.text!
        }
        var currentDay = "--"
        if self.day.text != ""
        {
            currentDay = self.day.text!
        }
        var currentYear = "----"
        if self.year.text != ""
        {
            currentYear = self.year.text!
        }
        self.datePreview.text = currentMonth+"/"+currentDay+"/"+currentYear
    }
}
