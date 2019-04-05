using System;
using System.Collections;
using System.Text;

namespace YGOM
{
	public static class Json
	{
		public static string Serialize(object obj)
		{
			return Json.Serializer.Serialize(obj);
		}

		private sealed class Serializer
		{
			private Serializer()
			{
				this._builder = new StringBuilder();
			}

			public static string Serialize(object obj)
			{
				Json.Serializer serializer = new Json.Serializer();
				serializer.SerializeValue(obj);
				return serializer._builder.ToString();
			}

			private void SerializeValue(object value)
			{
				if (value == null)
				{
					this._builder.Append("null");
					return;
				}
				string str;
				if ((str = (value as string)) != null)
				{
					this.SerializeString(str);
					return;
				}
				if (value is bool)
				{
					this._builder.Append((!(bool)value) ? "false" : "true");
					return;
				}
				IList anArray;
				if ((anArray = (value as IList)) != null)
				{
					this.SerializeArray(anArray);
					return;
				}
				IDictionary obj;
				if ((obj = (value as IDictionary)) != null)
				{
					this.SerializeObject(obj);
					return;
				}
				if (value is char)
				{
					this.SerializeString(new string((char)value, 1));
					return;
				}
				this.SerializeOther(value);
			}

			private void SerializeObject(IDictionary obj)
			{
				bool flag = true;
				this._builder.Append('{');
				IEnumerator enumerator = obj.Keys.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						if (!flag)
						{
							this._builder.Append(',');
						}
						this.SerializeString(obj2.ToString());
						this._builder.Append(':');
						this.SerializeValue(obj[obj2]);
						flag = false;
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
				this._builder.Append('}');
			}

			private void SerializeArray(IList anArray)
			{
				this._builder.Append('[');
				bool flag = true;
				IEnumerator enumerator = anArray.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object value = enumerator.Current;
						if (!flag)
						{
							this._builder.Append(',');
						}
						this.SerializeValue(value);
						flag = false;
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
				this._builder.Append(']');
			}

			private void SerializeString(string str)
			{
				this._builder.Append('"');
				char[] array = str.ToCharArray();
				int i = 0;
				while (i < array.Length)
				{
					char c = array[i];
					switch (c)
					{
					case '\b':
						this._builder.Append("\\b");
						break;
					case '\t':
						this._builder.Append("\\t");
						break;
					case '\n':
						this._builder.Append("\\n");
						break;
					case '\v':
						goto IL_84;
					case '\f':
						this._builder.Append("\\f");
						break;
					case '\r':
						this._builder.Append("\\r");
						break;
					default:
						goto IL_84;
					}
					IL_123:
					i++;
					continue;
					IL_84:
					if (c == '"')
					{
						this._builder.Append("\\\"");
						goto IL_123;
					}
					if (c == '\\')
					{
						this._builder.Append("\\\\");
						goto IL_123;
					}
					int num = Convert.ToInt32(c);
					if (num >= 32 && num <= 126)
					{
						this._builder.Append(c);
						goto IL_123;
					}
					this._builder.Append("\\u");
					this._builder.Append(num.ToString("x4"));
					goto IL_123;
				}
				this._builder.Append('"');
			}

			private void SerializeOther(object value)
			{
				if (value != null)
				{
					bool flag = value is float;
					float num = flag ? ((float)value) : 0f;
					if (!flag)
					{
						bool flag2 = value is int;
						if (flag2)
						{
							int num2 = (int)value;
						}
						if (!flag2)
						{
							bool flag3 = value is uint;
							if (flag3)
							{
								uint num3 = (uint)value;
							}
							if (!flag3)
							{
								bool flag4 = value is long;
								if (flag4)
								{
									long num4 = (long)value;
								}
								if (!flag4)
								{
									bool flag5 = value is sbyte;
									if (flag5)
									{
										sbyte b = (sbyte)value;
									}
									if (!flag5)
									{
										bool flag6 = value is byte;
										if (flag6)
										{
											byte b2 = (byte)value;
										}
										if (!flag6)
										{
											bool flag7 = value is short;
											if (flag7)
											{
												short num5 = (short)value;
											}
											if (!flag7)
											{
												bool flag8 = value is ushort;
												if (flag8)
												{
													ushort num6 = (ushort)value;
												}
												if (!flag8)
												{
													bool flag9 = value is ulong;
													if (flag9)
													{
														ulong num7 = (ulong)value;
													}
													if (!flag9)
													{
														bool flag10 = value is double;
														if (flag10)
														{
															double num8 = (double)value;
														}
														if (!flag10)
														{
															bool flag11 = value is decimal;
															if (flag11)
															{
																decimal num9 = (decimal)value;
															}
															if (!flag11)
															{
																goto IL_16A;
															}
														}
														this._builder.Append(Convert.ToDouble(value).ToString("R"));
														return;
													}
												}
											}
										}
									}
								}
							}
						}
						this._builder.Append(value);
						return;
					}
					float num10 = num;
					this._builder.Append(num10.ToString("R"));
					return;
				}
				IL_16A:
				this.SerializeString(value.ToString());
			}

			private readonly StringBuilder _builder;
		}
	}
}
