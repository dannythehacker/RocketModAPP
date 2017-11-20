﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using rceAssets;

namespace RocketConfigEditor
{
    class ConfigHandler
    {
        static List<Plugin> plugins;
        static List<Item> items;
        public static AppConfig getConfig()
        {
            string ConFileStart = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName.ToString() + "\\Roaming\\RocketApp";
            AppConfig Example;
            if (Directory.Exists(ConFileStart))
            {
                if (!File.Exists(ConFileStart + "\\A70zT589IFgr1B.txt"))
                {
                    File.Create(ConFileStart + "\\A70zT589IFgr1B.txt").Close();

                    Example = new AppConfig("127.0.0.1", "C:\\Rocket", true, "21", "", "", false);
                    FileStream stream = new FileStream(ConFileStart + "\\A70zT589IFgr1B.txt", FileMode.Create, FileAccess.Write);
                    StreamWriter write = new StreamWriter(stream);
                    write.WriteLine(Example.HostName + "," + Example.DirPath + "," + Example.IsLocal.ToString() + "," + Example._Port + "," + Example.User + "," + Example.Password + "," + Example.EncrType);
                    write.Flush();
                    stream.Flush();
                    write.Close();
                    stream.Dispose();
                    stream.Close();
                }
            }
            else
            {
                Directory.CreateDirectory(ConFileStart);
                if (!File.Exists(ConFileStart + "\\A70zT589IFgr1B.txt"))
                {
                    File.Create(ConFileStart + "\\A70zT589IFgr1B.txt").Close();
                }
                Example = new AppConfig("127.0.0.1", "C:\\Rocket", true, "21", "", "", false);
                FileStream stream = new FileStream(ConFileStart + "\\A70zT589IFgr1B.txt", FileMode.Create, FileAccess.Write);
                StreamWriter write = new StreamWriter(stream);
                write.WriteLine(Example.HostName + "," + Example.DirPath + "," + Example.IsLocal.ToString() + "," + Example._Port + "," + Example.User + "," + Example.Password + "," + Example.EncrType);
                write.Flush();
                stream.Flush();
                write.Close();
                stream.Dispose();
                stream.Close();
            }
            StreamReader read = new StreamReader(ConFileStart + "\\A70zT589IFgr1B.txt");
            string line;
            string[] data = null;
            while ((line = read.ReadLine()) != null)
            {
                data = line.Split(',');
            }

            AppConfig appCon = new AppConfig(data[0], data[1], Convert.ToBoolean(data[2]), data[3], data[4], data[5], Convert.ToBoolean(data[6]));
            read.Close();
            return appCon;
        }

