# POMT

## Desc
The Petsi Order Management Tool provides the following features on release:
  * A uniform way of managing orders:
      - Calls Square Order API for customer orders
      - Allows data entry for Special orders and Wholesale orders    
  * Builds and Prints reports:
      - prints customer order lists by day,
      - baking lists by day or date range,
      - standing order wholesale lists by account or by day
  * Wholesale Package label printing:
      - Given a target day, prints labels based on wholesale standing order data to a Rollo wireless printer
      - Prints three different types of labels

## Why
  The core function of the POMT is to centralize all active order data. 
  
  Before the POMT, in order to determine what needs to be baked on any given day required an employee to check wholesale standing order spreadsheets, check a clipboard of special order forms, run a python script to get an aggregate of online orders from a .csv export provided by Square (a POS system/platform), determine what needs to be baked for retail, and aggregate all on two lists for the morning and afternoon baking shifts. 
  
  The POMT reduces this process to the selection of a date, filter order type, and click a button to build and print various lists/reports, and effectively swaps the previous daily list building routine to accurate and timely data entry for non-automated order data pulls.
  
  While I was motivated to build this tool for the given example, the core of having our order data centralized enables the potenial of more features that can help make the buisness more efficient, like providing an intuitive forecast of our order volume by order type over a set amount of time.
  
## Usage
