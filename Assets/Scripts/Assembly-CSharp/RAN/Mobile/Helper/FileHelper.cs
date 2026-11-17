using System.Threading.Tasks;
using Newtonsoft.Json;
using RAN.Mobile.Helper.Models;

namespace RAN.Mobile.Helper
{
	public class FileHelper
	{
		public static Task<OperationResult<T>> ReadJsonFile<T>(string fileName, params JsonConverter[] converters) where T : class
		{
			return null;
		}

		public static Task<(bool, string)> WriteJsonFile(string path, string json)
		{
			return null;
		}
	}
}
