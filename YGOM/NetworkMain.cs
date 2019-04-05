using System;
using System.Collections.Generic;

namespace YGOM
{
	public static class NetworkMain
	{

		public static byte[] Entry(string cmd, Dictionary<string, object> param, string token, int id)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			dictionary2["act"] = cmd;
			dictionary2["id"] = id;
			Dictionary<string, object> dictionary3 = dictionary2;
			if (param != null && param.Count > 0)
			{
				dictionary3["params"] = param;
			}
			list.Add(dictionary3);
			dictionary["acts"] = list;
			dictionary["v"] = "2.2.1";
			dictionary["ua"] = "Steam/10.0.0/Steam";
			dictionary["h"] = Crc32.GetBinaryCrc32(BitConverter.GetBytes(0f));
			return Format.GetInstance().Serialize(dictionary, Convert.FromBase64String(token));
		}
	}
}
