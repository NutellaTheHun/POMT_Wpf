using Petsi.Utils;

namespace Petsi.Units
{
    public class WholesaleItem
    {
        public string WholesaleName { get; set; }
        public string Day { get; set; }
        public string CatalogObjectId { get; set; }
        public string ItemName { get; set; }
        public int Amount3 { get; set; }
        public int Amount5 { get; set; }
        public int Amount8 { get; set; }
        public int Amount10 { get; set; }

        public WholesaleItem() { }

        public PetsiOrder ToOnOrderItemHeader()
        {
            return new PetsiOrder(
                 Identifiers.WHOLESALE_INPUT,
                 WholesaleName,
                 WholesaleName + "-" + Day,
                 DayOfWeekToRFC3339(Day),
                 "wholesale",
                 ""
                );
        }

        public PetsiOrderLineItem ToOnOrderLineItem()
        {
            return new PetsiOrderLineItem(
                ItemName.ToLower(),
                CatalogObjectId,
                Amount3,
                Amount5,
                Amount8,
                Amount10,
                0
                );
        }

        public override string ToString()
        {
            return string.Format("Wholesale name: " + WholesaleName + ", day: " + Day + ", id: " + CatalogObjectId +
                ", item: " + ItemName + ", 3\": " + Amount3 + ", 5\": " + Amount5 + ", 8\": " + Amount8 + ", 10\": " + Amount10);
        }

        public string DayOfWeekToRFC3339(string wsInputDay)
        {
            return GetDateTimeFromDayOfWeek(wsInputDay).ToString("yyyy-MM-dd'T'HH:mm:ss.fffK");
        }
        static DayOfWeek GetDayOfWeekFromString(string dayOfWeekString)
        {
            switch (dayOfWeekString.ToLower())
            {
                case "sun":
                    return DayOfWeek.Sunday;
                case "mon":
                    return DayOfWeek.Monday;
                case "tue":
                    return DayOfWeek.Tuesday;
                case "wed":
                    return DayOfWeek.Wednesday;
                case "thu":
                    return DayOfWeek.Thursday;
                case "fri":
                    return DayOfWeek.Friday;
                case "sat":
                    return DayOfWeek.Saturday;
                default:
                    throw new ArgumentException("Invalid day of the week string", nameof(dayOfWeekString));
            }
        }
        static DateTime GetDateTimeFromDayOfWeek(string dayOfWeekString)
        {
            // Get the current day of the week
            DayOfWeek dayOfWeek = GetDayOfWeekFromString(dayOfWeekString);

            // Calculate the number of days to subtract to get to the desired day
            int daysToSubtract = (int)DateTime.Now.DayOfWeek - (int)dayOfWeek;
            if (daysToSubtract < 0)
                daysToSubtract += 7;

            // Subtract the days from the current date to get the desired day
            DateTime desiredDate = DateTime.Now.AddDays(-daysToSubtract);

            return desiredDate;
        }
    }
}
