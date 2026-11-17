using System.Collections.Generic;
using UnityEngine;

namespace RANMobile
{
	[CreateAssetMenu(fileName = "New Scene Set", menuName = "Scriptable Objects/ETC/Manager/New Scene Set")]
	public class SceneSet : ScriptableObject
	{
		[SerializeField]
		private new string name;

		[SerializeField]
		private string description;

		[SerializeField]
		private string environment;

		[SerializeField]
		private List<GameObject> sceneParts;

		public List<GameObject> SceneParts => null;

		public string Description => null;

		public string Name => null;

		public string Environment => null;
	}
}
