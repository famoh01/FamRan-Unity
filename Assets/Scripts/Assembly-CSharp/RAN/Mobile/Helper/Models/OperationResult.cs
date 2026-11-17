namespace RAN.Mobile.Helper.Models
{
	public class OperationResult<T> where T : class
	{
		public T Data { get; set; }

		public string Message { get; set; }

		public bool Succcess { get; set; }
	}
}