        public static void setConfig(AppConfig Example)
        {
            string ConFileStart = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName.ToString() + "\\Roaming\\RocketApp\\A70zT589IFgr1B.txt";
            FileStream stream = new FileStream(ConFileStart, FileMode.Create, FileAccess.Write);
            StreamWriter write = new StreamWriter(stream);
            write.WriteLine(Example.HostName + "," + Example.DirPath + "," + Example.IsLocal.ToString() + "," + Example._Port + "," + Example.User + "," + Example.Password + "," + Example.EncrType);
            write.Flush();
            write.Close();
            stream.Dispose();
            stream.Close();
        }
        public static void checkLocalFiles()
        {
            string ConFileStart = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)).FullName.ToString() + "\\RocketApp";
            string pluginDir = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)).FullName.ToString() + "\\RocketApp\\Plugins";

            if (!Directory.Exists(ConFileStart))
            {
                Directory.CreateDirectory(ConFileStart);
            }
            if (!Directory.Exists(pluginDir))
            {
                Directory.CreateDirectory(pluginDir);
            }
        }
        //fetch information on the plugins
        public static List<Plugin> GetPluginList()
        {
            string ConFileStart = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName.ToString() + "\\Roaming\\RocketApp";
            if (Directory.Exists(ConFileStart))
            {
                if (!File.Exists(ConFileStart + "\\PluginList.txt"))
                {
                    File.Create(ConFileStart + "\\PluginList.txt").Close();

                    LoadDefaultPlugins();
                    FileStream stream = new FileStream(ConFileStart + "\\PluginList.txt", FileMode.Create, FileAccess.Write);
                    StreamWriter write = new StreamWriter(stream);
                    foreach (Plugin item in plugins)
                    {
                        write.WriteLine(item.Name + "," + item.ConfigFile + "," + item.Enabled);

                    }
                    write.Flush();
                    stream.Flush();
                    write.Close();
                    stream.Dispose();
                    stream.Close();
                }
                else
                {
                    plugins = new List<Plugin>();
                    StreamReader read = new StreamReader(ConFileStart + "\\PluginList.txt");
                    string line;
                    string[] data = null;
                    while ((line = read.ReadLine()) != null)
                    {
                        data = line.Split(',');
                        plugins.Add(new Plugin(data[0], data[1], Convert.ToBoolean(data[2])));
                    }
                    read.Close();
                }
            }
            return plugins;
        }
        //this is only a default item index and will need to be updated based on game content.
        //idea (a simple textfile built by the community)
        public static List<Item> GetItemIndex()
        {
            string ConFileStart = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName.ToString() + "\\Roaming\\RocketApp";
            if (Directory.Exists(ConFileStart))
            {
                if (!File.Exists(ConFileStart + "\\ItemIndex.txt"))
                {
                    File.Create(ConFileStart + "\\ItemIndex.txt").Close();

                    CreateLocalItemIndex();
                    FileStream stream = new FileStream(ConFileStart + "\\ItemIndex.txt", FileMode.Create, FileAccess.Write);
                    StreamWriter write = new StreamWriter(stream);
                    foreach (Item item in items)
                    {
                        write.WriteLine(item.ID + "," + item.Name);

                    }
                    write.Flush();
                    stream.Flush();
                    write.Close();
                    stream.Dispose();
                    stream.Close();
                }
                else
                {
                    items = new List<Item>();
                    StreamReader read = new StreamReader(ConFileStart + "\\ItemIndex.txt");
                    string line;
                    string[] data = null;
                    while ((line = read.ReadLine()) != null)
                    {
                        data = line.Split(',');
                        items.Add(new Item(data[0], data[1]));
                    }
                    read.Close();
                }
            }
            return items;
        }
        //generate a list of plugins editable by user.
        public static void SetPlugins(List<Plugin> plugin)
        {
            string ConFileStart = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName.ToString() + "\\Roaming\\RocketApp";

            FileStream stream = new FileStream(ConFileStart + "\\PluginList.txt", FileMode.Create, FileAccess.Write);
            StreamWriter write = new StreamWriter(stream);
            foreach (Plugin item in plugin)
            {
                write.WriteLine(item.Name + "," + item.ConfigFile + "," + item.Enabled);

            }
            write.Flush();
            stream.Flush();
            write.Close();
            stream.Dispose();
            stream.Close();
        }
        //loading of plugins available/usable on the program
        //TODO: Build a mre dynamic way to load the plugins and add a "builder" for the plugins 
        public static void LoadDefaultPlugins()
        {
            
        }
        //this is an Important document!, it can be manually modified later on.
        public static void CreateLocalItemIndex()
        {
            items = new List<Item>()
            {
           new Item("2","Work Jeans"),
new Item("3","Orange Hoodie"),
new Item("4","Eaglefire"),
new Item("5","Eaglefire Iron Sights"),
new Item("6","Military Magazine"),
new Item("7","Military Suppressor"),
new Item("8","Vertical Grip"),
new Item("9","Red Daypack"),
new Item("10","Police Vest"),
new Item("11","Red Bandana"),
new Item("12","???"),
new Item("13","Canned Beans"),
new Item("14","Bottled Water"),
new Item("15","Medkit"),
new Item("16","Camp Axe"),
new Item("17","Military Drum"),
new Item("18","Timberwolf"),
new Item("19","Timberwolf Iron Sights"),
new Item("20","Timberwolf Magazine"),
new Item("21","8x Scope"),
new Item("22","Red Cross Scope"),
new Item("23","Gold Tuxedo Bottom"),
new Item("24","Gold Tuxedo Top"),
new Item("25","Gold Bowtie"),
new Item("26","Gold Monocle"),
new Item("27","Tophat"),
new Item("28","Gas"),
new Item("29","Maple Fortification"),
new Item("30","Maple Barricade"),
new Item("31","Maple Floor"),
new Item("32","Maple Doorway"),
new Item("33","Maple Wall"),
new Item("34","Maple Window"),
new Item("35","Maple Roof"),
new Item("36","Maple Pillar"),
new Item("37","Birch Log"),
new Item("38","Birch Stick"),
new Item("39","Maple Log"),
new Item("40","Maple Stick"),
new Item("41","Pine Log"),
new Item("42","Pine Stick"),
new Item("43","Military Ammunition Crate"),
new Item("44","Civilian Ammunition Box"),
new Item("45","Birch Barricade"),
new Item("46","Pine Barricade"),
new Item("47","Birch Fortification"),
new Item("48","Pine Fortification"),
new Item("49","Birch Doorway"),
new Item("50","Pine Doorway"),
new Item("51","Birch Floor"),
new Item("52","Pine Floor"),
new Item("53","Birch Pillar"),
new Item("54","Pine Pillar"),
new Item("55","Birch Roof"),
new Item("56","Pine Roof"),
new Item("57","Birch Wall"),
new Item("58","Pine Wall"),
new Item("59","Birch Window"),
new Item("60","Pine Window"),
new Item("61","Maple Plank"),
new Item("62","Birch Plank"),
new Item("63","Pine Plank"),
new Item("64","Rope"),
new Item("65","Wire"),
new Item("66","Cloth"),
new Item("67","Metal Scrap"),
new Item("68","Metal Sheet"),
new Item("69","Tape"),
new Item("70","Glue"),
new Item("71","Nails"),
new Item("72","Can"),
new Item("73","Explosives"),
new Item("74","Bricks"),
new Item("75","Chemicals"),
new Item("76","Blowtorch"),
new Item("77","Canned Tomato Soup"),
new Item("78","Canned Chicken Soup"),
new Item("79","Canned Tuna"),
new Item("80","Canned Cola"),
new Item("81","MRE"),
new Item("82","Chips"),
new Item("83","Chocolate Bar"),
new Item("84","Candy Bar"),
new Item("85","Granola Bar"),
new Item("86","Energy Bar"),
new Item("87","Canned Pasta"),
new Item("88","Canned Bacon"),
new Item("89","Canned Beef"),
new Item("90","Canned Sardines"),
new Item("91","Apple Juice"),
new Item("92","Grape Juice"),
new Item("93","Bottled Energy"),
new Item("94","Bottled Coconut"),
new Item("95","Bandage"),
new Item("96","Splint"),
new Item("97","Colt"),
new Item("98","Colt Magazine"),
new Item("99","Cobra"),
new Item("100","Cobra Magazine"),
new Item("101","Schofield"),
new Item("102","Schofield Iron Sights"),
new Item("103","Schofield Clip"),
new Item("104","Fire Axe"),
new Item("105","Baseball Bat"),
new Item("106","Hockey Stick"),
new Item("107","Ace"),
new Item("108","Ace Clip"),
new Item("109","Hawkhound"),
new Item("110","Hawkhound Iron Sights"),
new Item("111","Hawkhound Magazine"),
new Item("112","Bluntforce"),
new Item("113","12 Gauge Shells"),
new Item("114","Bluntforce Iron Sights"),
new Item("115","Mauve Berry"),
new Item("116","Honeybadger"),
new Item("117","Honeybadger Barrel"),
new Item("118","Honeybadger Iron Sights"),
new Item("119","Ranger Ammunition Box"),
new Item("120","Kitchen Knife"),
new Item("121","Military Knife"),
new Item("122","Zubeknakov"),
new Item("123","Ranger Magazine"),
new Item("124","Zubeknakov Iron Sights"),
new Item("125","Ranger Drum"),
new Item("126","Nykorev"),
new Item("127","Nykorev Box"),
new Item("128","Nykorev Iron Sights"),
new Item("129","Snayperskya"),
new Item("130","Snayperskya Magazine"),
new Item("131","Snayperskya Iron Sights"),
new Item("132","Dragonfang"),
new Item("133","Dragonfang Box"),
new Item("134","Dragonfang Iron Sights"),
new Item("135","Golf Club"),
new Item("136","Sledgehammer"),
new Item("137","Butcher Knife"),
new Item("138","Hammer"),
new Item("139","Swiss Knife"),
new Item("140","Butterfly Knife"),
new Item("141","Saw"),
new Item("142","Rake"),
new Item("143","Bipod"),
new Item("144","Ranger Suppressor"),
new Item("145","Horizontal Grip"),
new Item("146","Red Dot Sight"),
new Item("147","Red Halo Sight"),
new Item("148","Red Chevron Scope"),
new Item("149","Military Barrel"),
new Item("150","Military Muzzle"),
new Item("151","Tactical Laser"),
new Item("152","Tactical Light"),
new Item("153","7x Scope"),
new Item("154","Orange Shirt"),
new Item("155","Orange T-Shirt"),
new Item("156","Orange Parka"),
new Item("157","Purple Hoodie"),
new Item("158","Purple Shirt"),
new Item("159","Purple T-Shirt"),
new Item("160","Purple Parka"),
new Item("161","Green Hoodie"),
new Item("162","Green Parka"),
new Item("163","Green Shirt"),
new Item("164","Green T-Shirt"),
new Item("165","Red Hoodie"),
new Item("166","Red Parka"),
new Item("167","Red Shirt"),
new Item("168","Red T-Shirt"),
new Item("169","Yellow Hoodie"),
new Item("170","Yellow Parka"),
new Item("171","Yellow Shirt"),
new Item("172","Yellow T-Shirt"),
new Item("173","Blue Hoodie"),
new Item("174","Blue Parka"),
new Item("175","Blue Shirt"),
new Item("176","Blue T-Shirt"),
new Item("177","White Hoodie"),
new Item("178","White Parka"),
new Item("179","White Shirt"),
new Item("180","White T-Shirt"),
new Item("181","Black Hoodie"),
new Item("182","Black Parka"),
new Item("183","Black Shirt"),
new Item("184","Black T-Shirt"),
new Item("185","Black Bandana"),
new Item("186","Blue Bandana"),
new Item("187","Green Bandana"),
new Item("188","Orange Bandana"),
new Item("189","Purple Bandana"),
new Item("190","White Bandana"),
new Item("191","Yellow Bandana"),
new Item("192","Black Toque"),
new Item("193","Blue Toque"),
new Item("194","Green Toque"),
new Item("195","Orange Toque"),
new Item("196","Purple Toque"),
new Item("197","Red Toque"),
new Item("198","White Toque"),
new Item("199","Yellow Toque"),
new Item("200","Black Daypack"),
new Item("201","Blue Daypack"),
new Item("202","Green Daypack"),
new Item("203","Orange Daypack"),
new Item("204","Purple Daypack"),
new Item("205","White Daypack"),
new Item("206","Yellow Daypack"),
new Item("207","Outfit Jeans"),
new Item("208","Cowboy Jeans"),
new Item("209","Cargo Pants"),
new Item("210","???"),
new Item("211","Plaid Shirt"),
new Item("212","Khaki Pants"),
new Item("213","Corduroy Pants"),
new Item("214","Trouser Pants"),
new Item("215","Black Sweatervest"),
new Item("216","Blue Sweatervest"),
new Item("217","Green Sweatervest"),
new Item("218","Orange Sweatervest"),
new Item("219","Purple Sweatervest"),
new Item("220","Red Sweatervest"),
new Item("221","Yellow Sweatervest"),
new Item("222","White Sweatervest"),
new Item("223","Police Top"),
new Item("224","Police Bottom"),
new Item("225","Police Cap"),
new Item("226","Khaki Shorts"),
new Item("227","Cargo Shorts"),
new Item("228","Corduroy Shorts"),
new Item("229","Trouser Shorts"),
new Item("230","Chef Top"),
new Item("231","Chef Bottom"),
new Item("232","Construction Top"),
new Item("233","Firefighter Top"),
new Item("234","Firefighter Bottom"),
new Item("235","Ghillie Top"),
new Item("236","Ghillie Bottom"),
new Item("237","Ghillie Hood"),
new Item("238","Ghillie Vest"),
new Item("239","Chef Hat"),
new Item("240","Construction Helmet"),
new Item("241","Firefighter Helmet"),
new Item("242","Farmer Top"),
new Item("243","Farmer Bottom"),
new Item("244","Farmer Hat"),
new Item("245","Black Travelpack"),
new Item("246","Blue Travelpack"),
new Item("247","Green Travelpack"),
new Item("248","Orange Travelpack"),
new Item("249","Purple Travelpack"),
new Item("250","Red Travelpack"),
new Item("251","White Travelpack"),
new Item("252","Yellow Travelpack"),
new Item("253","Alicepack"),
new Item("254","Grenade"),
new Item("255","Blue Flare"),
new Item("256","Green Flare"),
new Item("257","Orange Flare"),
new Item("258","Purple Flare"),
new Item("259","Red Flare"),
new Item("260","Yellow Flare"),
new Item("261","Black Smoke"),
new Item("262","Blue Smoke"),
new Item("263","Green Smoke"),
new Item("264","Orange Smoke"),
new Item("265","Purple Smoke"),
new Item("266","Red Smoke"),
new Item("267","White Smoke"),
new Item("268","Yellow Smoke"),
new Item("269","Vaccine"),
new Item("270","Amber Berry"),
new Item("271","Indigo Berry"),
new Item("272","Jade Berry"),
new Item("273","Russet Berry"),
new Item("274","Teal Berry"),
new Item("275","Vermillion Berry"),
new Item("276","Flashlight"),
new Item("277","Carjack"),
new Item("278","Santa Hat"),
new Item("279","Santa Top"),
new Item("280","Santa Bottom"),
new Item("281","Maple Door"),
new Item("282","Birch Door"),
new Item("283","Pine Door"),
new Item("284","Jail Door"),
new Item("285","Metal Bar"),
new Item("286","Vault Door"),
new Item("287","Bars Fortification"),
new Item("288","Black Bedroll"),
new Item("289","Blue Bedroll"),
new Item("290","Green Bedroll"),
new Item("291","Orange Bedroll"),
new Item("292","Purple Bedroll"),
new Item("293","Red Bedroll"),
new Item("294","White Bedroll"),
new Item("295","Yellow Bedroll"),
new Item("296","16x Scope"),
new Item("297","Grizzly"),
new Item("298","Grizzly Magazine"),
new Item("299","Grizzly Iron Sights"),
new Item("300","Shadowstalker"),
new Item("301","Rail"),
new Item("302","Shadowstalker Scope"),
new Item("303","Prisoner Top"),
new Item("304","Prisoner Bottom"),
new Item("305","RCMP Bottom"),
new Item("306","RCMP Top"),
new Item("307","Forest Military Top"),
new Item("308","Forest Military Bottom"),
new Item("309","Forest Military Helmet"),
new Item("310","Forest Military Vest"),
new Item("311","Medic Top"),
new Item("312","Medic Bottom"),
new Item("313","RCMP Hat"),
new Item("314","Grocer Top"),
new Item("315","Grocer Bottom"),
new Item("316","Maple Stairs"),
new Item("317","Pine Stairs"),
new Item("318","Birch Stairs"),
new Item("319","Maple Hole"),
new Item("320","Pine Hole"),
new Item("321","Birch Hole"),
new Item("322","Maple Ramp"),
new Item("323","Birch Ramp"),
new Item("324","Pine Ramp"),
new Item("325","Maple Ladder"),
new Item("326","Birch Ladder"),
new Item("327","Pine Ladder"),
new Item("328","Locker"),
new Item("329","Carrot"),
new Item("330","Carrot Seed"),
new Item("331","Planter"),
new Item("332","Fertilizer"),
new Item("333","Binoculars"),
new Item("334","Military Nightvision"),
new Item("335","Corn"),
new Item("336","Corn Seed"),
new Item("337","Canteen"),
new Item("338","Lettuce"),
new Item("339","Lettuce Seed"),
new Item("340","Tomato"),
new Item("341","Tomato Seed"),
new Item("342","Potato"),
new Item("343","Potato Seed"),
new Item("344","Wheat"),
new Item("345","Wheat Seed"),
new Item("346","Crossbow"),
new Item("347","Arrow"),
new Item("348","Maple Arrow"),
new Item("349","Crossbow Iron Sights"),
new Item("350","Crossbow Barrel"),
new Item("351","Birch Arrow"),
new Item("352","Pine Arrow"),
new Item("353","Maple Bow"),
new Item("354","Bow Barrel"),
new Item("355","Birch Bow"),
new Item("356","Pine Bow"),
new Item("357","Compound Bow"),
new Item("358","Compound Iron Sights"),
new Item("359","Maple Torch"),
new Item("360","Birch Torch"),
new Item("361","Pine Torch"),
new Item("362","Campfire"),
new Item("363","Maplestrike"),
new Item("364","Maplestrike Iron Sights"),
new Item("365","Sandbag"),
new Item("366","Maple Crate"),
new Item("367","Birch Crate"),
new Item("368","Pine Crate"),
new Item("369","Metal Floor"),
new Item("370","Metal Doorway"),
new Item("371","Metal Wall"),
new Item("372","Metal Window"),
new Item("373","Metal Roof"),
new Item("374","Metal Pillar"),
new Item("375","Metal Stairs"),
new Item("376","Metal Hole"),
new Item("377","Metal Ramp"),
new Item("378","Metal Door"),
new Item("379","Metal Ladder"),
new Item("380","Masterkey"),
new Item("381","20 Gauge Shells"),
new Item("382","Caltrop"),
new Item("383","Maple Spikes"),
new Item("384","Birch Spikes"),
new Item("385","Pine Spikes"),
new Item("386","Barbed Wire"),
new Item("387","Adrenaline"),
new Item("388","Morphine"),
new Item("389","Antibiotics"),
new Item("390","Painkillers"),
new Item("391","Vitamins"),
new Item("392","Tablets"),
new Item("393","Rag"),
new Item("394","Dressing"),
new Item("395","Bloodbag"),
new Item("396","Mauve Crushed"),
new Item("397","Amber Crushed"),
new Item("398","Indigo Crushed"),
new Item("399","Jade Crushed"),
new Item("400","Russet Crushed"),
new Item("401","Teal Crushed"),
new Item("402","Vermillion Crushed"),
new Item("403","Suturekit"),
new Item("404","Cough Syrup"),
new Item("405","Mechanic Top"),
new Item("406","Mechanic Bottom"),
new Item("407","Engineer Top"),
new Item("408","Engineer Bottom"),
new Item("409","Aviators"),
new Item("410","Black Poncho"),
new Item("411","Blue Poncho"),
new Item("412","Green Poncho"),
new Item("413","Orange Poncho"),
new Item("414","Purple Poncho"),
new Item("415","Red Poncho"),
new Item("416","White Poncho"),
new Item("417","Yellow Poncho"),
new Item("418","Headphones"),
new Item("419","Flight Jacket"),
new Item("420","Biker Jacket"),
new Item("421","Suit Top"),
new Item("422","Suit Bottom"),
new Item("423","Engineer Hat"),
new Item("424","Fedora"),
new Item("425","Black Cap"),
new Item("426","Blue Cap"),
new Item("427","Green Cap"),
new Item("428","Orange Cap"),
new Item("429","Purple Cap"),
new Item("430","Red Cap"),
new Item("431","White Cap"),
new Item("432","Yellow Cap"),
new Item("433","Forest Beret"),
new Item("434","Black Balaclava"),
new Item("435","Blue Balaclava"),
new Item("436","Green Balaclava"),
new Item("437","Orange Balaclava"),
new Item("438","Purple Balaclava"),
new Item("439","Red Balaclava"),
new Item("440","White Balaclava"),
new Item("441","Yellow Balaclava"),
new Item("442","Maple Rampart"),
new Item("443","Maple Post"),
new Item("444","Birch Rampart"),
new Item("445","Pine Rampart"),
new Item("446","Metal Rampart"),
new Item("447","Birch Post"),
new Item("448","Pine Post"),
new Item("449","Metal Post"),
new Item("450","Maple Garage"),
new Item("451","Maple Gate"),
new Item("452","Birch Garage"),
new Item("453","Pine Garage"),
new Item("454","Metal Garage"),
new Item("455","Birch Gate"),
new Item("456","Pine Gate"),
new Item("457","Metal Gate"),
new Item("458","Generator"),
new Item("459","Spotlight"),
new Item("460","Bread"),
new Item("461","Tuna Sandwich"),
new Item("462","Milk Box"),
new Item("463","Orange Juice"),
new Item("464","Cheese"),
new Item("465","Canned Soda"),
new Item("466","Grilled Cheese Sandwich"),
new Item("467","BLT Sandwich"),
new Item("468","Ham Sandwich"),
new Item("469","Canned Ham"),
new Item("470","Eggs"),
new Item("471","Cake"),
new Item("472","Bottled Cola"),
new Item("473","Bottled Soda"),
new Item("474","Maple Rifle"),
new Item("475","Rifle Iron Sights"),
new Item("476","Makeshift Scope"),
new Item("477","Makeshift Muffler"),
new Item("478","Rifle Clip"),
new Item("479","Birch Rifle"),
new Item("480","Pine Rifle"),
new Item("481","Maple Bottle"),
new Item("482","Birch Bottle"),
new Item("483","Pine Bottle"),
new Item("484","Sportshot"),
new Item("485","Sportshot Magazine"),
new Item("486","Sportshot Iron Sights"),
new Item("487","Makeshift Bat"),
new Item("488","Desert Falcon"),
new Item("489","Desert Falcon Magazine"),
new Item("490","Chainsaw"),
new Item("491","Gold Aviators"),
new Item("492","3D Glasses"),
new Item("493","Key"),
new Item("494","Mystery Box 0"),
new Item("495","Fez"),
new Item("496","Crown"),
new Item("497","Obi"),
new Item("498","Antlers"),
new Item("499","Paper Hat"),
new Item("500","Headress"),
new Item("501","Viking Helmet"),
new Item("502","Eyepatch"),
new Item("503","Fishing Rod"),
new Item("504","Raw Trout"),
new Item("505","Raw Salmon"),
new Item("506","Birch Rod"),
new Item("507","Maple Rod"),
new Item("508","Pine Rod"),
new Item("509","Fishing Hat"),
new Item("510","Fishing Top"),
new Item("511","Fishing Bottom"),
new Item("512","Cooked Trout"),
new Item("513","Cooked Salmon"),
new Item("514","Raw Venison"),
new Item("515","Cooked Venison"),
new Item("516","Leather"),
new Item("517","Leather Top"),
new Item("518","Leather Bottom"),
new Item("519","Rocket Launcher"),
new Item("520","Rocket"),
new Item("521","Rocket Iron Sights"),
new Item("1000","Matamorez"),
new Item("1001","Matamorez Iron Sights"),
new Item("1002","Matamorez Barrel"),
new Item("1003","Matamorez Magazine"),
new Item("1004","Red Kobra Sight"),
new Item("1005","Matamorez Box"),
new Item("1006","Cobra Box"),
new Item("1007","Adaptive Chambering"),
new Item("1008","Rangefinder"),
new Item("1009","Desert Beret"),
new Item("1010","Desert Military Helmet"),
new Item("1011","Desert Military Top"),
new Item("1012","Desert Military Bottom"),
new Item("1013","Desert Military Vest"),
new Item("1014","Leather Pack"),
new Item("1015","Biohazard Top"),
new Item("1016","Biohazard Bottom"),
new Item("1017","Biohazard Hood"),
new Item("1018","Sabertooth"),
new Item("1019","Sabertooth Iron Sights"),
new Item("1020","Sabertooth Magazine"),
new Item("1021","Avenger"),
new Item("1022","Avenger Magazine"),
new Item("1023","Baton"),
new Item("1024","Peacemaker"),
new Item("1025","Peacemaker Iron Sights"),
new Item("1026","Peacemaker Magazine"),
new Item("1027","Viper"),
new Item("1028","Viper Iron Sights"),
new Item("1029","Viper Magazine"),
new Item("1030","Frying Pan"),
new Item("1031","Shovel"),
new Item("1032","Crowbar"),
new Item("1033","Paddle"),
new Item("1034","Pitchfork"),
new Item("1035","Machete"),
new Item("1036","Katana"),
new Item("1037","Heartbreaker"),
new Item("1038","Heartbreaker Iron Sights"),
new Item("1039","Kryzkarek"),
new Item("1040","Kryzkarek Magazine"),
new Item("1041","Yuri"),
new Item("1042","Yuri Magazine"),
new Item("1043","Yuri Iron Sights"),
new Item("1044","Civilian Nightvision"),
new Item("1045","Pumpkin Seed"),
new Item("1046","Pumpkin"),
new Item("1047","Pumpkin Pie"),
new Item("1048","Hockey Mask"),
new Item("1049","Jack-o-Lantern"),
new Item("1050","Safezone Radiator"),
new Item("1051","$5 Note"),
new Item("1052","$10 Note"),
new Item("1053","$20 Note"),
new Item("1054","$50 Note"),
new Item("1055","$100 Note"),
new Item("1056","Loonie"),
new Item("1057","Toonie"),
new Item("1058","Large Maple Plate"),
new Item("1059","Small Maple Plate"),
new Item("1060","Large Maple Frame"),
new Item("1061","Small Maple Frame"),
new Item("1062","Maple Siding"),
new Item("1063","Maple Pipe"),
new Item("1064","Large Birch Plate"),
new Item("1065","Small Birch Plate"),
new Item("1066","Large Birch Frame"),
new Item("1067","Small Birch Frame"),
new Item("1068","Birch Siding"),
new Item("1069","Birch Pipe"),
new Item("1070","Large Pine Plate"),
new Item("1071","Small Pine Plate"),
new Item("1072","Large Pine Frame"),
new Item("1073","Small Pine Frame"),
new Item("1074","Pine Siding"),
new Item("1075","Pine Pipe"),
new Item("1076","Amber Pie"),
new Item("1077","Indigo Pie"),
new Item("1078","Jade Pie"),
new Item("1079","Mauve Pie"),
new Item("1080","Russet Pie"),
new Item("1081","Teal Pie"),
new Item("1082","Vermillion Pie"),
new Item("1083","Maple Doorframe"),
new Item("1084","Maple Garageframe"),
new Item("1085","Birch Doorframe"),
new Item("1086","Birch Garageframe"),
new Item("1087","Pine Doorframe"),
new Item("1088","Pine Garageframe"),
new Item("1089","Metal Doorframe"),
new Item("1090","Metal Garageframe"),
new Item("1091","Large Metal Plate"),
new Item("1092","Small Metal Plate"),
new Item("1093","Metal Siding"),
new Item("1094","Metal Pipe"),
new Item("1095","Maple Sign"),
new Item("1096","Birch Sign"),
new Item("1097","Pine Sign"),
new Item("1098","Metal Sign"),
new Item("1099","4 Seater Makeshift Vehicle"),
new Item("1100","C4"),
new Item("1101","Landmine"),
new Item("1102","Claymore"),
new Item("1103","Umbrella"),
new Item("1104","Amber Berry Seed"),
new Item("1105","Indigo Berry Seed"),
new Item("1106","Jade Berry Seed"),
new Item("1107","Mauve Berry Seed"),
new Item("1108","Russet Berry Seed"),
new Item("1109","Teal Berry Seed"),
new Item("1110","Vermillion Berry Seed"),
new Item("1111","6 Seater Makeshift Vehicle"),
new Item("1112","1 Seater Makeshift Vehicle"),
new Item("1113","Snare"),
new Item("1114","Maple Jerrycan"),
new Item("1115","Birch Jerrycan"),
new Item("1116","Pine Jerrycan"),
new Item("1117","Pork"),
new Item("1118","Bacon"),
new Item("1119","Barbedwire Fence"),
new Item("1120","Raw Beef"),
new Item("1121","Cooked Beef"),
new Item("00000","enterName")
            };
        }
    }
}
