using UnityEngine;

public sealed class NameplateCuller
{
	private Canvas _canvas;

	private Billboard _billboard;

	private WorldSpaceUiConstantSize _constSize;

	private NameDisplayPosController _posCtrl;

	private bool _resolved;

	private bool _shown;

	private bool _inRange;

	public bool IsShown => false;

	private void Resolve(GameObject root)
	{
	}

	public void Update(GameObject root, float sqrDist, float showDistSqr, float hideDistSqr, bool onScreen = true)
	{
	}

	public void SetShown(bool show)
	{
	}

	public void ResetForReuse()
	{
	}
}
