using System.Collections.Generic;
using UnityEngine;

namespace RANMobile
{
	[CreateAssetMenu(fileName = "New Scene Group Set", menuName = "Scriptable Objects/ETC/Manager/New Scene Group Set")]
	public class SceneGroupSet : ScriptableObject
	{
		[SerializeField]
		private List<SceneSet> sceneSets;

		public List<SceneSet> SceneSets => null;
	}
}
