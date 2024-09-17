using Petsi.Managers;
using Petsi.Services;
using Petsi.Units;
using Petsi.Utils;

namespace Petsi.Tests
{
    public class InputGenerator
    {
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
                    
                    order.OrderDueDate = $"{year}-{month}-{day}T{hour}:{minute}:00.000Z";
                    hour++;
                    if(hour == 16) { hour = 8; }
                    if(minute == 10){ minute = 30;}
                    else{minute = 10;}

                    if(order.OrderType == "Square")
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

        public static List<string> GetSummerPieIds()
        {
            return new List<string> {
            //MUD
            "5EQVRWOQZOVWRXYVMXEEEK42",
            //CBP
            "UBPPQ6W6QJE2EUNXMXE6CYGY",
            //PECAN
            "Y4RKXZ2QML43POAYWKHB53FE",
            //MIX
            "LRJGUOIJUGOQULSFVYMS7QNA",
            //CHERRY
            "AI3FD2KHMNOM4RXQ323OSVLK",
            //APP CRUMB
            "5FN34FCDEUPFGWZVO5J62MM4",
            //APPLE
            "XI3XCRCDMPOPILIMWSMPHXGZ",
            //PEACH
            "WAPE6OUWHKXKLYVAIHZN2SSO",
            //PEACH BLACK
            "RRFJV23GCJZCWEPOTTFGRP22",
            //BLUE
            "U2JEQPZ4NIIPI4KTSHHTDDSR",
            //LEMON
            "LPM5UVEKCHV5RERZJYUJMVQE",
            //STRAWBARB
            "JZYB6XOSI7JF44IESBHYVBKH",
            //KEYLIME
            "X2BK7GH32XDCSZHYB6C5S7O6",
            //BACON
            "HDDJLWVTEFMHOMFYLQELEPCN",
            //MOZZ
            "PCFQ4OLXXSFNJZ5VP7O6SBMP",
            //JALAPENO
            "WB7XAPY5CIR4OBVGQU3HZ5IW",
            //POTM (mango lassi)
            "HXJX3VRXPQCAMCLSRDJDLEUB"
            };
        }

        public static List<string> GetSummerPastryIds()
        {
            return new List<string>
            {
                //TRIPLE
                "87b878ad-a45a-44f3-a1b3-8acdc03a8062",
                //CURRANT
                "fd3eba39-067b-422f-8ccc-01036fabaa13",
                //LEMON
                "12e6b7e0-c2fb-4ad4-be3b-1d960bd63cfd",
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
            };
        }
    }
}
