//
//  StorewiseTransactionVC.swift
//  SCC
//
//  Created by Jack Liu on 9/11/17.
//  Copyright © 2017 Shabang Systems, LLC. All rights reserved.
//

import Foundation
import UIKit
class StorewiseTransactionVC : UIViewController
{
    var bottomMargin = 25
    var graphView: ScrollableGraphView! = nil
    @IBOutlet var ContainerView: UIView!
    @objc let managedObjectContext = (UIApplication.shared.delegate as! AppDelegate).persistentContainer.viewContext
    func findKeyForValue(value: Double, dictionary: [String : Double]) ->String?
    {
        for i in dictionary
        {
            if value == i.value
            {
                return i.key
            }
        }
        
        return nil
    }
    func findValueForKey(key: String, dictionary: [String : Double]) -> Double?
    {
        if let result = dictionary[key]
        {return result}
        return nil
    }
    func mapFromValues(_ array: [Double], Dictionary dict: [String : Double]) -> [String : Double]
    {
        var newDict = [String : Double]()
        for i in array
        {
            if let key = findKeyForValue(value: i, dictionary: dict)
            {
                newDict[key] = i
            }
        }
        return newDict
    }
    
    func mapFromKeys(_ array: [String], Dictionary dict: [String : Double]) -> [String : Double]
    {
        var newDict = [String : Double]()
        for i in array
        {
            if let value = findValueForKey(key: i, dictionary: dict)
            {
                newDict[i] = value
            }
        }
        return newDict
    }
    @IBAction func showBigGraph(_ sender: UIGestureRecognizer) {
        //Show the Insights if graph is pushed
        if sender.state == .began
        {
            performSegue(withIdentifier: "showFullStorewise", sender: self)
            graphView = ScrollableGraphView(frame: CGRect(x: ContainerView.frame.minX, y: ContainerView.frame.minY, width: UIScreen.main.bounds.width, height: UIScreen.main.bounds.height))
        }
    }
    func loadGraph()
    {
        let plotData = DatabaseModule().GetData()
        var stores:[String : Double]  = [:]
        var times:[String: Double] = [:]
        for i in plotData
        {
            if stores[i.store!] != nil
            {
                let oldAmount: Double = stores[i.store!]!
                let oldTime: Double = times[i.store!]!
                stores[i.store!] = Double(oldAmount+Double(i.amount))
                times[i.store!] = Double(oldTime+1)
            }
            else
            {
                stores[i.store!] = Double(i.amount)
                times[i.store!] = Double(1)
            }
            
        }
        
        // Do any additional setup after loading the view, typically from a nib.
        let sortedTms = times.values.sorted(by: >)
        let mapped = mapFromValues(sortedTms, Dictionary: times)
        let sortedDict = mapFromKeys(Array(mapped.keys), Dictionary: stores)
        var data: [Double] = []
        for i in sortedDict
        {
            data.append(i.value)
        }
        var labs: [String] = []
        for i in sortedDict
        {
            labs.append(i.key)
        }
        var largestValue = 0.0
        for i in stores
        {
            if i.value > largestValue {largestValue = i.value}
        }
        guard largestValue > 0 else {return}
        // Disable the lines and data points.
        graphView.shouldDrawDataPoint = false
        graphView.lineColor = UIColor.clear
        
        // Tell the graph it should draw the bar layer instead.
        graphView.shouldDrawBarLayer = true
        
        // Customise the bar.
        graphView.barWidth = 25
        graphView.barLineWidth = 1
        graphView.barLineColor = UIColor.colorFromHex("#52DBFF")
        graphView.barColor = UIColor.colorFromHex("#34CCFF").withAlphaComponent(0.7)
        graphView.backgroundFillColor = UIColor.colorFromHex("#2f56E9")
        self.view.backgroundColor = UIColor.colorFromHex("#2f56E9")
        
        graphView.referenceLineLabelFont = UIFont.boldSystemFont(ofSize: 8)
        graphView.referenceLineColor = UIColor.white.withAlphaComponent(0.2)
        graphView.referenceLineLabelColor = UIColor.white
        graphView.numberOfIntermediateReferenceLines = 5
        graphView.dataPointLabelColor = UIColor.white
        graphView.showsHorizontalScrollIndicator = false
        graphView.shouldAnimateOnStartup = true
        graphView.adaptAnimationType = ScrollableGraphViewAnimationType.elastic
        graphView.animationDuration = 1.5
        
        graphView.shouldRangeAlwaysStartAtZero = true
        
        //Set the range
        let sortedValues = stores.values.sorted(by: >)
        graphView.rangeMax = sortedValues[0]+(sortedValues[0]/2)
        graphView.set(data: data, withLabels:labs)
        ContainerView.addSubview(graphView)
    }
    override func viewDidLoad() {
        super.viewDidLoad()
        let notificationCenter = NotificationCenter.default
        notificationCenter.addObserver(self, selector: #selector(managedObjectContextObjectsDidChange), name: NSNotification.Name.NSManagedObjectContextDidSave, object: managedObjectContext)
        if graphView == nil
        {
            graphView = ScrollableGraphView(frame: CGRect(x: ContainerView.frame.minX, y: ContainerView.frame.minY, width: UIScreen.main.bounds.width, height: ContainerView.frame.height-25))
        }
        loadGraph()
    }
    @objc func managedObjectContextObjectsDidChange(notification: NSNotification)
    {
        let subViews = ContainerView.subviews
        for subview in subViews{
            subview.removeFromSuperview()
        }
        loadGraph()
        self.view.backgroundColor = UIColor.white
        self.view.setNeedsDisplay()
        self.view.setNeedsLayout()
    }
    
}



