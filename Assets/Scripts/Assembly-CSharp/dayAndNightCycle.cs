using UnityEngine;

public class dayAndNightCycle : MonoBehaviour
{
	public Light directionalLight;

	public Material skyboxMaterial;

	public Color dayColor;

	public Color nightColor;

	public Color dayFogColor;

	public Color nightFogColor;

	public float dayIntensity;

	public float nightIntensity;

	private float transitionFactor;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void UpdateLighting()
	{
	}
}
