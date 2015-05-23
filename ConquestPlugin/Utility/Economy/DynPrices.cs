﻿using System;
using System.Collections.Generic;
using System.Linq;

using ConquestPlugin.Utility;
using ConquestPlugin.Utility.Shop;
using ConquestPlugin.GameModes;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.Definitions;
using Sandbox.ModAPI;

namespace ConquestPlugin.Utility.Shop
{
    class DynShopPrices
    {
        private enum itemvalues
        {
            Gravel = 1,
            IronIngots = 5,
            SiliconWafers = 5,
            NickelIngots = 7,
            CobaltIngots = 8,
            SilverIngots = 8,
            GoldIngots = 9,
            UraniumIngots = 12,
            MagnesiumPowder = 8,
            PlatinumIngots = 8
        }
        public static List<ShopItem> DynPrices(List<ShopItem> shopitems)
        {
            float worlddensity;
            MyObjectBuilder_PhysicalMaterialDefinition materials = new MyObjectBuilder_PhysicalMaterialDefinition();
            worlddensity = materials.Density;
            long capturedastroids = GetCapturedAstroids();
            foreach (ShopItem item in shopitems)
            {
                switch (item.ItemName)
                {
                    case ("Gravel"):
                        {
                           item.ItemPrice =  GetValue(capturedastroids, (float)itemvalues.Gravel);
                            break;
                        }
                    case("IronIngots"):
                        {
                            item.ItemPrice = GetValue(capturedastroids, (float)itemvalues.IronIngots);
                            break;
                        }
                    case("SiliconWafers"):
                        {
                            item.ItemPrice = GetValue(capturedastroids, (float)itemvalues.SiliconWafers);
                            break;
                        }
                    case("NickelIngots"):
                        {
                            item.ItemPrice = GetValue(capturedastroids, (float)itemvalues.NickelIngots);
                            break;
                        }
                    case("CobaltIngots"):
                        {
                            item.ItemPrice = GetValue(capturedastroids, (float)itemvalues.CobaltIngots);
                            break;
                        }
                    case("SilverIngots"):
                        {
                            item.ItemPrice = GetValue(capturedastroids, (float)itemvalues.SilverIngots);
                            break;
                        }
                    case("GoldIngots"):
                        {
                            item.ItemPrice = GetValue(capturedastroids, (float)itemvalues.GoldIngots);
                            break;
                        }
                    case("UraniumIngots"):
                        {
                            item.ItemPrice = GetValue(capturedastroids, (float)itemvalues.UraniumIngots);
                            break;
                        }
                    case("MagnesiumPowder"):
                        {
                            item.ItemPrice = GetValue(capturedastroids, (float)itemvalues.MagnesiumPowder);
                            break;
                        }
                    case("PlatinumIngots"):
                        {
                            item.ItemPrice = GetValue(capturedastroids, (float)itemvalues.PlatinumIngots);
                            break;
                        }
                    default:
                        break;
                }
            }
            return shopitems;
        }

        private static long GetValue(long capturedastroids , float relitivevalue)
        {
            if (capturedastroids == 0)
            {
                ChatUtil.SendPublicChat("DEBEG: ITEM {0} SHOULD HAVE A VALUE OF 50");
                //return 50;
                return (long)((long)(relitivevalue));
            }
            else
            {  
                ChatUtil.SendPublicChat("DEBEG: ITEM {0} SHOULD HAVE A VALUE OF 180");
                //return 180;
                return (long)(capturedastroids * ((long)(relitivevalue)));
            }
        }

        public static long GetCapturedAstroids()
        {
            long NumCapturedAstoids = 0;
            MyObjectBuilder_FactionCollection factionlist = MyAPIGateway.Session.GetWorld().Checkpoint.Factions;
            foreach (MyObjectBuilder_Faction faction in factionlist.Factions)
            {
                List<MyObjectBuilder_FactionMember> currentfacitonmembers = faction.Members;
                foreach(MyObjectBuilder_FactionMember currentmember in currentfacitonmembers)
                {
                    var leaders = GMConquest.Instance.Leaderboard.GroupBy(x => x.Value).Select(group => new { group.Key, Total = group.Count() }).OrderByDescending(x => x.Total);
                    foreach(var p in leaders)
                    {
                        if(p.Key == currentmember.PlayerId)
                        {
                            NumCapturedAstoids += p.Total;
                        }
                    }
                }
            }
            return NumCapturedAstoids;
        }
    }
}
