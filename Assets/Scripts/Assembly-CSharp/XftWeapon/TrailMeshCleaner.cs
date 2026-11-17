using System.Collections.Generic;
using UnityEngine;

namespace XftWeapon
{
	internal sealed class TrailMeshCleaner : MonoBehaviour
	{
		private static readonly List<TrailMeshCleaner> s_all;

		public XWeaponTrail TrailOwner;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		internal static void Sweep()
		{
		}
	}
}
