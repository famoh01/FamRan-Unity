using System.Runtime.CompilerServices;
using Models.Library.Shared.Game;
using UnityEngine;

namespace Assets.Scripts
{
	public static class GameVec3UnityExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ToUnity(this GameVec3 v)
		{
			return default;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameVec3 ToGame(this Vector3 v)
		{
			return default;
		}
	}
}
