﻿using Petsi.Utils;
using System.ComponentModel;

namespace Petsi.Units
{
    public class BackListItem : INotifyPropertyChanged
    {
        /// <summary>
        /// The name that is displayed on a backlist page.
        /// </summary>
        private string _pageDisplayName;
        public string PageDisplayName
        {
            get {
                return _pageDisplayName;
            }
            set 
            {
                if (_pageDisplayName != value)
                { 
                    _pageDisplayName = value;
                    OnPropertyChanged(nameof(PageDisplayName));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// The corresponding id of the item it represents from the square catalog.
        /// </summary>
        public string CatalogObjId { get; set; }

        private string _itemName;
        public string ItemName
        {
            get
            {
                return _itemName;
            }
            set
            {
                if (_itemName != value)
                {
                    _itemName = value;
                    OnPropertyChanged(nameof(ItemName));
                }
            }
        }

        public BackListItem() {}
        public BackListItem(string pageDisplayName, string catalogObjId)
        {
            PageDisplayName = pageDisplayName;
            CatalogObjId = catalogObjId;
        }
        public BackListItem(string pageDisplayName, string catalogObjId, string itemName)
        {
            PageDisplayName = pageDisplayName;
            CatalogObjId = catalogObjId;
            ItemName = itemName;
        }

        #region PIE
        public static BackListItem MUD()
        {
            return new BackListItem("MUD", "5EQVRWOQZOVWRXYVMXEEEK42");
        }
        public static BackListItem CBP()
        {
            return new BackListItem("CBP", "UBPPQ6W6QJE2EUNXMXE6CYGY");
        }
        public static BackListItem PECAN()
        {
            return new BackListItem("PECAN", "Y4RKXZ2QML43POAYWKHB53FE");
        }
        public static BackListItem MIX()
        {
            return new BackListItem("MIX", "LRJGUOIJUGOQULSFVYMS7QNA");
        }
        public static BackListItem CHERRY()
        {
            return new BackListItem("CHERRY", "AI3FD2KHMNOM4RXQ323OSVLK");
        }
        public static BackListItem APP_CRUMB()
        {
            return new BackListItem("APP CRUMB", "5FN34FCDEUPFGWZVO5J62MM4");
        }
        public static BackListItem APPLE()
        {
            return new BackListItem("APPLE", "XI3XCRCDMPOPILIMWSMPHXGZ");
        }
        public static BackListItem PEACH()
        {
            return new BackListItem("PEACH", "WAPE6OUWHKXKLYVAIHZN2SSO");
        }
        public static BackListItem PEACH_BLACK()
        {
            return new BackListItem("PEACH BLACK", "RRFJV23GCJZCWEPOTTFGRP22");
        }
        public static BackListItem STRAWBARB()
        {
            return new BackListItem("STRAWBARB", "JZYB6XOSI7JF44IESBHYVBKH");
        }
        public static BackListItem STRAWOAT()
        {
            return new BackListItem("STRAWOAT", "FX4SNQX4JGQ4VQFMROVGUTRO");
        }
        public static BackListItem BLUE()
        {
            return new BackListItem("BLUE", "U2JEQPZ4NIIPI4KTSHHTDDSR");
        }
        public static BackListItem SALTY()
        {
            return new BackListItem("SALTY", "HFGEU3EQQ266MTQ4S7IR4B2S");
        }
        public static BackListItem LEMON_CHESS_LAV()
        {
            return new BackListItem("LEMON", "LPM5UVEKCHV5RERZJYUJMVQE");
        }
        public static BackListItem KEY_LIME()
        {
            return new BackListItem("KEY LIME", "X2BK7GH32XDCSZHYB6C5S7O6");
        }
        public static BackListItem PUMP()
        {
            return new BackListItem("PUMP", "ZPUCJUQEX6BBR2TKB746ETMD");
        }
        public static BackListItem SWEEP()
        {
            return new BackListItem("SWEEP", "4VOFPSRMWXKGYYIAB5BKC45X");
        }
        public static BackListItem SPP()
        {
            return new BackListItem("SPP", "JMQJZLPVKI5VZ2MICZC7DGES");
        }
        public static BackListItem BACON()
        {
            return new BackListItem("BACON", "HDDJLWVTEFMHOMFYLQELEPCN");
        }
        public static BackListItem HAM()
        {
            return new BackListItem("HAM", "LTLXE3ZSP6DACWGUSMQ5KDJE");
        }
        public static BackListItem SAUSAGE()
        {
            return new BackListItem("SAUSAGE", "NR3CTSC6JVFECGFVNYS3S73S");
        }
        public static BackListItem POTATO()
        {
            return new BackListItem("POTATO", "PP3CF56MKXHKOK2PNRB3MHU6");
        }
        public static BackListItem MOZZ()
        {
            return new BackListItem("MOZZ", "PCFQ4OLXXSFNJZ5VP7O6SBMP");
        }
        public static BackListItem JALAPENO()
        {
            return new BackListItem("JALAPENO", "WB7XAPY5CIR4OBVGQU3HZ5IW");
        }
        public static BackListItem ARTICHOKE()
        {
            return new BackListItem("ARTICHOKE", "HVNK43IHDPX3QN6WSBJHSCAZ");
        }
        /*
        public static BackListItem VEGAN()//vegan on list?
        {
            return new BackListItem("VEGAN", "not implemented");
        }
        */
        public static BackListItem POTM()//define potms?
        {
            return new BackListItem("POTM", Identifiers.CATEGORY_POTM);
        }
        public static BackListItem PARBAKES()//define parbakes?
        {
            return new BackListItem("PARBAKES", Identifiers.CATEGORY_PARBAKE);
        }
        public static BackListItem V_APPLE()
        {
            return new BackListItem("VEGAN APPLE", "PP3CF56MKXHKOK2PNRB3MHU6");
        }
        public static BackListItem V_BLUE()
        {
            return new BackListItem("VEGAN BLUE", "BDPD6EWKRQSQKCHN7QISIQZ4");
        }
        public static BackListItem V_CHERRY()
        {
            return new BackListItem("VEGAN CHERRY", "S5M27M27YV35H5TRKWHMIFOB");
        }
        public static BackListItem V_MIX()
        {
            return new BackListItem("VEGAN Mix", "VUMTY3Q7442HWKJG22ZTLXL4");
        }
        public static BackListItem V_STRAWBARB()
        {
            return new BackListItem("VEGAN Straw", "ZVLI2WL7CMEHAQL6I74FQ4QT");
        }
        public static BackListItem LEMON()
        {
            return new BackListItem("LEMON", "LPM5UVEKCHV5RERZJYUJMVQE");
        }
        public static BackListItem APC()
        {
            return new BackListItem("APC", "5VU754PEJGTICGNKS26TO476");
        }
        public static BackListItem VEG()
        {
            return new BackListItem("VEG", "REAN6G52MNMAXGRNLDWFVLBT");
        }
        #endregion PIE
        #region PASTRY
        public static BackListItem BRIOCHE()
        { //Everything Brioche
            return new BackListItem("BRIOCHE", "EQXEL4IBTZUNDNZVLGVDWR3K");
        }
        public static BackListItem CURRANT()//Scone flavors dont exist in catalog, catalog id is built in SquareOrderInput.ParseOrderLineItem()
        {
            return new BackListItem("CURRANT", "userbased-fe4ca24e-7236-433d-8d15-308ebe601f00");
        }
        public static BackListItem LEMON_SCONE()
        {
            return new BackListItem("LEMON", "userbased-ab7383e1-38f0-46f2-9abc-403a8ddaf538");
        }
        public static BackListItem TRIPLE()
        {
            return new BackListItem("TRIPLE", "userbased-18b89386-745e-44d0-90cd-fd68f9895bf2");
        }
        public static BackListItem BISCUIT()
        {
            return new BackListItem("BISCUIT", "HPZP3O276SY467XNTYSTWGKM");
        }
        public static BackListItem FRESH_BLUE()
        {
            return new BackListItem("BLUE", "N6XRIRRLZB5Q6LKOGC4ZNZ3W");
        }
        public static BackListItem BUNS()//is this a general term or specifically Sweep Bun?
        {
            return new BackListItem("BUNS", "BCEODGMAXN2FHDWNOOUL66HB");
        }
        public static BackListItem CCHIP()
        {
            return new BackListItem("CCHIP", "RJJHBDTUMENI2DLHQAYRDPUB");
        }
        public static BackListItem SNICK()
        {
            return new BackListItem("SNICK", "CXNP6DDA2TSYGJQPULZEPPK5");
        }
        public static BackListItem PB()
        {
            return new BackListItem("PB", "6HRHO6HGBYVHGX7C4EA7UCLZ");
        }
        public static BackListItem GINGER()
        {
            return new BackListItem("GINGER", "KUJVD7J45N4BG4XGMVHYN7UK");
        }
        public static BackListItem BSA()
        {
            return new BackListItem("BSA", "not implemented");
        }
        public static BackListItem DSA()
        {
            return new BackListItem("DSA", "NSAV7C76JX3EO4T5EFENEJDX");
        }
        public static BackListItem MOCHA()
        {
            return new BackListItem("MOCHA", "GN4AD3OXNDEYEWKEA257K2RA");
        }
        public static BackListItem OAT()
        {
            return new BackListItem("OAT", "FURQUXJFPZPPPFBIIFZQZELM");
        }
        public static BackListItem CORN()
        {
            return new BackListItem("CORN", "2XFLPIDBCKFVMO4MRAQHELJW");
        }
        public static BackListItem SAVORY_CORN()
        {
            return new BackListItem("SAVORY CORN", "NLKDEWMDHTC2CZRRHIT3KQBP");
        }
        public static BackListItem BLUE_MUFFIN()
        {
            return new BackListItem("BLUE", "N6XRIRRLZB5Q6LKOGC4ZNZ3W");
        }
        public static BackListItem MEX()
        {
            return new BackListItem("MEX", "H37PVVMGZDKK6FRCONYIR5HW");
        }
        #endregion PASTRY
    }
}
