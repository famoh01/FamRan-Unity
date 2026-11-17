using System;
using Newtonsoft.Json;
using RAN.Mobile.GameModels;
using UnityEngine;

namespace Assets.Scripts
{
	[CreateAssetMenu(fileName = "New QGenItem", menuName = "Scriptable Objects/EditorGenItem/New QGenItem")]
	public class QGenItemObject : ScriptableObject
	{
		public SQTGenItemModel[] item;

		[NonSerialized]
		[JsonIgnore]
		public int itemNum;
	}
}
