using EXO;
using System;
using System.Reflection;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace xButtonAPI.Misc
{
	internal class ComponentManager : BaseModule
	{
		public override void OnApplicationStart()
		{
			var types = Assembly.GetExecutingAssembly().GetTypes();
			for (var i = 0; i < types.Length; i++)
			{
				RegisterTypeRecursive(types[i]);
			}
		}

		private static void RegisterTypeRecursive(Type t)
		{
			if (!(t == null) && t.IsSubclassOf(typeof(MonoBehaviour)))
			{
				ClassInjector.RegisterTypeInIl2Cpp(t, false);
			}
		}
	}
}
