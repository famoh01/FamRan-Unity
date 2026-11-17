using System.Collections.Generic;
using UnityEngine;

namespace RAN.Mobile.GameModels
{
	public class CrowData
	{
		public SNativeID Crow { get; set; }

		public int GenTime { get; set; }

		public int GenMinute { get; set; }

		public int GenHour { get; set; }

		public bool Sunday { get; set; }

		public bool Monday { get; set; }

		public bool Tuesday { get; set; }

		public bool Wednesday { get; set; }

		public bool Thursday { get; set; }

		public bool Friday { get; set; }

		public bool Saturday { get; set; }

		public Vector3 GenPos { get; set; }

		public List<WalkPoint> WalkPoints { get; set; }

		public Vector3 Rotation { get; set; }
	}
}
