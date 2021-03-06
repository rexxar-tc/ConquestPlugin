﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NLog;
using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.ModAPI;
using SEModAPI.API;
using SEModAPIExtensions.API;
using SEModAPIInternal.API.Common;
using SEModAPIInternal.API.Entity;
using SEModAPIInternal.API.Entity.Sector.SectorObject;
using SEModAPIInternal.API.Server;
using VRage;
using VRageMath;
using VRage.ObjectBuilders;

namespace ConquestPlugin.Utility
{
	class ChatUtil
	{
		private static readonly Logger Log = LogManager.GetLogger("PluginLog");
		private static Random m_random = new Random();

		public static void SendPublicChat(string message)
		{
			if (message == "") { return; }

			ChatManager.Instance.SendPublicChatMessage(message);
		}

		public static void SendPrivateChat(ulong steamID, string message)
		{
			if (message == "") { return; }

			ChatManager.Instance.SendPrivateChatMessage(steamID, message);
		}
		
		public static void DisplayDialog(ulong steamId, string header, string subheader, string content, string buttonText = "OK")
		{
			SendClientMessage(steamId, string.Format("/dialog \"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\"", header, subheader, " ", content.Replace("\r\n", "|"), buttonText));
		}
		
        public static void AddIngot(ulong steamID, String subID, long amount )
        {
				SendClientMessage(steamID, string.Format("/addingot \"{0}\" \"{1}\"", subID, amount));
        }

		public static void AddComp(ulong steamID, String subID, long amount)
		{
				SendClientMessage(steamID, string.Format("/addcomp \"{0}\" \"{1}\"", subID, amount));
		}

		public static void SendClientMessage(ulong steamId, string message)
		{
			if (PlayerMap.Instance.GetPlayerIdsFromSteamId(steamId).Count < 1)
			{
				Log.Info(string.Format("Unable to locate playerId for user with steamId: {0}", steamId));
				return;
			}           
			CubeGridEntity entity = new CubeGridEntity(new FileInfo(Conquest.PluginPath + "CommRelay.sbc"));
			long entityId = BaseEntity.GenerateEntityId();
			entity.EntityId = entityId;
			entity.DisplayName = string.Format("CommRelayOutput{0}", PlayerMap.Instance.GetPlayerIdsFromSteamId(steamId).First());
			entity.PositionAndOrientation = new MyPositionAndOrientation(VRageMath.GenerateRandomEdgeVector(), Vector3.Forward, Vector3.Up);

			foreach (MyObjectBuilder_CubeBlock block in entity.BaseCubeBlocks)
			{
				MyObjectBuilder_Beacon beacon = block as MyObjectBuilder_Beacon;
				if (beacon != null)
				{
                   
					beacon.CustomName = message;
				}
			}
           
			SectorObjectManager.Instance.AddEntity(entity);
			
		}

		public static bool CheckPlayerIsInWorld(ulong steamID)
		{
			IMyPlayerCollection allPlayers = MyAPIGateway.Players;
			List<IMyPlayer> listPlayers = new List<IMyPlayer>();
			allPlayers.GetPlayers(listPlayers);
			foreach (IMyPlayer currentPlayer in listPlayers)
			{
				if (currentPlayer.SteamUserId == steamID)
				{
					String ControlledEntity = currentPlayer.Controller.ControlledEntity.ToString();
					if (ControlledEntity.Contains("Astronau"))
					{
						return true;
					}
					else
					{
						ChatUtil.SendPrivateChat(steamID, "Please exit cockpit and try again.");
						return false;
					}
				}
			}
			return false;
		}
	}
}
