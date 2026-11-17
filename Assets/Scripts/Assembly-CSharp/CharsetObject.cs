using System.Collections.Generic;
using Core.Enumerators;
using Newtonsoft.Json;
using RAN.Mobile.Enumerators;
using RAN.Mobile.GameModels;
using UnityEngine;

[CreateAssetMenu(fileName = "New Charset", menuName = "Scriptable Objects/EditorCharset/New Charset")]
public class CharsetObject : ScriptableObject
{
	public TribeEnum tribe;

	public RAN.Mobile.Enumerators.CharacterClassEnum emClass;

	public RAN.Mobile.Enumerators.SchoolEnum school;

	public byte hair;

	public byte face;

	public short bright;

	public ushort level;

	public ushort guild;

	public double money;

	public ushort statsPoint;

	public ushort skillPoint;

	public PlayerConstant CharConstant;

	public PAFData experience;

	public PAData hp;

	public PAData mp;

	public PAData sp;

	public ushort pk;

	public SNativeID startMapId;

	public byte startGate;

	[JsonIgnore]
	public Vector3 startPos;

	public SNativeID[] item;

	public List<int> quest;

	public DefaultCharSetPutOnItem[] putOnItems;

	public DefaultCharSetInventory[] inventory;
}
