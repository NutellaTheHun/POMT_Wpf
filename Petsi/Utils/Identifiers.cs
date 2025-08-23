
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
        public const string TEST_ONESHOT_ORDERS = "test_oneShotOrder";
        public const string TEST_PERIODIC_ORDERS = "test_periodicOrder";
        public const string FROZEN_ORDERS = "frozenOrders";

        public const string ORDER_FREQUENCY_WEEKLY = "Weekly";
        public const string ORDER_FREQUENCY_ONE_TIME = "OneTime";

        public const string DELETED_ORDERS = "deletedOrders";

        public const string ORDER_TYPE_SQUARE = "Square";
        public const string ORDER_TYPE_WHOLESALE = "Wholesale";
        public const string ORDER_TYPE_SPECIAL = "Special";
        public const string ORDER_TYPE_RETAIL = "Retail";
        public const string ORDER_TYPE_EZ_CATER = "ezCater";
        public const string ORDER_TYPE_FARMERS = "FarmersMarket";

        public const string ORDER_INPUT_ORIGIN_SQUARE = "squareInput";
        public const string ORDER_INPUT_ORIGIN_USER = "userInput";
        public const string ORDER_INPUT_ORIGIN_EZCATER = "EzInput";

        //The name of the actual instantiated object
        public const string MODEL_ORDERS = "ORDERMODEL";
        public const string TEST_MODEL_ORDERS = "TEST_ORDERMODEL";

        //The name of the actual instantiated object
        public const string MODEL_CATALOG = "CATALOGMODEL";
        public const string TEST_MODEL_CATALOG = "TEST_CATALOGMODEL";

        //The name of the serialized file
        public const string MAIN_MODEL_CATALOG_FILE = "main_catalogmodel";
        public const string MAIN_MODEL_CATALOG_CATEGORIES_FILE = "main_catalog_categories";

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
        public const string CATEGORY_MERCH = "HDB76BYHJ2NCXIZUMTI3WJIB";
        public const string CATEGORY_POTM = "POTM_ID";
        public const string CATEGORY_PARBAKE = "PARBAKE_ID";
        public const string CATEGORY_TAKENBAKE = "CUAIOOBN27MNTBZNQZHLRUT5";
        public const string CATEGORY_BOXED_PASTRY = "2VNB6BHLNWNRVN7GEG3I2JQV";

        public const string MODIFY_NAME_NOTE_CARD = "Add your note in \"notes\" section in checkout";
        public const string BOX_OF_6 = "Box of 6";
        //public const string MODIFY_BOX_OF_6_COOKIES = "BX3UJ6TYAREM3DTNNDNULHIY";
        public const string MODIFY_BOX_OF_6_COOKIES = "ELSHIDHEI7EHVBSFDQAPRYPD";
        public const string MODIFY_BOX_OF_6_SCONES = "SAFPTYUFNUHIUOZCUFLQ6ERI";
        public const string MODIFY_BOX_OF_6_MUFFINS = "P5IQRX4LZ6E5JSJPPWAZMQY4";
        public const string MODIFY_SCONE = "VVJAQO74G3LSXLIFIGKATSZ2";
        public const string BOX_OF_SCONES_VARIATION = "box of scones";
        //public const string BOX_OF_6_BACON_BISCUITS = "A5QLMWSYER6MJXANX5FOE7FE"; //seems to be for chill only
        public const string BOX_OF_6_BACON_BISCUITS = "UN7KTRBERK2OWISHWS5LOKWT"; //seems to be for chill only
        public const string BOX_OF_6_BLUEBERRY_MUFFINS = "DZEF5HUCVC5LFXIYPMUPKSEM"; //seems to be for chill only
        public const string VALENTINES_CUTIE_BOX = "QZO55PUFPFOT4XP64KKAYV3W";

        public const string BREAKFAST_PASTRY_PLATTER = "G2OMEAX57DYUJ65S5GUOJFUL";
        public const string COFFEE_BREAK_PLATTER = "3TG4Z4RS6K6SQ5LLDALUBQYI";
        public const string MINI_SCONE_SAMPLER = "BDFUYW5QD7Q7ORC42MYY67KE";
        public const string MUFFIN_SAMPLER = "6IFQR5IXYAJYF2NB5VWWJJAR";

        public const string MINI_CURRANT = "userbased-6774ee7d-90b6-49d7-8e20-716c118535ad";
        public const string CURRANT_SCONE = "userbased-fe4ca24e-7236-433d-8d15-308ebe601f00";

        public const string MINI_LEMON = "userbased-f8c8b566-8711-4d51-8165-50e79bbe9a86";
        public const string LEMON_SCONE = "userbased-ab7383e1-38f0-46f2-9abc-403a8ddaf538";

        public const string MINI_TRIPLE = "userbased-2feced77-544d-4b20-bdf5-9b683c2f95a0";
        public const string BERRY_SCONE = "userbased-18b89386-745e-44d0-90cd-fd68f9895bf2";

        public const string MINI_BLUE_MUFFIN = "userbased-9cf79d69-5c60-43c0-9626-11115035106c";
        public const string BLUE_MUFFIN = "N6XRIRRLZB5Q6LKOGC4ZNZ3W";

        public const string MINI_BANANA_MUFFIN = "userbased-67470a07-fa80-479e-aa0f-47eac0b9106d";
        public const string BANANA_MUFFIN = "CGYIVCSBBIXQHLMNOP6A45X7";

        public const string DONUT = "TDQ5KTVDPVRZUAGPLPNAOXTE";
        public const string COFFEE_CAKE = "userbased-ab8da8e0-953b-4362-9b21-c7d43d55c5a3";

        public const string MINI_CC_CHIP = "userbased-bb596cee-0bbf-4a6f-9b5a-5405a16c6c2a";
        public const string CC_COOKIE = "RJJHBDTUMENI2DLHQAYRDPUB";

        public const string MINI_SNICK = "userbased-5f3df262-6899-4554-b15e-32cc75ef9e3b";
        public const string SNICK_COOKIE = "CXNP6DDA2TSYGJQPULZEPPK5";

        public const string MINI_GINGER = "userbased-459f3cf5-cf10-4456-9b4c-8bd46c7d5588";
        public const string GING_COOKIE = "KUJVD7J45N4BG4XGMVHYN7UK";


        public const string WALNUT_BROWNIE = "userbased-7502c513-6f9e-43ce-afd0-0047addd3968";
        public const string RASP_SWIRL_BAR = "userbased-cf183af2-0016-4447-abdf-6e9b318b5270";
        public const string MINI_TEACAKE = "userbased-29a88e7a-1505-4028-bb30-aac3f0694ed3";
        
        public const string CORN_RASP_MUFFIN = "2XFLPIDBCKFVMO4MRAQHELJW";
        

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
        public const string SETTING_BACKUP_PATH = "backupPath";
        public const string SETTING_STARTUP = "startUp";
        public const string SETTING_STARTUP_STATUS = "startUpStatus";
        public const string SETTING_STARTUP_STATUS_PENDING = "startUpPending";
        public const string SETTING_STARTUP_STATUS_INIT = "startUpInit";
        public const string SETTING_STARTUP_STATUS_NEUTRAL = "startUpNeutral";
        public const string SETTING_ERROR_LOG_PATH = "errorLog";
        public const string SETTING_ROOT_DIR = "rootDirectory";
        public const string SETTING_CURRENT_DATE = "CURRENT_DATE";

        public const string PETSI_CONFIG = "petsiConfig.txt";
     

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
