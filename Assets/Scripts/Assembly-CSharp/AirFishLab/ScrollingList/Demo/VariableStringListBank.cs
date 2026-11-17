using UnityEngine;
using UnityEngine.UI;

namespace AirFishLab.ScrollingList.Demo
{
	public class VariableStringListBank : BaseListBank
	{
		public class DataWrapper
		{
			public string data;
		}

		[SerializeField]
		private InputField _contentInputField;

		[SerializeField]
		private string[] _contents;

		[SerializeField]
		private CircularScrollingList _circularList;

		[SerializeField]
		private CircularScrollingList _linearList;

		private readonly DataWrapper _dataWrapper;

		public void ChangeContents()
		{
		}

		public override object GetListContent(int index)
		{
			return null;
		}

		public override int GetListLength()
		{
			return 0;
		}
	}
}
