using System;
using System.Collections.Generic;

namespace RAN.Mobile.GameModels
{
	public class ObjectPool<T> where T : new()
	{
		private readonly Stack<T> objects;

		private readonly Func<T> objectGenerator;

		public ObjectPool()
		{
		}

		public ObjectPool(Func<T> objectGenerator)
		{
		}

		public T Get()
		{
			return default;
		}

		public void Return(T item)
		{
		}
	}
}
