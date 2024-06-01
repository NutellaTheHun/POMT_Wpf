using Petsi.Interfaces;
using Petsi.Managers;
using Petsi.Models;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.CommandLine.UnitBuilders
{
    public class PetsiOrderUnitBuilder : UnitBuilderBase
    {
        OrderModelPetsi orderModel;
        ICatalogService catalogService;
      
        string Recipient;
        string OrderDueDate;
        string FulfillmentType;
        string Note;
        List<PetsiOrderLineItem> lineItems;
        int step;

        public PetsiOrderUnitBuilder(OrderModelPetsi model)
        {
            orderModel = model;
            catalogService = (ICatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            lineItems = new List<PetsiOrderLineItem>();
            step = 0;
        }
        public override Task Actions(Stack<ICommandable> contextChain, string actionIdentifier)
        {
            string input;
            while (step < 6)
            {
                Console.Clear();
                CommandFrameView();
                switch (step)
                {
                    case 0:
                        Console.Write("Recipient Name: ");
                        Recipient = Console.ReadLine();
                        step++;
                        break;
                    case 1:
                        Console.Write("Order Due Date mm/dd/yyyy: ");
                        input = Console.ReadLine();
                        if (input == "back")
                        {
                            step--;
                        }
                        else if(ParseDate(input, out OrderDueDate))
                        {
                            step++;
                        }
                        break;
                    case 2:
                        Console.WriteLine("[0] Pickup");
                        Console.WriteLine("[1] Delivery");
                        Console.Write("Select fulfillment Type index:");
                        input = Console.ReadLine();
                        switch (input)
                        {
                            case "0":
                                FulfillmentType = Identifiers.FULFILLMENT_PICKUP;
                                step++;
                                break;
                            case "1":
                                FulfillmentType = Identifiers.FULFILLMENT_DELIVERY;
                                step++;
                                break;
                            case "back":
                                step--;
                                break;
                            default:
                                Console.WriteLine("Invalid input.");
                                break;
                        }
                        break;
                    case 3:
                        Console.WriteLine("Write a note if desired");
                        Console.Write("Note: ");
                        input = Console.ReadLine();
                        if(input == "back")
                        {
                            step--;
                        }
                        else
                        {
                            Note = input;
                            step++;
                        }
                        break;
                    case 4:
                        Console.WriteLine("Type \"add\" to add order items:");
                        Console.WriteLine("Type \"done\" when finished");
                        input = Console.ReadLine().ToLower();
                        switch(input)
                        {
                            case "add":
                                lineItems.Add(ParseLineItem());
                                break;
                            case "done":
                                step++;
                                break;
                            case "back":
                                step--;
                                break;
                            default:
                                Console.WriteLine("Oops PetsiOrder LineItem step error");
                                break;
                        }
                        break;
                    case 5:
                        orderModel.AddOrder(new PetsiOrder(
                            "frame_input",
                            Recipient,
                            catalogService.GenerateCatalogId(), //GenerateCatalogId Method's output isnt unqique to the catalog.
                            OrderDueDate,
                            FulfillmentType,
                            Note,
                            lineItems));
                        contextChain.Pop();
                        step++;
                        break;
                    default:
                        Console.WriteLine("Oops Petsi Order builder step error");
                        break;
                }
            }
            return Task.CompletedTask;
        }

        private bool ParseDate(string input, out string date)
        {
            DateTime result;
            if(DateTime.TryParse(input, out result))
            {
                date = result.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK");
                return true;
            }
            Console.WriteLine("Invalid input: " + input);
            date = "";
            return false;
        }

        public override void CommandFrameView()
        {
            Console.WriteLine("       Recipient: " + Recipient);
            Console.WriteLine("  Order Due Date: " + OrderDueDate);
            Console.WriteLine("Fulfillment Type: " +  FulfillmentType);
            Console.WriteLine("            Note: " + Note);
            PrintLineItems();
        }

        public override string GetComponentName()
        {
            return "Petsi Order Unit Builder";
        }
        private void PrintLineItems()
        {
            foreach(PetsiOrderLineItem lineItem in lineItems)
            {
                Console.WriteLine("        Item Name: " + lineItem.ItemName);
                Console.WriteLine("       Catalog Id: " + lineItem.CatalogObjectId);
                Console.WriteLine("         Amount 3: " + lineItem.Amount3);
                Console.WriteLine("         Amount 5: " + lineItem.Amount5);
                Console.WriteLine("         Amount 8: " + lineItem.Amount8);
                Console.WriteLine("        Amount 10: " + lineItem.Amount10);
            }
        }
        private PetsiOrderLineItem ParseLineItem()
        {
            string input;
            int step = 0;
            string ItemName = "NULL", CatalogObjectId = "NULL";
            int Amount3 = 0, Amount5 = 0, Amount8 = 0, Amount10 = 0, AmountRegular = 0;
            while (step < 5)
            {
                switch (step)
                {
                    case 0:
                        Console.Write("Item Name: ");
                        input = Console.ReadLine();
                        if(ConfirmItemName(input, out ItemName, out CatalogObjectId))
                        {
                            step++;
                        }
                        break;
                    case 1:
                        Console.Write("Amount 3\": ");
                        input = Console.ReadLine();
                        if(input == "back") { step--; }
                        else if(int.TryParse(input, out Amount3)){ step++; }
                        else{ Console.WriteLine("Invalid input: " + input); }
                        break;
                    case 2:
                        Console.Write("Amount 5\": ");
                        input = Console.ReadLine();
                        if (input == "back") { step--; }
                        else if (int.TryParse(input, out Amount5)) { step++; }
                        else { Console.WriteLine("Invalid input: " + input); }
                        break;
                    case 3:
                        Console.Write("Amount 8\": ");
                        input = Console.ReadLine();
                        if (input == "back") { step--; }
                        else if (int.TryParse(input, out Amount8)) { step++; }
                        else { Console.WriteLine("Invalid input: " + input); }
                        break;
                    case 4:
                        Console.Write("Amount 10\": ");
                        input = Console.ReadLine();
                        if (input == "back") { step--; }
                        else if (int.TryParse(input, out Amount10)) { step++; }
                        else { Console.WriteLine("Invalid input: " + input); }
                        break;
                    default:
                        Console.WriteLine("Opps line item step error");
                        break;
                }
            }
            return new PetsiOrderLineItem(ItemName, CatalogObjectId, Amount3, Amount5, Amount8, Amount10, AmountRegular);
        }
        private bool ConfirmItemName(string targetName, out string ItemName, out string CatalogObjectId)
        {
            List<string> nameResult = catalogService.NameContains(targetName);
            string input;
            int num;
            if (nameResult.Count == 0)
            {
                Console.WriteLine("Name not found in catalog.");
                ItemName = "";
                CatalogObjectId = "";
                return false;
            }
            else
            {
                int i = 0;
                foreach (string name in nameResult)
                {
                    Console.WriteLine("[" + i + "] " + name);
                    i++;
                }
                Console.WriteLine("Please confirm selection by entering index: ");

                input = Console.ReadLine();

                num = int.Parse(input);
                if (num < 0 || num > nameResult.Count - 1)
                {
                    Console.WriteLine("Invalid input");
                    ItemName = "";
                    CatalogObjectId = "";
                    return false;
                }
            }
            ItemName = nameResult[num];
            CatalogObjectId = catalogService.GetCatalogObjectId(ItemName);
            return true;
        }
    }
}
