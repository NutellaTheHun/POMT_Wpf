using Petsi.Interfaces;
using Petsi.Reports;

namespace Petsi.CommandLine
{
    public class ReportDirectorFrameBehavior : FrameBehaviorBase
    {
        ReportDirector director;
        public ReportDirectorFrameBehavior(ReportDirector rd)
        {
            director = rd;
        }
        public override Task Actions(Stack<ICommandable> contextChain, string actionIdentifier)
        {
            if (actionIdentifier == null) { return Task.CompletedTask; }

            bool isRange = false, isAll = false;
            DateTime firstDt = default(DateTime), lastDt = default(DateTime);

            string[] args = actionIdentifier.ToLower().Split(' ');

            if (args[0] == "back"){ contextChain.Pop(); return Task.CompletedTask; }

            //At minimum all commands have two strings.
            else if (args.Length < 2) { Console.WriteLine("Invalid command, needs more input."); return Task.CompletedTask; }

            //all requires 2 strings "all <0|1|2|3>"
            else if (args[0] == "all"){ isAll = true;}

            //range requires 4 strings "range <0|1|2|3> <mm/dd/yyyy> <mm/dd/yyyy>"
            else if (args[0] == "range")
            {
                if (args.Length < 4) { Console.WriteLine("Invalid command, needs more input."); return Task.CompletedTask; ; }
                if (!DateTime.TryParse(args[2], out firstDt)) { Console.WriteLine("Invalid first date: " + args[2]); return Task.CompletedTask; }
                if (!DateTime.TryParse(args[3], out lastDt)) { Console.WriteLine("Invalid second date: " + args[3]); return Task.CompletedTask;}
                isRange = true;
            }
            //standard target day requires 2 strings "<0|1|2|3> <mm/dd/yyyy>"
            else
            {
                if(!DateTime.TryParse(args[1], out firstDt)) { Console.WriteLine("Invalid first date: " + args[1]); return Task.CompletedTask; }
            }

            if(isAll)
            {  
                switch (args[1])
                {
                    case "0":
                        director.CreateFrontList(null);
                        break;

                    case "1":
                        director.CreateBackList(null,null);
                        break;

                    case "2":
                        director.CreateWsDay(null);
                        break;

                    case "3":
                        director.CreateWsDayName(null);
                        break;

                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
                Console.WriteLine("Done.");
            }
            else if(isRange)
            {
                switch (args[1])
                {
                    case "0":
                        //DEl_1 -> pickup_1 -> Del_2 -> pu_2, or Del_1 -> Del_2 -> pu_1?
                        //director.CreateFrontList(firstDt, lastDt);
                        Console.WriteLine("Cannot create frontlist of multiple days at once.");
                        break;

                    case "1":
                        director.CreateBackList(firstDt, lastDt);
                        break;

                    case "2":
                        Console.WriteLine("Cannot create wholesale wsDay of multiple days at once.");
                        //director.CreateWsByDay(omp.WsByDayRange(firstDt, lastDt));
                        break;

                    case "3":
                        Console.WriteLine("Cannot create wholesale wsDayname of multiple days at once.");
                        //director.CreateWsDayName(omp.WsByDayByNameRange(firstDt, lastDt));
                        break;

                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
                Console.WriteLine("Done.");
            }
            else
            {
                switch (args[0])
                {
                    case "0":
                        director.CreateFrontList(firstDt);
                        break;

                    case "1":
                        director.CreateBackList(firstDt, null);
                        break;

                    case "2":
                        director.CreateWsDay(firstDt);
                        break;

                    case "3":
                        director.CreateWsDayName(firstDt);     
                        break;

                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
                Console.WriteLine("Done.");
            }
            return Task.CompletedTask;
        }

        public override void CommandFrameView()
        {
            Console.WriteLine("[0]: Create FrontList");
            Console.WriteLine("[1]: Create Backlist");
            Console.WriteLine("[2]: Create Wholesale Agg");
            Console.WriteLine("[3]: Create Wholesale by account");
            Console.WriteLine("All: Selects all data for report.");
            Console.WriteLine("Range: Selects two dates to select all data inbetween for report.");
            Console.WriteLine("back: go back.");
            Console.WriteLine("\nValid input format:");
            Console.WriteLine("     <all> <0|1|2|3>");
            Console.WriteLine("     <range> <2> <mm/dd/yyyy> <mm/dd/yyyy>");
            Console.WriteLine("     <0|1|2|3> <mm/dd/yyyy>");
            Console.WriteLine("     <back>");
        }

        public override string GetComponentName()
        {
            return "Report Director";
        }
    }
}
