//
//  GraphPageController.swift
//  SCC
//
//  Created by Jack Liu on 9/11/17.
//  Copyright © 2017 Shabang Systems, LLC. All rights reserved.
//

import Foundation
import UIKit
class GraphPageController: UIPageViewController, UIPageViewControllerDelegate, UIPageViewControllerDataSource
{
    var currentCount = 0
    var ViewControllers: [UIViewController] = {
        let storyBoard = UIStoryboard(name: "Main", bundle: nil)
        return [storyBoard.instantiateViewController(withIdentifier: "MonthlyTransactionGraph"), storyBoard.instantiateViewController(withIdentifier: "StorewiseTransactionGraph")]
    }()
    private func retriveUIView(_ name: String) -> UIViewController
    {
        return UIStoryboard(name: "Main", bundle: nil).instantiateViewController(withIdentifier: name)
    }
    override func viewDidLoad() {
        super.viewDidLoad()
        dataSource = self
        delegate = self
        view.frame = CGRect(x: 0, y: 0, width: self.view.frame.width, height: self.view.frame.height+37)
        if let firstVC = ViewControllers.first{
            firstVC.view.frame = self.view.frame
            setViewControllers([firstVC], direction: .forward, animated: true, completion: nil)
        }
//       Timer.scheduledTimer(timeInterval: 2, target: self, selector: Selector(("moveToNextPageTwoSecond")), userInfo: nil, repeats: true)
    }
//    func moveToNextPageTwoSecond (){
//        // Get the View Controller for the page X of the Page View Controller and store in an array
//        var pageXViewController: UIViewController = ViewControllers[0]
//        if currentCount == 0
//        {
//            pageXViewController = ViewControllers[1]
//        }
//        if currentCount == 1
//        {
//            pageXViewController = ViewControllers[0]
//        }
//
//        let viewControllers: NSArray = [pageXViewController]
//        // Set the first page of the Page View Controller
//        setViewControllers(viewControllers as? [UIViewController], direction: .forward, animated: true, completion: nil)
//
//    }
    func pageViewController(_ pageViewController: UIPageViewController, viewControllerBefore viewController: UIViewController) -> UIViewController? {
        guard let vcIndex = ViewControllers.index(of: viewController) else {return nil}
        let prev = vcIndex-1
        currentCount = prev
        guard prev >= 0 else {return nil}
        guard ViewControllers.count  > prev else {return nil}
        let vcInstance = ViewControllers[prev]
        vcInstance.view.frame = self.view.frame
        return vcInstance
    }
    
    func pageViewController(_ pageViewController: UIPageViewController, viewControllerAfter viewController: UIViewController) -> UIViewController? {
        //Check if there is a view controller after, if so, add the count and return the next one.
        guard let vcIndex = ViewControllers.index(of: viewController) else {return nil}
        let next = vcIndex+1
        guard ViewControllers.count != next else {return nil}
        guard ViewControllers.count > next else {return nil}
        currentCount = next
        let vcInstance = ViewControllers[next]
        vcInstance.view.frame = self.view.frame
        return vcInstance
    }
    func presentationCount(for pageViewController: UIPageViewController) -> Int {
        return ViewControllers.count
    }
    func presentationIndex(for pageViewController: UIPageViewController) -> Int {
        if let firstViewController = viewControllers?.first
        {
            let firstVCIndex = ViewControllers.index(of: firstViewController)
            return firstVCIndex!
        }
        else
        {
            return 1
        }
    }
    override func viewDidLayoutSubviews() {
        super.viewDidLayoutSubviews()
        if let scrollView = view.subviews.filter({ $0 is UIScrollView }).first,
            let pageControl = view.subviews.filter({ $0 is UIPageControl }).first {
            scrollView.frame = view.bounds
            view.bringSubview(toFront:pageControl)
        }
    }
    
}
