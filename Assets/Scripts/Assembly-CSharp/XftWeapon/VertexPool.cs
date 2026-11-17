using UnityEngine;

namespace XftWeapon
{
	public class VertexPool
	{
		public class VertexSegment
		{
			public int VertStart;

			public int IndexStart;

			public int VertCount;

			public int IndexCount;

			public VertexPool Pool;

			public VertexSegment(int start, int count, int istart, int icount, VertexPool pool)
			{
			}

			public void ClearIndices()
			{
			}
		}

		public Vector3[] Vertices;

		public int[] Indices;

		public Vector2[] UVs;

		public Color[] Colors;

		public bool IndiceChanged;

		public bool ColorChanged;

		public bool UVChanged;

		public bool VertChanged;

		public bool UV2Changed;

		protected int VertexTotal;

		protected int VertexUsed;

		protected int IndexTotal;

		protected int IndexUsed;

		public bool FirstUpdate;

		protected bool VertCountChanged;

		public const int BlockSize = 108;

		public float BoundsScheduleTime;

		public float ElapsedTime;

		protected XWeaponTrail _owner;

		protected MeshFilter _meshFilter;

		protected Mesh _mesh2d;

		protected Material _material;

		public Mesh MyMesh => null;

		public void RecalculateBounds()
		{
		}

		public void SetMeshObjectActive(bool flag)
		{
		}

		private void CreateMeshObj(XWeaponTrail owner, Material material)
		{
		}

		public void Destroy()
		{
		}

		public VertexPool(Material material, XWeaponTrail owner)
		{
		}

		public VertexSegment GetVertices(int vcount, int icount)
		{
			return null;
		}

		protected void InitArrays()
		{
		}

		public void EnlargeArrays(int count, int icount)
		{
		}

		public void LateUpdate()
		{
		}
	}
}
