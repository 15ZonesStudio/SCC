//
//  ManagedDatabaseModule.swift
//  SCC
//
//  Created by Jack Liu on 9/10/17.
//  Copyright © 2017 Shabang Systems, LLC. All rights reserved.
//

import Foundation
import CoreData
import UIKit
let context = (UIApplication.shared.delegate as! AppDelegate).persistentContainer.viewContext
class DatabaseModule
{
    
    var Data:[Transaction] = []
    
    //Pass in transaction data as params, saves transaction data into CoreData's sqlite base.
    func NewTransaction(Day dy:Int32, Month mn:Int32, Year yr:Int32, Amount am:Float, Store st:String)
    {
        let newTransaction = NSEntityDescription.insertNewObject(forEntityName: "Transaction", into: context) as NSManagedObject
        newTransaction.setValue(dy, forKey: "day")
        newTransaction.setValue(mn, forKey: "month")
        newTransaction.setValue(yr, forKey: "year")
        newTransaction.setValue(am, forKey: "amount")
        newTransaction.setValue(st, forKey: "store")
        do
        {
            
            try context.save()
        }
        catch
        {
            print("SCCERROR: Insertion Failed")
        }
        
        
        
    }
    
    //Use a fetch request to fetch all transaction data.
    func GetData() -> [Transaction]
    {
        
        let fetchRequest = NSFetchRequest<NSFetchRequestResult>(entityName: "Transaction")
        fetchRequest.returnsObjectsAsFaults = false;
        //Narrow down the results by only feching some objects: fetchRequest.predicate = NSPredicate(format: "month = '%@'", "1")
        do
        {
            Data = try context.fetch(fetchRequest) as! [Transaction]
        }
        catch
        {
            print("SCCERROR: Fetch Failed")
        }
        //Get result from array works like this: Data[*somerow*].store
        return Data
    }
    func ResetDatabase()
    {
        let fetch = NSFetchRequest<NSFetchRequestResult>(entityName: "Transaction")
        let request = NSBatchDeleteRequest(fetchRequest: fetch)
        do
        {
            _ = try context.execute(request)
        }
        catch
        {
            print("SCCERROR: Batch Delete Failed")
        }
        
    }
    func GetDataWithParams(Month month:String? = nil, Year year: String? = nil, Store store: String? = nil, Amount amount: String? = nil) -> [Transaction]?
    {
        
        let fetchRequest = NSFetchRequest<NSFetchRequestResult>(entityName: "Transaction")
        var data: [Transaction] = []
        if let _month = month
        {
            fetchRequest.predicate = NSPredicate(format: "month == %@", NSNumber(value: Int(_month)!))
        }
        if let _store = store
        {
            fetchRequest.predicate = NSPredicate(format: "store == %@", NSNumber(value: Int(_store)!))
        }
        if let _year = year
        {
            fetchRequest.predicate = NSPredicate(format: "year == %@", NSNumber(value: Int(_year)!))
        }
        if let _amount = amount
        {
            fetchRequest.predicate = NSPredicate(format: "amount"+_amount)
        }
        do
        {
            data = try context.fetch(fetchRequest) as! [Transaction]
        }
        catch
        {
            print("SCCERROR: Fetch Failed")
        }
        return data
        //Get result from array works like this: Data[*somerow*].store
    }
    func DeleteData(Data data: [Transaction], At index: Int)
    {
        do
        {
            //Remember to remove the value from the data as well, or make a fetch right after (not ideal)
            context.delete(data[index] as NSManagedObject)
            try context.save()
        }
        catch
        {
            print("SCCERROR: Delete Failed")
        }
        
    }
    
}

