﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using Newtonsoft.Json.Linq;

namespace FlexParse
{
	public class Value : Instruction
	{
		private string _deserializedTypeName;
		public TypeDef Type { get; private set; }
		public string FieldName { get; set; }

		public virtual void Read(JToken target, ReaderContext context)
		{
			target[FieldName] = JToken.FromObject(Type.Read(context));
		}

		public virtual void Write(JToken item, WriterContext context)
		{
			Type.Write(item[FieldName], context);
		}

		public void PostDeserialization(TypeSet set)
		{
			Type = set[_deserializedTypeName];
		}

		#region IXmlSerializable

		public XmlSchema GetSchema()
		{
			return null;
		}

		public virtual void ReadXml(XmlReader reader)
		{
			reader.MoveToContent();
			FieldName = reader.GetAttribute("Name");
			_deserializedTypeName = reader.GetAttribute("Type");
			reader.Skip();
		}

		public virtual void WriteXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}

		#endregion IXmlSerializable
	}
}