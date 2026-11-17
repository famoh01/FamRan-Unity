using System.Collections.Generic;
using UnityEngine;

internal static class GhostResourcePool
{
	private const int MaxPooledMeshes = 128;

	private const int MaxPooledMaterials = 256;

	private const string GhostShaderPath = "_RANClient/Data/Effect/Shader/Mobile URP Ghost";

	private static readonly int MainTexID;

	private static readonly int BaseMapID;

	private static readonly int ColorID;

	private static readonly int BaseColorID;

	private static readonly int AtlasRegionID;

	private static readonly int SrcBlendID;

	private static readonly int DstBlendID;

	private static readonly int ZWriteID;

	private static readonly Stack<Mesh> meshes;

	private static readonly Stack<Material> materials;

	private static readonly HashSet<Material> rented;

	private static Shader s_ghostShader;

	private static Material s_hiddenMaterial;

	public static Shader LoadGhostShader()
	{
		return null;
	}

	public static Material HiddenMaterial(Shader shader)
	{
		return null;
	}

	public static Mesh RentMesh()
	{
		return null;
	}

	public static void ReturnMesh(Mesh m)
	{
	}

	public static Material RentMaterial(Shader shader)
	{
		return null;
	}

	private static void ResetForRent(Material mat)
	{
	}

	public static void ReturnMaterial(Material mat)
	{
	}
}
