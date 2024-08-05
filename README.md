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
  
## Usage/Examples

### Order Dashboard
![OrderListView](https://github.com/user-attachments/assets/14a830d5-79e4-4696-bb61-3c2aa15c4bd5)
* View all currently active orders
* Filter by order type (Wholesale, Square, Retail, ...) with tab buttons
* Search orders by recipient name, or item name
* Double click an order to see details

### Order Item View
![OrderView](https://github.com/user-attachments/assets/6eb31014-f5cc-49e7-8559-fac269ba7d16)
* Shows order specifics, whether creating a new order or viewing an existing one
* Toggle and edit button to make changes, click save to finalize changes
* Toggle freeze order to ensure it isn't included in active order aggregation processes
* type an item name and utilize the drop down to select the item from the catalog (including alternative names, details in the Catalog Item View)

### Report Dashboard
![ReportView](https://github.com/user-attachments/assets/a3ab703d-02fe-486a-813f-8dd313bdc8b5)
* Filter by order types to include/exclude on order pull
* Select a start date, can be date range for specific report types
* Select whether to print, export to designated folder, or both
* Configure the activate report template for bakers list (a static list structure of products, to ensure the list is the same sequence of items for consistent readability)

### Example of Baker List/Report
![BackListPie](https://github.com/user-attachments/assets/c6758c95-ea32-41a7-a61e-27a0193a0177)
* All reports have a header with Report number, and other specifics (Report number is used to assist with debugging/debriefing any potential problems concerning order accuracy)
* A simple break down utilizing the summer pie template
* Row names specified in the template and mapped to the correct item
* Vegan pies denoted with a V

### Template List
![TemplateListView](https://github.com/user-attachments/assets/8000070d-eae0-48db-8525-df664ecafb9d)
* A simple view of all saved templates

### Template Item View
![TemplateView](https://github.com/user-attachments/assets/8b74d01b-3b50-41da-a882-fab445971de3)
* Details of the Summer Pies template
* Determines the format of a bakers list for pies

### Catalog List
![CatalogListView](https://github.com/user-attachments/assets/7f91827e-8365-47cf-86ae-dd85480712f3)
* View all items in the catalog/menu
* Pulled from Square's Catalog API
* Can add non-square catalog items

### Catalog Item View
![CatalogItemView](https://github.com/user-attachments/assets/d71859ae-7ce3-4d41-92be-c2c3ee5f27fb)
* Details of a catalog item, whether creating an item or viewing/modifying an existing one
* Toggle edit button to make changes, save to finalize
* Alternative names allow multiple names to be associated with an item, to both adhere to common abrieviations used in the buisnes, and to solve naming conflicts due how Squares catalog interacts with some POMT requirements.
* Associate vegan, Take N Bake, and Vegan Take N Bake pies to properly aggregate on lists
* Assoicate label files for wholesale products, (can also interact through label configuration view)
* Apply attributes to restrict sizes, flag as the Pie of the Month (POTM), or if parbakes are required for proper aggregation

### Label Dashboard
![LabelView](https://github.com/user-attachments/assets/20fe98d4-6424-4a63-94f3-679cff107ee1)
* A simple dashboard to print labels to a local thermal printer for wholesale packaging
* Select the date of order fulfillment and prints from three different sized labels

### Configure Labels View
![ConfigureLabels](https://github.com/user-attachments/assets/9e58754a-3006-4454-8389-467c3ecfdf24)
* A niche but helpful window to manage wholesale label configurations
* Provides a list of all items with a label mapping
* double click to view label mapping details

### Label Item View
![LabelItemView](https://github.com/user-attachments/assets/bf11f3e9-7de3-417c-9f8a-48893af79ef3)
* A detailed view of a label mapping, whether creating or viewing and existing

### Settings
![Settings](https://github.com/user-attachments/assets/3714f73f-f618-4517-8f12-620752924d20)
* A basic window for configuring the POMT on a local machine
* Determine required directory locations
* Determine active templates
* Specify report and label printers
