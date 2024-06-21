
using Petsi.Models;
using Petsi.Utils;

namespace Petsi.Services
{
    public class ErrorService : ServiceBase
    {
        public static ErrorService instance;


        public delegate void TableBuilderOverflowEvent(object sender, EventArgs e);

        public event TableBuilderOverflowEvent TBOverflow;


        public delegate void SquareOrderInputNewItemEvent(object sender, EventArgs e);

        public event SquareOrderInputNewItemEvent SoiNewItem;

        private ErrorService()
        {
            SetServiceName(Identifiers.SERVICE_ERROR);
        }

        public static ErrorService Instance()
        {
            if(instance == null) { instance = new ErrorService(); }
            return instance;
        }

        public override void Update(ModelBase model)
        {
            throw new NotImplementedException();
        }

        public void RaiseTBOverflowEvent()
        {
            TBOverflow?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseSoiNewItemEvent()
        {
            SoiNewItem?.Invoke(this, EventArgs.Empty);
        }
    }
}
