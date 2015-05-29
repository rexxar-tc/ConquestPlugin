﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using ConquestPlugin.GameModes;
using ConquestPlugin.Utility;

namespace ConquestPlugin.Utility.Economy
{
    class Factionpointtransaction
    {
        public static bool transferFP(ulong userID,string factionname, int amount)
        {

            if (FactionPoints.RemoveFP((ulong)Faction.getFactionID(userID), amount) == true)
            {
                FactionPoints.AddFP(Faction.getFactionIDformName(factionname), amount);
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
