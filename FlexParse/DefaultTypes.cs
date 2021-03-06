﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace FlexParse
{
	public static class DefaultTypes
	{
		public static void PopulateTypeSet(TypeSet set)
		{
			set.Add(new Boolean());
			set.Add(new Byte());
			set.Add(new Int16());
			set.Add(new Int32());
			set.Add(new Int64());
			set.Add(new Float());
			set.Add(new SizedString());
			set.Add(new NullString());
			set.Add(new Guid());
		}

		private sealed class Boolean : TypeDef
		{
			public string Name => nameof(Boolean);

			public JToken Read(ReaderContext context)
			{
				return new JValue(context.Reader.ReadBool());
			}

			public void Write(JToken value, WriterContext context)
			{
				context.Writer.Write(value.ToObject<bool>());
			}
		}

		private sealed class Byte : TypeDef
		{
			public string Name => nameof(Byte);

			public JToken Read(ReaderContext context)
			{
				return new JValue(context.Reader.ReadByte());
			}

			public void Write(JToken value, WriterContext context)
			{
				context.Writer.Write(value.ToObject<byte>());
			}
		}

		private sealed class Int16 : TypeDef
		{
			public string Name => nameof(Int16);

			public JToken Read(ReaderContext context)
			{
				return new JValue(context.Reader.ReadInt16());
			}

			public void Write(JToken value, WriterContext context)
			{
				context.Writer.Write(value.ToObject<short>());
			}
		}

		private sealed class Int32 : TypeDef
		{
			public string Name => nameof(Int32);

			public JToken Read(ReaderContext context)
			{
				return new JValue(context.Reader.ReadInt32());
			}

			public void Write(JToken value, WriterContext context)
			{
				context.Writer.Write(value.ToObject<int>());
			}
		}

		private sealed class Int64 : TypeDef
		{
			public string Name => nameof(Int64);

			public JToken Read(ReaderContext context)
			{
				return new JValue(context.Reader.ReadInt64());
			}

			public void Write(JToken value, WriterContext context)
			{
				context.Writer.Write(value.ToObject<long>());
			}
		}

		private sealed class Float : TypeDef
		{
			public string Name => nameof(Float);

			public JToken Read(ReaderContext context)
			{
				return new JValue(context.Reader.ReadFloat());
			}

			public void Write(JToken value, WriterContext context)
			{
				context.Writer.Write(value.ToObject<long>());
			}
		}

		private sealed class SizedString : TypeDef
		{
			public string Name => nameof(SizedString);

			public JToken Read(ReaderContext context)
			{
				return new JValue(context.Reader.ReadLengthSuffixedString());
			}

			public void Write(JToken value, WriterContext context)
			{
				string text = value.ToObject<string>();
				context.Writer.Write(text.Length);
				context.Writer.Write(text);
			}
		}

		private sealed class NullString : TypeDef
		{
			public string Name => nameof(NullString);

			public JToken Read(ReaderContext context)
			{
				return new JValue(context.Reader.ReadNullTerminatedString());
			}

			public void Write(JToken value, WriterContext context)
			{
				context.Writer.Write(value.ToObject<string>());
				context.Writer.Write((byte)0x00);
			}
		}

		private sealed class Guid : TypeDef
		{
			public string Name => nameof(System.Guid);

			public JToken Read(ReaderContext context)
			{
				return new System.Guid(context.Reader.ReadBytes(16));
			}

			public void Write(JToken value, WriterContext context)
			{
				context.Writer.WriteBytes(value.Value<System.Guid>().ToByteArray());
			}
		}
	}
}