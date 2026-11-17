using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Core.Enumerators;
using Models.Library.Shared.Game;
using Models.Network.Shared.Response;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RANMobile.Authentication
{
	public class CharacterSelectionData : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CDelay_003Ed__46 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public GameObject child;

			public GameCharData2 selectedData;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CDelay_003Ed__46(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CMoveCamera_003Ed__42 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public CharacterSelectionData _003C_003E4__this;

			public Vector3 targetPos;

			public Quaternion targetRot;

			private Vector3 _003CstartPos_003E5__2;

			private Quaternion _003CstartRot_003E5__3;

			private float _003Celapsed_003E5__4;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CMoveCamera_003Ed__42(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[SerializeField]
		public GameObject[] _CharSelectList;

		[SerializeField]
		public GameObject[] _characterPrefab;

		[SerializeField]
		public Transform[] _characterSpawnAreaMale;

		[SerializeField]
		public Transform[] _characterSpawnAreaFemale;

		[SerializeField]
		private Button _createButton;

		[SerializeField]
		private Button _deleteButton;

		[SerializeField]
		private Button _startButton;

		[SerializeField]
		private TextMeshProUGUI _createRemain;

		[Header("Inspect Mode Setup")]
		public Transform inspectSpawnPoint;

		[Header("Camera Zoom Settings")]
		public Camera mainCamera;

		public float zoomDuration;

		[Tooltip("Drag the 4 empty GameObjects that represent the perfect camera view for each slot.")]
		public Transform[] cameraZoomPositions;

		private Vector3 originalCameraPosition;

		private Quaternion originalCameraRotation;

		private Coroutine cameraMoveCoroutine;

		[Header("New Profile Panel (Left Side)")]
		public GameObject profilePanel;

		public Button backButton;

		public TextMeshProUGUI profileNameText;

		public TextMeshProUGUI profileLevelText;

		public TextMeshProUGUI profileClassText;

		public TextMeshProUGUI profileSchoolText;

		[Header("Extended Lore Texts")]
		public TextMeshProUGUI profileClassNameText;

		public TextMeshProUGUI profileClassDescText;

		public TextMeshProUGUI profileWeaponNameText;

		public TextMeshProUGUI profileWeaponTypeText;

		[Header("Profile Icons (UI Image Components)")]
		public Image profileClassIcon;

		public Image profileSchoolIcon;

		public Image profileWeaponIcon;

		[Header("Club & EXP")]
		public TextMeshProUGUI profileClubText;

		[Tooltip("Drag the actual 'ExpBar' image here, not the BG or Border!")]
		public Image profileExpBarFill;

		[Header("Icon Sprites (Drag your 2D pictures here)")]
		[Tooltip("Order: 0:Brawler, 1:Swordsman, 2:Archer, 3:Shaman")]
		public Sprite[] classIcons;

		[Tooltip("Order: 0:SacredGate, 1:MysticPeak, 2:Phoenix")]
		public Sprite[] schoolIcons;

		[Tooltip("Order: 0:Gauntlet, 1:Sword, 2:Bow, 3:Wand")]
		public Sprite[] defaultWeaponIcons;

		public List<CharacterSelected> selectedCharacters;

		private Animator _animator;

		public GameObject[] charGo;

		public GameObject[] effectGo;

		[Header("Inspect Navigation UI")]
		public Button previousCharButton;

		public Button nextCharButton;

		[Header("State Tracking")]
		public bool isInspecting;

		private CharacterSelected currentlyInspecting;

		private void Start()
		{
		}

		[IteratorStateMachine(typeof(_003CMoveCamera_003Ed__42))]
		private IEnumerator MoveCamera(Vector3 targetPos, Quaternion targetRot)
		{
			return null;
		}

		public void FetchCharacterData()
		{
		}

		private void OnCharacterListReceived(CharacterListResponse response)
		{
		}

		public List<CharacterSelected> GetAllSelectedCharacters()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CDelay_003Ed__46))]
		private IEnumerator Delay(GameCharData2 selectedData, GameObject child)
		{
			return null;
		}

		private void SetCreateButton(int charRemain)
		{
		}

		public GameObject GetCharacterGO(GameObject GO)
		{
			return null;
		}

		public void OnCharListClick(int charCount, GameCharData2 selectedData, GameObject child)
		{
		}

		public void OnBackClicked()
		{
		}

		private string GetClassDescription(CharacterClassEnum charClass)
		{
			return null;
		}

		public void ResourcesLoader(string path)
		{
		}

		private void SetSelectedData(GameCharData2 selectedData)
		{
		}

		public void OnPreviousCharacterClick()
		{
		}

		public void OnNextCharacterClick()
		{
		}

		private void SwitchInspectedCharacter(CharacterSelected newChar)
		{
		}
	}
}
