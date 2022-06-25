using System;
using System.Collections.Generic;
using UnityEngine;
using VRC.Udon;

namespace DruUdonStuff
{	
	internal class UdonStuff
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
			UdonBehaviour[] array = UnityEngine.Object.FindObjectsOfType<UdonBehaviour>();
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
	}
}
