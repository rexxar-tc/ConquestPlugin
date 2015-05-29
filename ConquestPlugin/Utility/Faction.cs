﻿using Sandbox.Common;
using Sandbox.ModAPI;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.Definitions;
using System.Collections.Generic;
using SEModAPIInternal.API.Common;

namespace ConquestPlugin.Utility
{
	public class Faction
	{
		public static long getFactionID(ulong steamId)
		{
			long playerID = PlayerMap.Instance.GetPlayerIdsFromSteamId(steamId)[0];
			MyObjectBuilder_FactionCollection factioncollection = MyAPIGateway.Session.GetWorld().Checkpoint.Factions;
			foreach (MyObjectBuilder_Faction faction in factioncollection.Factions)
			{
				List<MyObjectBuilder_FactionMember> currentfaction = faction.Members;
				foreach (MyObjectBuilder_FactionMember currentmember in currentfaction)
				{
					if (currentmember.PlayerId == playerID)
					{
						return faction.FactionId;   
					}
				}
			}
			return 0;
		}
        public static long getFactionIDformName(string factname)
        {
            MyObjectBuilder_FactionCollection factioncollection = MyAPIGateway.Session.GetWorld().Checkpoint.Factions;
            foreach(MyObjectBuilder_Faction faction in factioncollection.Factions)
            {
                if (faction.Name == factname)
                {
                    return faction.FactionId;
                }
            }
            return 0;
        }

        public static MyObjectBuilder_Faction getFaction(long factionID)
        {
            MyObjectBuilder_FactionCollection factionlist = MyAPIGateway.Session.GetWorld().Checkpoint.Factions;
            foreach(MyObjectBuilder_Faction faction in factionlist.Factions)
            {
                if(faction.FactionId == factionID)
                {
                    return faction;
                }
            }
            return null;
        }
       

	}
}