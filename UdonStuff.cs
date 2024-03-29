﻿using System;
using System.Collections.Generic;
using UnityEngine;
using VRC;
using VRC.SDKBase;
using VRC.Udon;
using Wrapper.PlayerWrapper;
using Wrapper.WorldWrapper;

namespace EXO_Udon
{	
	internal static class UdonStuff
	{
		public static void SendUdonEventAll(string obj, string udonEvent)
		{
			GameObject gameObject = GameObject.Find(obj);
			bool flag = gameObject;
			if (flag)
			{
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, udonEvent);
			}
		}
		public static void SendUdonEventOwner(string obj, string udonEvent)
		{
			GameObject gameObject = GameObject.Find(obj);
			bool flag = gameObject;
			if (flag)
			{
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, udonEvent);
			}
		}
		public static void SendUdonEventsWithName(string udonEvent)
		{
			UdonBehaviour[] array = WorldWrapper.udonBehaviours;
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < array.Length; i++)
			{
				foreach (string a in array[i]._eventTable.Keys)
				{
					bool flag = a == udonEvent && !list.Contains(array[i].gameObject);
					if (flag)
					{
						list.Add(array[i].gameObject);
					}
				}
			}
			foreach (GameObject gameObject in list)
			{
				bool flag2 = gameObject;
				if (flag2)
				{
					gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, udonEvent);
				}
			}
		}
		public static VRC.Player GrabOwner(this GameObject gameObject)
		{
			foreach (VRC.Player player in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
			{
				if (player.field_Private_VRCPlayerApi_0.IsOwner(gameObject))
				{
					return player;
				}
			}
			return null;
		}
		public static void SetEventOwner(this GameObject gameObject, VRC.Player player)
		{
			if (GrabOwner(gameObject) != player)
			{
				Networking.SetOwner(player.field_Private_VRCPlayerApi_0, gameObject);
			}
		}
		public static void TargetedEvent(string udonevent, VRCPlayer player)
		{
			for (int j = 0; j < WorldWrapper.udonBehaviours.Length; j++)
			{
				foreach (string name in WorldWrapper.udonBehaviours[j]._eventTable.Keys)
				{
					if (name == udonevent)
					{
						SetEventOwner(WorldWrapper.udonBehaviours[j].gameObject, player.prop_Player_0);
						WorldWrapper.udonBehaviours[j].SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, name);
					}
				}
			}
		}
		internal static void UdonSend(string udonEvent, string targetnetwork)
		{
			switch (targetnetwork)
			{
				case "target":
					{
						for (int j = 0; j < WorldWrapper.udonBehaviours.Length; j++)
						{
							foreach (string name in WorldWrapper.udonBehaviours[j]._eventTable.Keys)
							{
								if (name == udonEvent)
								{
									SetEventOwner(WorldWrapper.udonBehaviours[j].gameObject, PlayerWrapper.SelectedVRCPlayer());
									WorldWrapper.udonBehaviours[j].SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, name);
								}
							}
						}
					}
					break;
				case "everyone":
					{
						for (int j = 0; j < WorldWrapper.udonBehaviours.Length; j++)
						{
							foreach (string name in WorldWrapper.udonBehaviours[j]._eventTable.Keys)
							{
								if (name == udonEvent)
								{
									WorldWrapper.udonBehaviours[j].SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, name);
								}
							}
						}
					}
					break;
				case "local":
					{
						for (int j = 0; j < WorldWrapper.udonBehaviours.Length; j++)
						{
							foreach (string name in WorldWrapper.udonBehaviours[j]._eventTable.Keys)
							{
								if (name == udonEvent)
								{
									SetEventOwner(WorldWrapper.udonBehaviours[j].gameObject, PlayerWrapper.LocalPlayer());
									WorldWrapper.udonBehaviours[j].SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, name);
								}
							}
						}
					}
					break;
			}
		}
	}
}
