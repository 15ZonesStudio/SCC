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
    
    let managedContext = (UIApplication.shared.delegate as! AppDelegate).persistentContainer.viewContext
    override func viewDidLoad() {
        super.viewDidLoad()
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
}
