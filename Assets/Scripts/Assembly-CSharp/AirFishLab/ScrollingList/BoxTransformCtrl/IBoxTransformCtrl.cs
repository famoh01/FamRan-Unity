using UnityEngine;

namespace AirFishLab.ScrollingList.BoxTransformCtrl
{
	public interface IBoxTransformCtrl
	{
		void SetInitialTransform(Transform transform, int boxID, int numOfBoxes);

		void SetLocalTransform(Transform boxTransform, float delta, out bool needToUpdateToLastContent, out bool needToUpdateToNextContent);
	}
}
