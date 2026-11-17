using UnityEngine;

namespace RAN.Mobile.GameModels
{
	public class Gate
	{
		public GateData GateIdName { get; set; }

		public bool GateIn { get; set; }

		public bool GateOut { get; set; }

		public SNativeID ToMapId { get; set; }

		public byte ToGateId { get; set; }

		public MinMaxModel GenInterval { get; set; }

		public Vector3 GatePoint { get; set; }
	}
}
