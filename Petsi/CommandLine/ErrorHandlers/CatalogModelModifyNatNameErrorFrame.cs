using Petsi.Interfaces;
using Petsi.Models;
using Petsi.Utils;

namespace Petsi.CommandLine.ErrorHandlers
{
    public class CatalogModelModifyNatNameErrorFrame : CatalogModelFrameBehavior
    {
        string _errorName;
        public CatalogModelModifyNatNameErrorFrame(CatalogModelPetsi cmp, string errorName) : base(cmp)
        {
            _errorName = errorName;
        }

        public override Task Actions(Stack<ICommandable> contextChain, string actionIdentifier)
        {
            string arg;
            PrintModel(null);
            Console.WriteLine("Select item: ");
            arg = Console.ReadLine();
            if (arg != null)
            {
                _cmp.GetItems()[Int32.Parse(arg)].AddNaturalName(_errorName);
            }
            else
            {
                SystemLogger.Log("CatalogModelModifyNatNameErrorFrame: arg is null for item index: errorName: " + _errorName);
            }
            contextChain.Pop();

            return Task.CompletedTask;
        }

        public override void CommandFrameView()
        {
            throw new NotImplementedException();
        }

        public override string GetComponentName()
        {
            return "Catalog Model Handle Error Modify Natural Name";
        }
    }
}
