
using Petsi.Units;
using bli = Petsi.Units.BackListItem;
namespace Petsi.Reports
{
    public static class BacklistPageFormatSelector
    {
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
    }
}
