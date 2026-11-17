using UnityEngine;
using UnityEngine.EventSystems;

namespace RANMobile.Authentication
{
	public class CharacterRotator : MonoBehaviour, IDragHandler, IEventSystemHandler
	{
		[Header("Setup")]
		[Tooltip("Drag your CharCreationSpawn object here!")]
		public Transform targetToRotate;

		[Tooltip("Adjust this to make the spin faster or slower.")]
		public float rotationSpeed;

		public void OnDrag(PointerEventData eventData)
		{
		}
	}
}
