using System;
using System.Collections.Generic;
using Models.Library.Shared.Game;
using UnityEngine;

namespace Assets.Scripts.Server
{
	public class UnitCrow : MonoBehaviour
	{
		public int crowID;

		public ushort tyrannyOwner;

		public SNativeID crowDataId;

		public LinkedListNode<UnitCrow> node;

		[NonSerialized]
		public EnemyAiController enemyController;
	}
}
