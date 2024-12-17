using ClosedXML.Excel;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Reports.DeliveryBuilder
{
    public class DeliverySheetBuilder
    {
        private Report _report;
        public DeliverySheetBuilder(Report report)
        {
            _report = report;
        }

        public IXLWorkbook BuildDeliveryPages(List<PetsiOrder> orders)
        {
            int orderCount = 0;
            foreach (PetsiOrder order in orders)
            {
                if (order.FulfillmentType == Identifiers.FULFILLMENT_DELIVERY && order.OrderType != Identifiers.ORDER_TYPE_WHOLESALE)
                {
                    orderCount++;
                    BuildDeliveryPage(_report.Wb, order, orderCount);
                }
            }

            return _report.Wb;
        }

        private void BuildDeliveryPage(IXLWorkbook deliveryPages, PetsiOrder order, int orderCount)
        {
            if (orderCount % 2 != 0)
            {
                IXLWorksheet page = deliveryPages.Worksheets.Add();
                BuildDeliveryTable(0, 0, page, order);
                //AddSeparation Line bottom of row 15
            }
            else
            {
                BuildDeliveryTable(15, 1, deliveryPages.Worksheets.Last(), order);
            }
            
        }

        private void BuildDeliveryTable(int rootRow, int rootCol, IXLWorksheet page, PetsiOrder order)
        {

            //Logo
            page.Range(GetLocalCell(1, 1), GetLocalCell(2, 3)).Merge();
            var imagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "petsiDir\\images\\petsiLogo.png");
            var iamge = page.AddPicture(imagePath)
                .MoveTo(GetLocalCell(1, 1));

            //4,1: Font 14, Bold, val="Day:"
            GetLocalCell(4, 1).Value = "Day:";
            GetLocalCell(4, 1).Style.Font.SetBold(true);
            GetLocalCell(4, 1).Style.Font.SetFontSize(14);

            //4,2: font size 14, left aligned, order.fulfillmentDate.ToDatOfWeek()
            GetLocalCell(4, 2).Value = DateTime.Parse(order.OrderDueDate).DayOfWeek.ToString();
            GetLocalCell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
            GetLocalCell(4, 2).Style.Font.SetFontSize(14);

            //5,1: Font 14, Bold, val="Date:"
            GetLocalCell(5, 1).Value = "Date:";
            GetLocalCell(5, 1).Style.Font.SetBold(true);
            GetLocalCell(5, 1).Style.Font.SetFontSize(14);

            //5,2: Font size 14, left aligned val=order.fulfillmentDate.ToShortDateString()
            GetLocalCell(5, 2).Value = DateTime.Parse(order.OrderDueDate).ToShortDateString();
            GetLocalCell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
            GetLocalCell(5, 2).Style.Font.SetFontSize(14);

            //7,1: Font size 14, bold, val="Order Contact:"
            GetLocalCell(7, 1).Value = "Order Contact:";
            GetLocalCell(7, 1).Style.Font.SetBold(true);
            GetLocalCell(7, 1).Style.Font.SetFontSize(14);

            //8,2: Font size 14, val=order.Name
            GetLocalCell(8, 2).Value = order.Recipient;
            GetLocalCell(8, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
            GetLocalCell(8, 2).Style.Font.SetFontSize(14);

            //7,5: Font size 14, bold, val="Delivery Address"
            GetLocalCell(7, 5).Value = "Delivery Address";
            GetLocalCell(7, 5).Style.Font.SetBold(true);
            GetLocalCell(7, 5).Style.Font.SetFontSize(14);

            //8,5: Font size 14, TextWrap, Merge to (10,7), val=order.DeliveryAddress
            GetLocalCell(8, 5).Value = order.DeliveryAddress;
            GetLocalCell(8, 5).Style.Font.SetFontSize(14);
            GetLocalCell(8, 5).Style.Alignment.WrapText = true;
            page.Range(GetLocalCell(8, 5), GetLocalCell(10, 7)).Merge();


            //9,1: Font size 14, bold, val="Phone:"
            GetLocalCell(9, 1).Value = "Phone:";
            GetLocalCell(9, 1).Style.Font.SetBold(true);
            GetLocalCell(9, 1).Style.Font.SetFontSize(14);

            //10,2: font size 14, merge cell (10,3), left align, val=order.number
            GetLocalCell(10, 2).Value = order.PhoneNumber;
            GetLocalCell(10, 2).Style.Font.SetFontSize(14);
            page.Range(GetLocalCell(10, 2), GetLocalCell(10, 3)).Merge();
            GetLocalCell(10, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

            //11,1: Font size 14, bold, val="Instructions"
            GetLocalCell(11, 1).Value = "Instructions:";
            GetLocalCell(11, 1).Style.Font.SetBold(true);
            GetLocalCell(11, 1).Style.Font.SetFontSize(14);

            //12,1: Font size 14, merge (14,7), val=order.notes?
            GetLocalCell(12, 1).Value = order.Note;
            GetLocalCell(12, 1).Style.Font.SetFontSize(14);
            GetLocalCell(12, 1).Style.Alignment.WrapText = true;
            page.Range(GetLocalCell(12, 1), GetLocalCell(14, 7)).Merge();

            IXLCell GetLocalCell(int row, int col) 
            {
                return page.Cell(row+rootRow, col+rootCol);
            }
        }
    }
}
