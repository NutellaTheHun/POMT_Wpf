using Petsi.Filing;
using Petsi.Units;
using Petsi.Utils;
using bli = Petsi.Units.BackListItem;
namespace Petsi.Reports
{
    public class BacklistTemplateFormatSelector
    {
        private static BacklistTemplateFormatSelector _instance;
        private List<(string name, List<bli> template)> templates;
        private FileBehavior fileBehavior;
        private BacklistTemplateFormatSelector()
        {
            fileBehavior = new FileBehavior("BackListTemplates");
            //templates = new List<(string name, List<bli> template)>();
            InitTemplates();
        }
        public static BacklistTemplateFormatSelector GetInstance()
        {
            if(_instance == null)
            {
                _instance = new BacklistTemplateFormatSelector();
            }
            return _instance;
        }
        public List<bli> GetPieFormat()
        {
            string targetName = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_PIE_TEMPLATE);
            return GetTemplate(targetName);
        }
        public List<bli> GetPastryFormat()
        {
            string targetName = PetsiConfig.GetInstance().GetVariable(Identifiers.SETTING_PASTRY_TEMPLATE);
            return GetTemplate(targetName);
        }

        public void AddTemplate(string name, List<bli> template)
        {
            templates.Add((name, template));
        }

        public void RemoveTemplate(string name, List<bli> template)
        {
            templates.Remove((name, template));
        }

        private List<bli> GetTemplate(string targetName)
        {
            var element = templates.FirstOrDefault(x => x.name == targetName);
            return element.template;
        }

        public List<(string name, List<bli> template)> GetTemplates()
        {
            return templates;
        }

        private void InitTemplates()
        {
            templates = fileBehavior.BuildDataListFile<(string, List<bli>)>("templates");
            if(templates == null)
            {
                templates = new List<(string name, List<bli> template)>
                {
                    BootSummerFormat(),
                    BootPieFormat(),
                    BootPastryFormat()
                };
                fileBehavior.DataListToFile("templates", templates);
            }
        }

        public (string name,List<BackListItem> template) BootSummerFormat()
        {

            return("Summer Pies", new List<BackListItem>
            {
                bli.MUD(),
                bli.CBP(),
                bli.PECAN(),
                bli.MIX(),
                bli.CHERRY(),
                bli.APP_CRUMB(),
                bli.APPLE(),
                bli.PEACH(),
                bli.PEACH_BLACK(),
                bli.BLUE(),
                bli.LEMON_CHESS_LAV(),
                bli.KEY_LIME(), 
                bli.BACON(),
                bli.MOZZ(),
                bli.JALAPENO(),
                bli.POTM()
            });
            
        }

        public (string name, List<BackListItem> template) BootPieFormat()
        {
            return ("Standard Pies", new List<BackListItem>
            {
                bli.MUD(),
                bli.CBP(),
                bli.PECAN(),
                bli.MIX(),
                bli.CHERRY(),
                bli.APP_CRUMB(),
                bli.APPLE(),
                bli.BLUE(),
                bli.STRAWBARB(),
                bli.STRAWOAT(),
                bli.LEMON_CHESS_LAV(),
                bli.KEY_LIME(),
                bli.POTM(),
                bli.BACON(),
                bli.POTATO(),
                bli.ARTICHOKE(),
                bli.HAM(),
                bli.PARBAKES(),
                bli.STUFF_BRIOCHE(),
                bli.GARLIC_BRIOCHE(),
                bli.ALMOND_BRIOCHE()
            });

        }

        public (string name, List<BackListItem> template) BootPastryFormat()
        {
            return ("Standard Pastry", new List<BackListItem>
            {
                bli.CURRANT(),
                bli.LEMON_SCONE(),
                bli.TRIPLE(),
                bli.BISCUIT(),
                bli.MUFFINS(),
                bli.BRIOCHE(),
                bli.BUNS(),
                bli.CCHIP(),
                bli.SNICK(),
                bli.PB(),
                bli.GINGER(),
                bli.BSA(),
                bli.OAT(),
                bli.MOCHA()
            });
        }
    }
}
/*
     public static List<BackListItem> GetSummerFormat()
        {
            return new List<BackListItem>
            {
                bli.MUD(),
                bli.CBP(),
                bli.PECAN(),
                bli.MIX(),
                bli.CHERRY(),
                bli.APP_CRUMB(),
                bli.APPLE(),
                bli.PEACH(),
                bli.PEACH_BLACK(),
                bli.BLUE(),
                bli.LEMON(),
                bli.KEY_LIME(), 
                bli.BACON(),
                bli.MOZZ(),
                bli.JALAPENO(),
                bli.VEGAN(),
                bli.POTM()
            };
            
        }

        public static List<BackListItem> GetStandardFormat()
        {
            return new List<BackListItem>
            {
                bli.MUD(),
                bli.CBP(),
                bli.PECAN(),
                bli.MIX(),
                bli.CHERRY(),
                bli.APP_CRUMB(),
                bli.APPLE(),
                bli.BLUE(),
                bli.STRAWBARB(),
                bli.STRAWOAT(),
                bli.VEGAN(),
                bli.LEMON(),
                bli.KEY_LIME(),
                bli.POTM(),
                bli.BACON(),
                bli.POTATO(),
                bli.ARTICHOKE(),
                bli.HAM(),
                bli.PARBAKES(),
                bli.STUFF_BRIOCHE(),
                bli.GARLIC_BRIOCHE(),
                bli.ALMOND_BRIOCHE()
            };

        }

        public static List<BackListItem> GetPastryFormat()
        {
            return new List<BackListItem>
            {
                bli.CURRANT(),
                bli.LEMON(),
                bli.TRIPLE(),
                bli.BISCUIT(),
                bli.MUFFINS(),
                bli.BRIOCHE(),
                bli.BUNS(),
                bli.CCHIP(),
                bli.SNICK(),
                bli.PB(),
                bli.GINGER(),
                bli.BSA(),
                bli.OAT(),
                bli.MOCHA()
            };
        }
 */