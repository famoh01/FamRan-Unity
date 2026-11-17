using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.Effects;

namespace UnityStandardAssets.SceneUtils
{
	public class ParticleSceneControls : MonoBehaviour
	{
		public enum Mode
		{
			Activate = 0,
			Instantiate = 1,
			Trail = 2
		}

		public enum AlignMode
		{
			Normal = 0,
			Up = 1
		}

		[Serializable]
		public class DemoParticleSystem
		{
			public Transform transform;

			public Mode mode;

			public AlignMode align;

			public int maxCount;

			public float minDist;

			public int camOffset;

			public string instructionText;
		}

		[Serializable]
		public class DemoParticleSystemList
		{
			public DemoParticleSystem[] items;
		}

		public DemoParticleSystemList demoParticles;

		public float spawnOffset;

		public float multiply;

		public bool clearOnChange;

		public Text titleText;

		public Transform sceneCamera;

		public Text instructionText;

		public Button previousButton;

		public Button nextButton;

		public GraphicRaycaster graphicRaycaster;

		public EventSystem eventSystem;

		private ParticleSystemMultiplier m_ParticleMultiplier;

		private List<Transform> m_CurrentParticleList;

		private Transform m_Instance;

		private static int s_SelectedIndex;

		private Vector3 m_CamOffsetVelocity;

		private Vector3 m_LastPos;

		private static DemoParticleSystem s_Selected;

		private void Awake()
		{
		}

		private void OnDisable()
		{
		}

		private void Previous()
		{
		}

		public void Next()
		{
		}

		private void Update()
		{
		}

		private bool CheckForGuiCollision()
		{
			return false;
		}

		private void Select(int i)
		{
		}
	}
}
