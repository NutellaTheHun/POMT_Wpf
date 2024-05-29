using Petsi.CommandLine.UnitBuilders;
using Petsi.Interfaces;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Utils;

namespace Petsi.CommandLine.ErrorHandlers
{
    public class CatalogServiceErrorFrameBehavior : ErrorHandlerBase
    {
        /// <summary>
        /// The name of input from ValidateModifyName that failed.
        /// </summary>
        string errorName;

        bool isCreateNew;
        public CatalogServiceErrorFrameBehavior(string name) { errorName = name; }
        public override Task Actions(Stack<ICommandable> contextChain, string actionIdentifier)
        {
            Console.WriteLine("No catalog match for modifier item: " + errorName);
            Console.WriteLine("Add to an existing item [0] or add to an existing?");
            Console.WriteLine("Create a new catalog item [1]?");
            int input;
            int.TryParse(Console.ReadLine(), out input);
            isCreateNew = input == 1;
            if(isCreateNew)
            {
                contextChain.Push(
                    new CatalogItemUnitBuilder(
                        (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG), errorName));
                contextChain.Peek().Actions(contextChain, "");
            }
            else
            {
                contextChain.Push(
                    new CatalogModelModifyNatNameErrorFrame(
                        (CatalogModelPetsi)ModelManagerSingleton.GetInstance().GetModel(Identifiers.MODEL_CATALOG), errorName));
                contextChain.Peek().Actions(contextChain, "");
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
            return "Catalog Service Error Handler";
        }
    }
}
