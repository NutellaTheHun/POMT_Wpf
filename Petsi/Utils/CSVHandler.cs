using CsvHelper;
using Petsi.Interfaces;
using Petsi.Units;
using System.Globalization;

namespace Petsi.Util
{
    public class CSVHandler : IWholesaleChannelInput
    {
        string filepath;
        public static int wholesaleLinesProcessed;
        public CSVHandler(string filepath)
        {
            this.filepath = filepath;
            wholesaleLinesProcessed = 0;
        }

        public List<WholesaleItem> LoadWholesaleData()
        {
            List<WholesaleItem> result = new List<WholesaleItem>();

            using (var reader = new StreamReader(filepath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = csv.GetRecord<WholesaleItem>();
                    result.Add(record);
                    wholesaleLinesProcessed++;
                }
            }
            foreach(WholesaleItem item in result)
            {
                item.WholesaleName = char.ToUpper(item.WholesaleName[0]) +item.WholesaleName.Substring(1);
            }
            return result;
        }
    }
}
