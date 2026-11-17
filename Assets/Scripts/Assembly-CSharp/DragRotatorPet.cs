using UnityEngine;
using UnityEngine.EventSystems;

public class DragRotatorPet : MonoBehaviour, IDragHandler, IEventSystemHandler
{
	[Header("Dependencies")]
	[Tooltip("Drag the GameObject holding the PetPanelHandler here.")]
	public PetPanelHandler targetPanelHandler;

	[Header("Settings")]
	[Tooltip("Adjust how fast the pet spins relative to your finger movement.")]
	public float rotationSensitivity;

	public void OnDrag(PointerEventData eventData)
	{
	}
}
