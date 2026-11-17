using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class JSONExporter : MonoBehaviour
{
	public class ShouldSerializeContractResolver : DefaultContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			return null;
		}
	}

	private class ExcludeObsoletePropertiesResolver : DefaultContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			return null;
		}
	}

	public GameObject glogic;

	public GameObject levFile;

	public static JSONExporter instance;

	private void OnValidate()
	{
	}

	public void ExportToJsonLev()
	{
	}

	public void ExportToJson()
	{
	}

	private void SaveToJsonFile(string fileName, string folder, string json)
	{
	}
}
