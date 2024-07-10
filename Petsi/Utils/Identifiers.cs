
namespace Petsi.Utils
{
    public static class Identifiers
    {
        public const string SQUARE_ORDER_INPUT = "SquareOrderInput";
        public const string WHOLESALE_INPUT = "WholesaleInput";
        public const string SQUARE_CATALOG_INPUT = "SquareCatalogInput";
        public const string ONE_SHOT_INPUT = "OneShotInput";
        public const string USER_ENTERED_INPUT = "UserEntered";
        public const string PERIODIC_ORDERS = "periodicOrder";
        public const string ONE_SHOT_ORDERS = "oneShotOrder";
        public const string FROZEN_ORDERS = "frozenOrders";

        public const string ORDER_FREQUENCY_WEEKLY = "Weekly";
        public const string ORDER_FREQUENCY_ONE_TIME = "OneTime";

        public const string DELETED_ORDERS = "deletedOrders";

        public const string ORDER_TYPE_SQUARE = "Square";
        public const string ORDER_TYPE_WHOLESALE = "Wholesale";
        public const string ORDER_TYPE_SPECIAL = "Special";
        public const string ORDER_TYPE_RETAIL = "Retail";
        public const string ORDER_TYPE_EZ_CATER = "ezCater";

        public const string ORDER_INPUT_ORIGIN_SQUARE = "squareInput";
        public const string ORDER_INPUT_ORIGIN_USER = "userInput";
        public const string ORDER_INPUT_ORIGIN_EZCATER = "EzInput";

        public const string MODEL_ORDERS = "ORDERMODEL";

        public const string MODEL_CATALOG = "CATALOGMODEL";
        public const string MAIN_MODEL_CATALOG_FILE = "main_catalogmodel";

        public const string FULFILLMENT_PICKUP = "PICKUP";
        public const string FULFILLMENT_DELIVERY = "DELIVERY";
        public const string FULFILLMENT_WHOLESALE = "wholesale";

        public const string WS_DAY_SUN = "sun";
        public const string WS_DAY_MON = "mon";
        public const string WS_DAY_TUE = "tue";
        public const string WS_DAY_WED = "wed";
        public const string WS_DAY_THU = "thu";
        public const string WS_DAY_FRI = "fri";
        public const string WS_DAY_SAT = "sat";

        public const string SIZE_CUTIE = "cutie";
        public const string SIZE_SMALL = "Small";
        public const string SIZE_MEDIUM = "Medium";
        public const string SIZE_LARGE = "Large";
        public const string SIZE_REGULAR = "Regular";

        public const string CATEGORY_PIE = "GNF2D3XZMZBO2R3ZDHHLTELC";
        public const string CATEGORY_FROZEN_PIE = "CUAIOOBN27MNTBZNQZHLRUT5";
        public const string CATEGORY_PASTRY = "RJHRRYGCM4CMJQ4L57KP4JK5";
        public const string CATEGORY_DRINKS = "TPSEG2FP3QTC24PUTRQAUAEE";
        public const string CATEGORY_BOXED_TEA_PLATTERS = "PCM22LM7KMUAUOPP7MKQNRBL";
        public const string CATEGORY_MERCH_ICECREAM = "HDB76BYHJ2NCXIZUMTI3WJIB";
        public const string CATEGORY_POTM = "POTM_ID";

        public const string MODIFY_NAME_NOTE_CARD = "Add your note in \"notes\" section in checkout";
        public const string BOX_OF_6 = "Box of 6";
        public const string MODIFY_BOX_OF_6_COOKIES = "BX3UJ6TYAREM3DTNNDNULHIY";
        public const string MODIFY_BOX_OF_6_SCONES = "YOSF3YAHQIIWSYC7SRD4NSZI";
        public const string MODIFY_BOX_OF_6_MUFFINS = "3PISTAMARHW6Q5CWUO3GPE5S";
        public const string MODIFY_SCONE = "UFVOR7Y5GMDEATE32T4M2IAI";
        public const string BOX_OF_SCONES_VARIATION = "box of scones";
        public const string BOX_OF_6_BACON_BISCUITS = "A5QLMWSYER6MJXANX5FOE7FE"; //seems to be for chill only
        public const string BOX_OF_6_BLUEBERRY_MUFFINS = "DZEF5HUCVC5LFXIYPMUPKSEM"; //seems to be for chill only

        //Temporary ID for an item when a multiItem match event occurs, is resolved after user intervention window. (NotifyCatalogValidateMultiItemView.cs)
        public const string SOI_MULTI_ITEM_MATCH_EVENT_ID_SIG = "%FIND_NEW_ID"; 
        public const string SOI_NEW_ITEM_EVENT_ID_SIG = "%NEW_ITEM_ID"; 

        public const string SERVICE_CATALOG = "CATALOG_SERVICE";
        public const string SERVICE_CATEGORY = "CATEGORY_SERVICE";
        public const string SERVICE_LABEL = "LABEL_SERVICE";
        public const string SERVICE_TEMPLATE = "TEMPLATE_SERVICE";
        public const string SERVICE_ERROR = "ERROR_SERVICE";

        public const string USER_BASED_ID_TAG = "userbased-";

        public const string ENV_SCI = "env_square_catalog_input";
        public const string ENV_SOI = "env_square_order_input";
        public const string ENV_WSI = "env_wholesale_input";
        public const string ENV_OSI = "env_oneShot_input";
        public const string ENV_OMP = "env_order_model";
        public const string ENV_CMP = "env_catalog_model";
        public const string ENV_CATA_S = "env_catalog_serv";
        public const string ENV_CATE_S = "env_category_serv";

        public const string SETTING_LABEL_PRINTER = "labelPrinter";
        public const string SETTING_STD_PRINTER = "standardPrinter";
        public const string SETTING_PIE_TEMPLATE = "pieTemplateName";
        public const string SETTING_PASTRY_TEMPLATE = "pastryTemplateName";
        public const string SETTING_LABEL_FP = "labelDirectory";
        public const string SETTING_DAYNUM = "environPeriod";
        public const string SETTING_FILESERVICE_PATH = "fileServicePath";
        public const string SETTING_ENVIRON_PATH = "environFileServicePath";
        public const string SETTING_REPORT_CNT_PATH = "reportCountPath";
        public const string SETTING_REPORT_EXPORT_PATH = "createdReportPath";
        public const string SETTING_CUTIE_LBL_PATH = "cutieDirectory";
        public const string SETTING_PIE_LBL_PATH = "standardDirectory";
        public const string SETTING_SQUARE = "squareKey";
        public const string SETTING_STARTUP = "startUp";
        public const string SETTING_STARTUP_STATUS = "startUpStatus";
        public const string SETTING_STARTUP_STATUS_PENDING = "startUpPending";
        public const string SETTING_STARTUP_STATUS_INIT = "startUpInit";
        public const string SETTING_STARTUP_STATUS_NEUTRAL = "startUpNeutral";

        public static List<string> GetOrderTypes()
        {
            return new List<string>
            {
                ORDER_TYPE_SPECIAL,
                ORDER_TYPE_SQUARE,
                ORDER_TYPE_WHOLESALE
            };
        }
    }
}
