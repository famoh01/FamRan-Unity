using Core.Enumerators;
using Models.Library.Shared.Game;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
	public interface IActor
	{
		uint GetId();

		CrowType GetCrow();

		ushort GetBodyRadius();

		Vector3 GetPosition();

		bool ReceiveSkillFact(SNativeID skillID, ushort level, uint select, ushort casterCrow, uint casterID, float age = -1f);
	}
}
