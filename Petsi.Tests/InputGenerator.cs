using Petsi.Interfaces;
using Petsi.Managers;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Tests
{
    public class InputGenerator
    {
        public static List<PetsiOrder> GetTestOrdersMultiDay(List<string> itemIds, List<string> orderTypes, int orderAmountPerType, DateTime start, DateTime end)
        {
            List<PetsiOrder> result = new List<PetsiOrder>();
            for (DateTime date = start; date <= end; date = date.AddDays(1))
            {
                result.AddRange(GetTestOrders(itemIds, orderTypes, orderAmountPerType, date));
            }
            return result;
        }

        public static List<PetsiOrder> GetTestOrders(List<string> itemIds, List<string> orderTypes, int orderAmountPerType, DateTime date)
        {
            List<PetsiOrder> result = new List<PetsiOrder>();

            int orderCount = 0;
            string year = date.Year.ToString();
            string month = date.Month.ToString();
            string day = date.Day.ToString();
            int hour = 8;
            int minute = 10;

            foreach (string orderType in orderTypes)
            {
                for (int i = 0; i < orderAmountPerType; i++)
                {
                    PetsiOrder order = new PetsiOrder();
                    order.OrderType = orderType;
                    order.OrderId = Guid.NewGuid().ToString();

                    order.Recipient = $"{orderType}_{orderCount}";
                    orderCount++;

                    //order.OrderDueDate = $"{year}-{month}-{day}T{hour}:{minute}:00.000Z";
                    order.OrderDueDate = $"{month}/{day}/{year} {hour}:{minute}";
                    hour++;
                    if (hour == 16) { hour = 8; }
                    if (minute == 10) { minute = 30; }
                    else { minute = 10; }

                    if (order.OrderType == "Square")
                    {
                        order.IsOneShot = true;
                        order.IsUserEntered = false;
                        order.FulfillmentType = "PICKUP";
                    }
                    if (order.OrderType == "Special")
                    {
                        order.IsOneShot = true;
                        order.IsUserEntered = true;
                        order.FulfillmentType = "PICKUP";
                    }
                    if (order.OrderType == "Wholesale")
                    {
                        order.IsPeriodic = true;
                        order.IsUserEntered = true;
                        order.FulfillmentType = "DELIVERY";
                    }
                    if (order.OrderType == "FarmersMarket")
                    {
                        order.IsPeriodic = true;
                        order.IsUserEntered = true;
                        order.FulfillmentType = "PICKUP";
                    }

                    order.Email = "email_test";
                    order.DeliveryAddress = "deliveryAddr_test";
                    order.PhoneNumber = "phoneNum_test";

                    order.LineItems = GenerateLineItems(itemIds);
                    result.Add(order);
                }
            }

            return result;
        }

        static List<PetsiOrderLineItem> GenerateLineItems(List<string> itemIds)
        {
            CatalogService cs = (CatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            List<PetsiOrderLineItem> result = new List<PetsiOrderLineItem>();
            foreach (string itemId in itemIds)
            {
                CatalogItemPetsi item = cs.GetCatalogItemById(itemId);
                if (item.VariationExists("Regular")) //CHECK VEGANS, UNBAKED, V UNBAKED
                {
                    result.Add(new PetsiOrderLineItem(item.ItemName, itemId, 0, 0, 0, 0, 1));
                }
                else
                {
                    result.Add(new PetsiOrderLineItem(item.ItemName, itemId, 1, 1, 1, 1, 0));
                }
            }
            return result;
        }
        public static List<string> GetSpringPieIds()
        {
            return new List<string> {
            //MUD*
            "5EQVRWOQZOVWRXYVMXEEEK42",
            //CBP*
            "UBPPQ6W6QJE2EUNXMXE6CYGY",
            //PECAN*
            "Y4RKXZ2QML43POAYWKHB53FE",
            //MIX*
            "LRJGUOIJUGOQULSFVYMS7QNA",
            //CHERRY*
            "AI3FD2KHMNOM4RXQ323OSVLK",
            //APP CRUMB*
            "5FN34FCDEUPFGWZVO5J62MM4",
            //APPLE*
            "XI3XCRCDMPOPILIMWSMPHXGZ",
            //BLUE*
            "U2JEQPZ4NIIPI4KTSHHTDDSR",
            //STRAWBARB*
            "JZYB6XOSI7JF44IESBHYVBKH",
            //KEYLIME*
            "X2BK7GH32XDCSZHYB6C5S7O6",
            //BACON*
            "HDDJLWVTEFMHOMFYLQELEPCN",
            //Potato Leek
            "PP3CF56MKXHKOK2PNRB3MHU6",
            //POTM (mango lassi)
            "HXJX3VRXPQCAMCLSRDJDLEUB",
            //vegan apple*
            "QBZPGPJRNZOG4TKKXLJ3YCCR",
            //vegan blue*
            "BDPD6EWKRQSQKCHN7QISIQZ4",
            "BDPD6EWKRQSQKCHN7QISIQZ4",
            //vegan cherry*
            "S5M27M27YV35H5TRKWHMIFOB",
            //vegan mix*
            "VUMTY3Q7442HWKJG22ZTLXL4",
            };
        }
        public static List<string> GetSummerPieIds()
        {
            return new List<string> {
            //MUD*
            "5EQVRWOQZOVWRXYVMXEEEK42",
            //CBP*
            "UBPPQ6W6QJE2EUNXMXE6CYGY",
            //PECAN*
            "Y4RKXZ2QML43POAYWKHB53FE",
            //MIX*
            "LRJGUOIJUGOQULSFVYMS7QNA",
            //CHERRY*
            "AI3FD2KHMNOM4RXQ323OSVLK",
            //APP CRUMB*
            "5FN34FCDEUPFGWZVO5J62MM4",
            //APPLE*
            "XI3XCRCDMPOPILIMWSMPHXGZ",
            //PEACH*
            "WAPE6OUWHKXKLYVAIHZN2SSO",
            //PEACH BLACK*
            "RRFJV23GCJZCWEPOTTFGRP22",
            //BLUE*
            "U2JEQPZ4NIIPI4KTSHHTDDSR",
            //LEMON
            "LPM5UVEKCHV5RERZJYUJMVQE",
            //STRAWBARB*
            "JZYB6XOSI7JF44IESBHYVBKH",
            //KEYLIME*
            "X2BK7GH32XDCSZHYB6C5S7O6",
            //BACON*
            "HDDJLWVTEFMHOMFYLQELEPCN",
            //MOZZ*
            "PCFQ4OLXXSFNJZ5VP7O6SBMP",
            //JALAPENO
            "WB7XAPY5CIR4OBVGQU3HZ5IW",
            //POTM (mango lassi)
            "HXJX3VRXPQCAMCLSRDJDLEUB",
            //vegan apple
            "QBZPGPJRNZOG4TKKXLJ3YCCR",
            //vegan blue
            "BDPD6EWKRQSQKCHN7QISIQZ4",
            //vegan cherry
            "S5M27M27YV35H5TRKWHMIFOB",
            //vegan mix
            "VUMTY3Q7442HWKJG22ZTLXL4",
            };
        }

        public static List<string> GetFallPieIds()
        {
            return new List<string> {
            //MUD*
            "5EQVRWOQZOVWRXYVMXEEEK42",
            //CBP*
            "UBPPQ6W6QJE2EUNXMXE6CYGY",
            //PECAN*
            "Y4RKXZ2QML43POAYWKHB53FE",
            //MIX*
            "LRJGUOIJUGOQULSFVYMS7QNA",
            //PUMPKIN
            "ZPUCJUQEX6BBR2TKB746ETMD",
            //SALTY
            "HFGEU3EQQ266MTQ4S7IR4B2S",
            //SWEEP
            "4VOFPSRMWXKGYYIAB5BKC45X",
            //SPP
            "JMQJZLPVKI5VZ2MICZC7DGES",
            //CHERRY*
            "AI3FD2KHMNOM4RXQ323OSVLK",
            //APP CRUMB*
            "5FN34FCDEUPFGWZVO5J62MM4",
            //APC
            "5VU754PEJGTICGNKS26TO476",
            //APPLE*
            "XI3XCRCDMPOPILIMWSMPHXGZ",
            //BLUE*
            "U2JEQPZ4NIIPI4KTSHHTDDSR",
            //BACON*
            "HDDJLWVTEFMHOMFYLQELEPCN",
            //Ham
            "LTLXE3ZSP6DACWGUSMQ5KDJE",
            //VEG
            "REAN6G52MNMAXGRNLDWFVLBT",
            //POTM (mango lassi)
            "HXJX3VRXPQCAMCLSRDJDLEUB",
            //vegan apple*
            "QBZPGPJRNZOG4TKKXLJ3YCCR",
            //vegan blue*
            "BDPD6EWKRQSQKCHN7QISIQZ4",
            //vegan cherry*
            "S5M27M27YV35H5TRKWHMIFOB",
            //vegan mix*
            "VUMTY3Q7442HWKJG22ZTLXL4",
            };
        }

        public static List<string> GetSummerPastryIds()
        {
            return new List<string>
            {
                //TRIPLE
                "userbased-18b89386-745e-44d0-90cd-fd68f9895bf2",
                //CURRANT
                "userbased-fe4ca24e-7236-433d-8d15-308ebe601f00",
                //LEMON
                "userbased-ab7383e1-38f0-46f2-9abc-403a8ddaf538",
                //BISC
                "HPZP3O276SY467XNTYSTWGKM",
                //BLUE
                "N6XRIRRLZB5Q6LKOGC4ZNZ3W",
                //CC
                "RJJHBDTUMENI2DLHQAYRDPUB",
                //SNICK
                "CXNP6DDA2TSYGJQPULZEPPK5",
                //PB
                "6HRHO6HGBYVHGX7C4EA7UCLZ",
                //GING
                "KUJVD7J45N4BG4XGMVHYN7UK",
                //DSA
                "NSAV7C76JX3EO4T5EFENEJDX",
                //MOCHA
                "GN4AD3OXNDEYEWKEA257K2RA",
                //OAT
                "FURQUXJFPZPPPFBIIFZQZELM",
                //BRIOCHE(everything but the bagel)
                "EQXEL4IBTZUNDNZVLGVDWR3K",
                //CORN
                "2XFLPIDBCKFVMO4MRAQHELJW",
                //SAVORY CORN
                "NLKDEWMDHTC2CZRRHIT3KQBP",
            };
        }

        public static List<string> GetMerchandiseItemIds()
        {
            ICatalogService catalogService = (ICatalogService)ServiceManagerSingleton.GetInstance().GetService(Identifiers.SERVICE_CATALOG);
            return catalogService.GetItemIdsByCategory(Identifiers.CATEGORY_MERCH);
        }

        public static List<string> GetStandardOrderTypes()
        {
            return new List<string>
            {
                "Square",
                "Wholesale",
                "Special",
                "FarmersMarket"
            };
        }
    }
}
