//
//  TableViewController.swift
//  SCC
//
//  Created by Jack Liu on 9/9/17.
//  Copyright © 2017 Shabang Systems, LLC. All rights reserved.
//

import UIKit
import CoreData
class TableViewController: UIViewController, UITableViewDelegate, UITableViewDataSource {
    
    // Managed Object Context
    let managedContext = (UIApplication.shared.delegate as! AppDelegate).persistentContainer.viewContext
    
    // Data model: These strings will be the data for the table view cells
    public var data: [Transaction] = []
    
    // Length of data
    public var dataLength: Int = 0
    
    // cell reuse id (cells that scroll out of view can be reused)
    let cellReuseIdentifier = "cell"
    
    // don't forget to hook this up from the storyboard
    @IBOutlet var tableView: UITableView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        // Fetch the transaction data from CoreData
        let fetchRequest: NSFetchRequest<Transaction> = Transaction.fetchRequest()
        do
        {
            data = try managedContext.fetch(fetchRequest)
            tableView.reloadData()
        }catch{
            print("SCCERROR: Could not fetch data")
        }
        
        // Reverse the data array
        data.reverse()
        
        // Get the length of data
        dataLength = data.count
        
        // Register the table view cell class and its reuse id
        self.tableView.register(UITableViewCell.self, forCellReuseIdentifier: cellReuseIdentifier)
        
        // This view controller itself will provide the delegate methods and row data for the table view.
        tableView.delegate = self
        tableView.dataSource = self
    }
    
    // number of rows in table view
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return self.dataLength
    }
    
    // create a cell for each table view row
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        // create a new cell if needed or reuse an old one & try to parse it to be TransactionTableViewCell
        guard let cell = tableView.dequeueReusableCell(withIdentifier: "cellID", for: indexPath) as? TransactionTableViewCell else {
                fatalError("The dequeued cell is not an instance of TransactionTableViewCell.")
            }
        
        // set the text from the data
        cell.amountLabel.text = String(data[indexPath.row].amount)
        cell.storeLabel.text = data[indexPath.row].store
        cell.dateLabel.text = String(data[indexPath.row].month) + "/" + String(data[indexPath.row].day) + "/" + String(data[indexPath.row].year)
        
        return cell
    }
    
    // method to run when table view cell is tapped
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        tableView.deselectRow(at: indexPath, animated: true)
    }
    
    // method to delete data
    func tableView(_ tableView: UITableView, commit editingStyle: UITableViewCellEditingStyle, forRowAt indexPath: IndexPath) {
        if editingStyle == .delete {
            let dbModule = DatabaseModule()
            dbModule.DeleteData(Data: data, At: indexPath.row)
            self.dataLength -= 1
            self.tableView.deleteRows(at: [indexPath], with: .automatic)
            
            // Re-fetch and reload the data form database
            // Fetch the transaction data from CoreData
            let fetchRequest: NSFetchRequest<Transaction> = Transaction.fetchRequest()
            do
            {
                data = try managedContext.fetch(fetchRequest)
            }catch{
                print("SCCERROR: Could not fetch data")
            }
            // Reverse the data array
            data.reverse()
            // Actually reload
            tableView.reloadData()
        
        }
    }
}
