namespace Petsi.Labels
{
    //FrameBehavior Functionality
    public static class LoadLabels
    {
        //FrameBehavior Functionality
        public static List<(string, string)> GetStandardInitialMap()
        {
            List<(string id, string path)> standardLabelMap = new List<(string id, string path)>()
            {
                ("5FN34FCDEUPFGWZVO5J62MM4", "Apple-Crumb_pie_ingredient_labels.jpg"),
                ("5VU754PEJGTICGNKS26TO476", "Apple-Pear-Cran_pie_ingredient.jpg"),
                ("WB7XAPY5CIR4OBVGQU3HZ5IW", "Bacon-Corn-Jalapeno-pie_ingredient_label-29.jpg"),
                ("HDDJLWVTEFMHOMFYLQELEPCN", "Bacon-Leek-Gruyere-pie_ingredient_label-02.jpg"),
                ("U2JEQPZ4NIIPI4KTSHHTDDSR", "Blueberry_ingredient_label.jpg"),
                ("Y4RKXZ2QML43POAYWKHB53FE", "Brown-Butter-Pecan_pie_ingredient.jpg"),
                ("AI3FD2KHMNOM4RXQ323OSVLK", "cherry-crumb-pie_ingredient.jpg"),
                ("UBPPQ6W6QJE2EUNXMXE6CYGY", "Chocolate-Bourbon-Pecan_pie_ingredient.jpg"),
                ("XI3XCRCDMPOPILIMWSMPHXGZ", "Classic-Apple_pie_ingredient.jpg"),
                ("JZYB6XOSI7JF44IESBHYVBKH", "Classic-Strawberry-Rhubarb-pie_ingredient.jpg"),
                ("PCFQ4OLXXSFNJZ5VP7O6SBMP", "Fresh-Mozz-Tomato-Basil_pie_ingredient.jpg"),
                ("KBXKBROTQ3NBOBWHCUKYI5BT", "Italian-Ricotta-pie_ingredient-28.jpg"),
                ("X2BK7GH32XDCSZHYB6C5S7O6", "Key-Lime_pie_ingredient.jpg"),
                ("LPM5UVEKCHV5RERZJYUJMVQE", "Lemon-Chess_pie_ingredient.jpg"),
                ("HXJX3VRXPQCAMCLSRDJDLEUB", "Mango-Lassi-pie-ingredient.PNG"),
                ("5EQVRWOQZOVWRXYVMXEEEK42", "Mississippi-Mud_pie_ingredient.jpg"),
                ("LRJGUOIJUGOQULSFVYMS7QNA", "Mixed-Berry_pie_ingredient.jpg"),
                ("RRFJV23GCJZCWEPOTTFGRP22", "Peach-Blackberry-pie_ingredient_label.jpg"),
                ("PP3CF56MKXHKOK2PNRB3MHU6", "Potato-Leek-Goat-Cheese-pie_ingredient_label-25.jpg"),
                ("ZPUCJUQEX6BBR2TKB746ETMD", "Pumpkin_pie_ingredient_label.jpg"),
                ("REAN6G52MNMAXGRNLDWFVLBT", "Roast-Veg onion -pie_ingredient.jpg"),
                ("HFGEU3EQQ266MTQ4S7IR4B2S", "Salted-Caramel-Apple_pie_ingredient.jpg"),
                ("FX4SNQX4JGQ4VQFMROVGUTRO", "Strawberry-Rhubarb-Oat-Crumb-pie_ingredient.jpg"),
                ("WAPE6OUWHKXKLYVAIHZN2SSO", "Summer-Peach_pie_ingredient.jpg"),
                ("JMQJZLPVKI5VZ2MICZC7DGES", "Sweet-Potato-Pecan_pie_ingredient.jpg"),
                ("4VOFPSRMWXKGYYIAB5BKC45X", "Sweet-Potato_pie_ingredient.jpg"),
                ("QBZPGPJRNZOG4TKKXLJ3YCCR", "Vegan-Apple_pie_ingredient.jpg"),
                ("BDPD6EWKRQSQKCHN7QISIQZ4", "Vegan-Blueberry_pie_ingredient.jpg"),
                ("S5M27M27YV35H5TRKWHMIFOB", "Vegan-Cherry_pie_ingredient.jpg"),
                ("VUMTY3Q7442HWKJG22ZTLXL4", "Vegan-Mixed-Berry_pie_ingredient.jpg"),
                ("ZVLI2WL7CMEHAQL6I74FQ4QT", "Vegan-Strawberry-Rhubarb-pie_ingredient-29.jpg"),
                ("LTLXE3ZSP6DACWGUSMQ5KDJE", "Walden-Local-Ham-ingredient-label-30.png"),
                ("care", "pie-care-directory-label-v2-03.jpg"),
                ("round", "Round-Allergen-Label-01.png")
            };
            return standardLabelMap;
        }
        public static List<(string id, string path)> GetCutieInitialMap()
        {
            List<(string id, string path)> cutieLabelMap = new List<(string id, string path)>()
            { 
                ("5FN34FCDEUPFGWZVO5J62MM4", "Apple-Crumb_cutie-pie-ingred-labels-07.jpg"),
                ("5VU754PEJGTICGNKS26TO476", "Apple-Pear-Cranberry-cutie-pie.jpg"),
                ("U2JEQPZ4NIIPI4KTSHHTDDSR", "Blueberry_cutie-pie-ingred-labels-09.jpg"),
                ("Y4RKXZ2QML43POAYWKHB53FE", "Brown-Butter-Pecan_cutie-pie-ingred-labels-11.jpg"),
                ("AI3FD2KHMNOM4RXQ323OSVLK", "Cherry-Crumb_cutie-pie-ingred-labels-08.jpg"),
                ("UBPPQ6W6QJE2EUNXMXE6CYGY", "Chocolate-Bourbon-Pecan_cutie-pie-ingred-labels-12.jpg"),
                ("XI3XCRCDMPOPILIMWSMPHXGZ", "Classic-Apple_cutie-pie-ingred-labels-05.jpg"),
                ("5EQVRWOQZOVWRXYVMXEEEK42", "cutiepie-labels_mississippi-mud-13.jpg"),
                ("X2BK7GH32XDCSZHYB6C5S7O6", "Key-Lime-cutie-labels-14.jpg"),
                ("LRJGUOIJUGOQULSFVYMS7QNA", "Mixed-Berry_cutie-pie-ingred-labels-10.jpg"),
                ("ZPUCJUQEX6BBR2TKB746ETMD", "Pumpkin_cutie-pie-ingred-labels-06.jpg"),
                ("JMQJZLPVKI5VZ2MICZC7DGES", "Sweet-Potato-Pecan-cutie-pie.jpg"),
                ("QBZPGPJRNZOG4TKKXLJ3YCCR", "Vegan-Apple_cutie-pie-ingred-labels-02.jpg"),
                ("BDPD6EWKRQSQKCHN7QISIQZ4", "Vegan-Blueberry_cutie-pie-ingred-labels-01.jpg"),
                ("S5M27M27YV35H5TRKWHMIFOB", "Vegan-Cherry_cutie-pie-ingred-labels-03.jpg"),
                ("VUMTY3Q7442HWKJG22ZTLXL4", "Vegan-Mixed-Berry_cutie-pie-ingred-labels-04.jpg")
            };
            return cutieLabelMap;
        }
    }
}
