
using Petsi.Models;
using Petsi.Utils;

namespace Petsi.Services
{
    public class ErrorService : ServiceBase
    {
        public ErrorService()
        {
            SetServiceName(Identifiers.SERVICE_ERROR);
        }
        public override void Update(ModelBase model)
        {
            throw new NotImplementedException();
        }
    }
}
