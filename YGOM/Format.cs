using System;
using System.Collections.Generic;

namespace YGOM
{
	public abstract class Format
	{
		public static Format GetInstance()
		{
			return Format.SingleInstance;
		}

		public abstract byte[] Serialize(Dictionary<string, object> dict, byte[] token);

		private static readonly Format SingleInstance = new FormatYgom();
	}
}
