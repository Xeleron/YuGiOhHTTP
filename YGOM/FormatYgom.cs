using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YGOM
{
	public class FormatYgom : Format
	{
		public override byte[] Serialize(Dictionary<string, object> dict, byte[] token)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				string s = Json.Serialize(dict);
				byte[] bytes = Encoding.UTF8.GetBytes(s);
				short value = (short)token.Length;
				byte[] bytes2 = BitConverter.GetBytes((short)bytes.Length);
				byte[] bytes3 = BitConverter.GetBytes(value);
				memoryStream.Write(bytes3, 0, bytes3.Length);
				memoryStream.Write(bytes2, 0, bytes2.Length);
				memoryStream.Write(token, 0, token.Length);
				memoryStream.Write(bytes, 0, bytes.Length);
				result = memoryStream.ToArray();
			}
			return result;
		}
	}
}
