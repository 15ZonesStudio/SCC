//
//  MonthlyTransactionVC.swift
//  SCC
//
//  Created by Jack Liu on 9/11/17.
//  Copyright © 2017 Shabang Systems, LLC. All rights reserved.
//

import Foundation
import UIKit
class MonthlyTransactionVC : UIViewController
{
    @IBOutlet var ContainerView: UIView!
    var graphView: ScrollableGraphView! = nil
    @IBAction func showBigGraph(_ sender: UIGestureRecognizer) {
        //Show the Insights if graph is pushed
        if sender.state == .began
        {
            performSegue(withIdentifier: "showFullMonthly", sender: self)
            graphView = ScrollableGraphView(frame: CGRect(x: ContainerView.frame.minX, y: ContainerView.frame.minY, width: UIScreen.main.bounds.width, height: UIScreen.main.bounds.height))
        }
    }
    override func viewDidLoad() {
        super.viewDidLoad()
        if graphView == nil
        {
            graphView = ScrollableGraphView(frame: CGRect(x: ContainerView.frame.minX, y: ContainerView.frame.minY, width: UIScreen.main.bounds.width, height: ContainerView.frame.height-25))
        }
        let plotData = DatabaseModule().GetData()
        var months:[Int : Double]  = [
            1: 0.0,
            2: 0.0,
            3: 0.0,
            4: 0.0,
            5: 0.0,
            6: 0.0,
            7: 0.0,
            8: 0.0,
            9: 0.0,
            10: 0.0,
            11: 0.0,
            12: 0.0,
            ]
        for i in plotData
        {
            if months[Int(i.month)] != nil
            {
                let oldAmount: Double = months[Int(i.month)]!
                months[Int(i.month)] = Double(oldAmount+Double(i.amount))
            }
            else
            {
                months[Int(i.month)] = Double(i.amount)
            }
            
        }
        // Do any additional setup after loading the view, typically from a nib.
        let labels = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]
        let sortedKeys = months.sorted(by: <)
        var data: [Double] = []
        for i in sortedKeys
        {
            data.append(i.value)
        }
        
        //Make sure the data is not straight 0
        var largestValue = 0.0
        for i in months
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
        graphView.barLineColor = UIColor.colorFromHex("#777777")
        graphView.barColor = UIColor.colorFromHex("#555555")
        graphView.backgroundFillColor = UIColor.colorFromHex("#333333")
        self.view.backgroundColor = UIColor.colorFromHex("#333333")
        
        graphView.referenceLineLabelFont = UIFont.boldSystemFont(ofSize: 8)
        graphView.referenceLineColor = UIColor.white.withAlphaComponent(0.2)
        graphView.referenceLineLabelColor = UIColor.white
        graphView.numberOfIntermediateReferenceLines = 5
        graphView.dataPointLabelColor = UIColor.white.withAlphaComponent(0.5)
        graphView.showsHorizontalScrollIndicator = false
        graphView.shouldAnimateOnStartup = true
        //graphView.adaptAnimationType = ScrollableGraphViewAnimationType.elastic
        graphView.animationDuration = 1.5
        
        graphView.shouldRangeAlwaysStartAtZero = true
        
        //Set the range
        let sortedValues = months.values.sorted(by: >)
        graphView.rangeMax = sortedValues[0]+(sortedValues[0]/2)
        graphView.set(data: data, withLabels:labels)
        ContainerView.addSubview(graphView)
    }
    
}
