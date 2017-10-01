//
//  BarGraphSource.swift
//  SCC
//
//  Created by Jack Liu on 9/10/17.
//  Copyright © 2017 Shabang Systems, LLC. All rights reserved.
//

import Foundation
class BarGraphDataSource: ScrollableGraphViewDataSource
{
    var data: [Float] = []
    init (_ data: [Float])
    {
        self.data = data
    }
    
    func value(forPlot plot: Plot, atIndex pointIndex: Int) -> Double {
        return Double(data[pointIndex])
    }
    
    func label(atIndex pointIndex: Int) -> String {
        return String(pointIndex+1)
    }
    
    func numberOfPoints() -> Int {
        return self.data.count
    }
    
    
}
