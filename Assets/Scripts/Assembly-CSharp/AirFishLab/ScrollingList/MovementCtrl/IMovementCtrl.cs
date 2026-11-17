namespace AirFishLab.ScrollingList.MovementCtrl
{
	public interface IMovementCtrl
	{
		void SetMovement(float baseValue, bool flag);

		void SetSelectionMovement(float distance);

		bool IsMovementEnded();

		float GetDistance(float deltaTime);
	}
}
