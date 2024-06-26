﻿using Petsi.Reports.TableBuilder;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Reports.PageBuilder
{
    public class PageBuilderFrontListNotes : PageBuilderBase
    {
        public PageBuilderFrontListNotes(Report report) : base(report)
        {
        }

        public override int GetItemLineCount<T>(T item)
        {
            PetsiOrder order = item as PetsiOrder;
            if (order.Note != "")
            {
                return 1;
            }
            return 0;
        }

        protected override void ConfigureMaxRows()
        {
            maxPageRowCount = 40;
        }

        protected override void ConfigureTables()
        {
            _tables.Add(new TableFrontListNote((7,1), 2, 32));
        }
    }
}
